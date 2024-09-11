using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppVendas.Data;
using AppVendas.Models;

namespace AppVendas.Controllers
{
    public class ItemDaVendasController : Controller
    {
        private readonly AplicationDbContext _context;

        public ItemDaVendasController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemDaVendas
        public async Task<IActionResult> Index()
        {
            var aplicationDbContext = _context.itensDaVenda.Include(i => i.Produto);
            return View(await aplicationDbContext.ToListAsync());
        }

        // GET: ItemDaVendas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDaVenda = await _context.itensDaVenda
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItemDavendaId == id);
            if (itemDaVenda == null)
            {
                return NotFound();
            }

            return View(itemDaVenda);
        }

        // GET: ItemDaVendas/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.produtos, "ProdutoId", "ProdutoId");
            return View();
        }

        // POST: ItemDaVendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemDavendaId,VendaId,ProdutoId,QtdadeVendida,ValorTotalDoItem")] ItemDaVenda itemDaVenda)
        {
            if (ModelState.IsValid)
            {
                itemDaVenda.ItemDavendaId = Guid.NewGuid();
                _context.Add(itemDaVenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.produtos, "ProdutoId", "ProdutoId", itemDaVenda.ProdutoId);
            return View(itemDaVenda);
        }

        // GET: ItemDaVendas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDaVenda = await _context.itensDaVenda.FindAsync(id);
            if (itemDaVenda == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.produtos, "ProdutoId", "ProdutoId", itemDaVenda.ProdutoId);
            return View(itemDaVenda);
        }

        // POST: ItemDaVendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemDavendaId,VendaId,ProdutoId,QtdadeVendida,ValorTotalDoItem")] ItemDaVenda itemDaVenda)
        {
            if (id != itemDaVenda.ItemDavendaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemDaVenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemDaVendaExists(itemDaVenda.ItemDavendaId))
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
            ViewData["ProdutoId"] = new SelectList(_context.produtos, "ProdutoId", "ProdutoId", itemDaVenda.ProdutoId);
            return View(itemDaVenda);
        }

        // GET: ItemDaVendas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDaVenda = await _context.itensDaVenda
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.ItemDavendaId == id);
            if (itemDaVenda == null)
            {
                return NotFound();
            }

            return View(itemDaVenda);
        }

        // POST: ItemDaVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemDaVenda = await _context.itensDaVenda.FindAsync(id);
            if (itemDaVenda != null)
            {
                _context.itensDaVenda.Remove(itemDaVenda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemDaVendaExists(Guid id)
        {
            return _context.itensDaVenda.Any(e => e.ItemDavendaId == id);
        }
    }
}
