﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayerDB.DataBaseScaffold;

using DataLayerDB.Interface;
using DataLayerDB.Implement;

using Microsoft.AspNetCore.Http;
using eStore.Models;

namespace eStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly eStoreContext _context;

        private readonly IOrderRepository _orderRepository;

        private readonly IOrderDetailsRepository _orderDetailRepository;

        public OrdersController(eStoreContext context, IMemberRepository memberRepository, IMapper mapper)
        {
            _context = context;

            _orderRepository = new OrderRepository(context);

            _orderDetailRepository = new OrderDetailsRepository(context);
        }

        // GET: Orders
        public ActionResult Index(String startDate, string endDate)
        {
            IQueryable<Order> result;
            if (startDate != null && endDate != null)
            {
                
                DateTime startDateParam = DateTime.Parse(startDate);
                DateTime endDateParam = DateTime.Parse(endDate);

                result = _orderRepository.GetAllByOrderTime(startDateParam, endDateParam);
            } else
            {
                result = _orderRepository.GetAll();
            }
            return View(result);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = _orderRepository.GetById((int)id);
            
            if (order == null)
            {
                return NotFound();
            }

            var orderDetails = _orderDetailRepository.GetAllOrdersDetailsByOrderId(order.OrderId);

            OrderStatisticViewModel result = new OrderStatisticViewModel();
            result.Order = order;
            result.OrderDetails = orderDetails.ToList();
            return View(result);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            OrderViewModel order = new OrderViewModel();
            order.Members = _mapper.Map<List<MemberViewModel>>(_memberRepository.GetMembers().ToList());
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,MemberId,OrderDate,RequireDate,ShippedDate,Freight")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,MemberId,OrderDate,RequireDate,ShippedDate,Freight")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _orderRepository.UpdateOrder(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'eStoreContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
