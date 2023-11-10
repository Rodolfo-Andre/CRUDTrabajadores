using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDTrabajadores.Data;
using CRUDTrabajadores.Models;
using CRUDTrabajadores.Enum;

namespace CRUDTrabajadores.Controllers
{
    public class TrabajadorController : Controller
    {
        private readonly ContextoBD _context;
        private readonly IEnumerable<Objeto> listaSexo = System.Enum.GetNames(typeof(SexoEnum)).Select(s => new Objeto() { Valor = s });
        private readonly IEnumerable<Objeto> listaTipoDocumento = System.Enum.GetNames(typeof(TipoDocumentoEnum)).Select(s => new Objeto() { Valor = s });

        public TrabajadorController(ContextoBD context)
        {
            _context = context;
        }

        // GET: Trabajador
        public async Task<IActionResult> Index(string nombre)
        {
            var lista = await _context.Trabajador.FromSqlRaw("EXEC SP_OBTENER_TRABAJADORES {0}", nombre ?? "").ToListAsync();

            foreach (var trabajador in lista)
            {
                _context.Entry(trabajador).Reference(t => t.Departamento).Load();
                _context.Entry(trabajador).Reference(t => t.Provincia).Load();
                _context.Entry(trabajador).Reference(t => t.Distrito).Load();
            }

            ViewData["nombre"] = nombre;

            return View(lista);
        }

        // GET: Trabajador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trabajador == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajador
                .Include(t => t.Departamento)
                .Include(t => t.Distrito)
                .Include(t => t.Provincia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // GET: Trabajador/Create
        public IActionResult Create()
        {
            var primerDepartamento = _context.Departamento.First();
            var primeraProvinciaAsociadaDepartamento = _context.Provincia.Where(p => p.DepartamentoId == primerDepartamento.Id).First();

            ViewData["SelectSexo"] = new SelectList(listaSexo, "Valor", "Valor");
            ViewData["SelectTipoDocumento"] = new SelectList(listaTipoDocumento, "Valor", "Valor");
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "Id", "NombreDepartamento", primerDepartamento);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia.Where(p => p.DepartamentoId == primerDepartamento.Id), "Id", "NombreProvincia", primeraProvinciaAsociadaDepartamento);
            ViewData["DistritoId"] = new SelectList(_context.Distrito.Where(d => d.ProvinciaId == primeraProvinciaAsociadaDepartamento.Id), "Id", "NombreDistrito");
            return View();
        }

        // POST: Trabajador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,Telefono,DepartamentoId,ProvinciaId,DistritoId")] Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SelectSexo"] = new SelectList(listaSexo, "Valor", "Valor", trabajador.Sexo);
            ViewData["SelectTipoDocumento"] = new SelectList(listaTipoDocumento, "Valor", "Valor", trabajador.TipoDocumento);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "Id", "NombreDepartamento", trabajador.DepartamentoId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajador.ProvinciaId);
            ViewData["DistritoId"] = new SelectList(_context.Distrito, "Id", "NombreDistrito", trabajador.DistritoId);
            return View(trabajador);
        }

        // GET: Trabajador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trabajador == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajador.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }

            ViewData["SelectSexo"] = new SelectList(listaSexo, "Valor", "Valor", trabajador.Sexo);
            ViewData["SelectTipoDocumento"] = new SelectList(listaTipoDocumento, "Valor", "Valor", trabajador.TipoDocumento);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "Id", "NombreDepartamento", trabajador.DepartamentoId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajador.ProvinciaId);
            ViewData["DistritoId"] = new SelectList(_context.Distrito, "Id", "NombreDistrito", trabajador.DistritoId);
            return View(trabajador);
        }

        // POST: Trabajador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,Telefono,DepartamentoId,ProvinciaId,DistritoId")] Trabajador trabajador)
        {
            if (id != trabajador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadorExists(trabajador.Id))
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

            ViewData["SelectSexo"] = new SelectList(listaSexo, "Valor", "Valor", trabajador.Sexo);
            ViewData["SelectTipoDocumento"] = new SelectList(listaTipoDocumento, "Valor", "Valor", trabajador.TipoDocumento);
            ViewData["DepartamentoId"] = new SelectList(_context.Departamento, "Id", "NombreDepartamento", trabajador.DepartamentoId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajador.ProvinciaId);
            ViewData["DistritoId"] = new SelectList(_context.Distrito, "Id", "NombreDistrito", trabajador.DistritoId);
            return View(trabajador);
        }

        // GET: Trabajador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trabajador == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajador
                .Include(t => t.Departamento)
                .Include(t => t.Distrito)
                .Include(t => t.Provincia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // POST: Trabajador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajador = await _context.Trabajador.FindAsync(id);

            if (trabajador != null)
            {
                _context.Trabajador.Remove(trabajador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadorExists(int id)
        {
            return (_context.Trabajador?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
