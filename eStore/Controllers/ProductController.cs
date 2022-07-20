using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayerDB.DataBaseScaffold;
using DataLayer.Interface;
using AutoMapper;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly eStoreContext _context;
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public ProductController(eStoreContext context, IMemberRepository memberRepository, IMapper mapper)
        {
            _context = context;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }
        // GET: Product
        public async Task<IActionResult> Index(string txtProductName, int txtPrice)
        {
            var product = _context.Products.ToList();
            if (!String.IsNullOrEmpty(txtProductName))
            {
                product = _context.Products.Where(m => m.ProductName.Contains(txtProductName)).ToList();
            }
            if (txtPrice != 0)
            {
                product = _context.Products.Where(m => m.UnitPrice == txtPrice).ToList();
            }
            if (txtPrice != 0 && !String.IsNullOrEmpty(txtProductName))
            {
                product = _context.Products.Where(m => m.UnitPrice == txtPrice && m.ProductName.Contains(txtProductName)).ToList();
            }
            return View(product);


            //return _context.Products != null ? 
                          //View(await _context.Products.ToListAsync()) :
                          //Problem("Entity set 'eStoreContext.Products'  is null.");
        }

        public async Task<IActionResult> Search(string txtProductName, int txtPrice)
        {
            var product = _context.Products.ToList();
            if (!String.IsNullOrEmpty(txtProductName))
            {
                product = _context.Products.Where(m=>m.ProductName.Contains(txtProductName)).ToList();
            }
            if(txtPrice != 0)
            {
                product = _context.Products.Where(m=>m.UnitPrice==txtPrice).ToList();
            }
            if(txtPrice !=0 && !String.IsNullOrEmpty(txtProductName))
            {
                product = _context.Products.Where(m => m.UnitPrice == txtPrice && m.ProductName.Contains(txtProductName)).ToList();
            }
            return View(product);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,CategoryId,ProductName,Weight,UnitPrice,UnitInStock")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'eStoreContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
