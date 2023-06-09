using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lobasoft_Erp.Data;
using Lobasoft_Erp.Models;

namespace Lobasoft_Erp.Controllers
{
    public class LBS_AreaComercialController : Controller
    {
        private readonly Contexto _context;

        public LBS_AreaComercialController(Contexto context)
        {
            _context = context;
        }

        // GET: LBS_AreaComercial
        public async Task<IActionResult> Index()
        {
              return _context.LBS_AreaComercial != null ? 
                          View(await _context.LBS_AreaComercial.ToListAsync()) :
                          Problem("Entity set 'Contexto.LBS_AreaComercial'  is null.");
        }

        // GET: LBS_AreaComercial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LBS_AreaComercial == null)
            {
                return NotFound();
            }

            var lBS_AreaComercial = await _context.LBS_AreaComercial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lBS_AreaComercial == null)
            {
                return NotFound();
            }

            return View(lBS_AreaComercial);
        }

        // GET: LBS_AreaComercial/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LBS_AreaComercial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreAreaComercial,Descripcion")] LBS_AreaComercial lBS_AreaComercial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lBS_AreaComercial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lBS_AreaComercial);
        }

        // GET: LBS_AreaComercial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LBS_AreaComercial == null)
            {
                return NotFound();
            }

            var lBS_AreaComercial = await _context.LBS_AreaComercial.FindAsync(id);
            if (lBS_AreaComercial == null)
            {
                return NotFound();
            }
            return View(lBS_AreaComercial);
        }

        // POST: LBS_AreaComercial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreAreaComercial,Descripcion")] LBS_AreaComercial lBS_AreaComercial)
        {
            if (id != lBS_AreaComercial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lBS_AreaComercial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LBS_AreaComercialExists(lBS_AreaComercial.Id))
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
            return View(lBS_AreaComercial);
        }

        // GET: LBS_AreaComercial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LBS_AreaComercial == null)
            {
                return NotFound();
            }

            var lBS_AreaComercial = await _context.LBS_AreaComercial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lBS_AreaComercial == null)
            {
                return NotFound();
            }

            return View(lBS_AreaComercial);
        }

        // POST: LBS_AreaComercial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LBS_AreaComercial == null)
            {
                return Problem("Entity set 'Contexto.LBS_AreaComercial'  is null.");
            }
            var lBS_AreaComercial = await _context.LBS_AreaComercial.FindAsync(id);
            if (lBS_AreaComercial != null)
            {
                _context.LBS_AreaComercial.Remove(lBS_AreaComercial);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LBS_AreaComercialExists(int id)
        {
          return (_context.LBS_AreaComercial?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
