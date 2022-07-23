using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayerDB.DataBaseScaffold;

using DataLayerDB.Interface;
using DataLayerDB.Implement;
using eStore.Models;
using DataLayer.Interface;
using AutoMapper;

namespace eStore.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly eStoreContext _context;

        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public object Session { get; private set; }

        public OrderDetailsController(eStoreContext context, IProductRepository productRepository, IMapper mapper, IOrderDetailsRepository orderDetailsRepository)
        {
            _context = context;
            _orderDetailsRepository = orderDetailsRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }





        // GET: OrderDetails
        public async Task<IActionResult> Index(int? OrderID)
        {
            return _context.OrderDetails != null ? 
                          View(await _context.OrderDetails.ToListAsync()) :
                          Problem("Entity set 'eStoreContext.OrderDetails'  is null.");
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //if (id == 0 || _context.OrderDetails == null)
            //{
            //    return Create(id);
            //}
            //var orderDetail = await _context.OrderDetails
            //    .FirstOrDefaultAsync(m => m.OrderId == id);
            //if (orderDetail == null)
            //{
            //    return Create(id);
            //}
            //    return View(orderDetail);

            if (id == 0 || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetail == null)
            {
                return RedirectToAction("Create", new { OrderId = id});
            }
            //else
            //{
            //    OrderDetailsViewModel detail = new OrderDetailsViewModel();
            //    detail.orderDetails = orderDetail;
            //}

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create(int OrderID)
        {
            OrderDetailsViewModel detail = new OrderDetailsViewModel();
            detail.Products = _mapper.Map<List<ProductViewModel>>(_productRepository.GetProducts().ToList());
            detail.OrderId = OrderID ;
            return View(detail);
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {   
                int orderId = orderDetail.OrderId;

                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || _context.OrderDetails == null)
            {
                return RedirectToAction("Create", new { OrderId = id });
            }

            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return RedirectToAction("Create", new { OrderId = id });
            }
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductId,UnitPrice,Quantity,Discount")] OrderDetail orderDetail)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderId))
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
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderDetails == null)
            {
                return Problem("Entity set 'eStoreContext.OrderDetails'  is null.");
            }
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details));
        }

        private bool OrderDetailExists(int id)
        {
          return (_context.OrderDetails?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
