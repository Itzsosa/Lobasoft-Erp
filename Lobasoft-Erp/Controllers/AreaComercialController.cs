using Microsoft.AspNetCore.Mvc;
using Lobasoft_Erp.Models;
using Lobasoft_Erp.Data;
using Microsoft.EntityFrameworkCore;

namespace Lobasoft_Erp.Controllers
{
    public class AreaComercialController : Controller
    {
        private readonly Contexto _contexto;
        public AreaComercialController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _contexto.LBS_AreaComercial.ToListAsync());
        }

        // GET: AreaComercialController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LBS_AreaComercial area)
        {
            try
            {
                _contexto.Add(area);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.WriteLine("Error");
                return View(area);
            }
        }//fin del crear

        // GET: AreaComercialController/Edit/
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            var temp = await _contexto.LBS_AreaComercial.FindAsync(id);

            if (temp == null)
            {
                return NotFound();
            }

            else
            {
                return View(temp);
            }
        }//fin del get edit

        // POST: AreaComercialController/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LBS_AreaComercial area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            try
            {
                _contexto.Update(area);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.WriteLine("Error");
                return View(area);
            }
        }//fin del post edit

        // GET: AreaComercialController/Delete/
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            var temp = await _contexto.LBS_AreaComercial.FindAsync(id);

            if (temp == null)
            {
                return NotFound();
            }
            else
            {
                return View(temp);
            }
        }//fin del get delete

        // POST: AreaComercialController/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var temp = await _contexto.LBS_AreaComercial.FindAsync(id);
            _contexto.LBS_AreaComercial.Remove(temp);
            await _contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }//fin del post delete

        // GET: AreaComercialController/Details/
        public async Task<ActionResult> Details(int? id)
        {
            var temp = await _contexto.LBS_AreaComercial.FindAsync(id);

            if (temp == null)
            {
                return NotFound();
            }

            else
            {
                return View(temp);
            }
        }//fin del details

    }//fin de la class
}//fin del namespace