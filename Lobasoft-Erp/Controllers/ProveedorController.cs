using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lobasoft_Erp.Models;
using Lobasoft_Erp.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lobasoft_Erp.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly Contexto _contexto;

        public ProveedorController(Contexto contexto)
        {
            _contexto = contexto;
        }

        // GET: ProveedorController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.LBS_Proveedores.ToListAsync());
        }

        // GET: ProveedorController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var temp = await _contexto.LBS_Proveedores.FindAsync(id);

            if (temp == null)
            {
                return NotFound();
            }
            else
            {
                return View(temp);
            }
        }

        // GET: ProveedorController/Create
        [HttpGet]
        public ActionResult Create()
        {
            var areasComerciales = _contexto.LBS_AreaComercial.Select(a => new { a.Id, a.NombreAreaComercial }).ToList();
            ViewBag.AreasComerciales = areasComerciales;
            return View();
        }

        // POST: ProveedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LBS_Proveedores proveedor, int[] areasComercialesSeleccionadas)
        {
            if (proveedor.Nombre!="")
            {
                _contexto.Add(proveedor);
                await _contexto.SaveChangesAsync();

                if (areasComercialesSeleccionadas != null)
                {
                    foreach (var areaComercialId in areasComercialesSeleccionadas)
                    {
                        var asignacionAreaProveedor = new LBS_AsignacionAreaProveedor
                        {
                            A_idProveedor = proveedor.Id,
                            A_idAreaComercial = areaComercialId
                        };

                        _contexto.Add(asignacionAreaProveedor);
                        await _contexto.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var areasComerciales = _contexto.LBS_AreaComercial.Select(a => new { a.Id, a.NombreAreaComercial }).ToList();
            ViewBag.AreasComerciales = areasComerciales;

            return View(proveedor);
        }


        // GET: ProveedorController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            var temp = await _contexto.LBS_Proveedores.FindAsync(id);

            if (temp == null)
            {
                return NotFound();
            }
            else
            {
                return View(temp);
            }
        }

        // POST: ProveedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LBS_Proveedores proveedor)
        {
            if (id != proveedor.Id)
            {
                return NotFound();
            }

            try
            {
                _contexto.Update(proveedor);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                Console.WriteLine("Error");
                return View(proveedor);
            }
        }

        // GET: ProveedorController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            var temp = await _contexto.LBS_Proveedores.FindAsync(id);

            if (temp == null)
            {
                return NotFound();
            }
            else
            {
                return View(temp);
            }
        }

        // POST: ProveedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var temp = await _contexto.LBS_Proveedores.FindAsync(id);
            _contexto.LBS_Proveedores.Remove(temp);
            await _contexto.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    
    }
}
