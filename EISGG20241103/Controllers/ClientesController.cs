using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EISGG20241103.Models;

namespace EISGG20241103.Controllers
{
    public class ClientesController : Controller
    {
        private readonly EISG20241103DBContext _context;

        public ClientesController(EISG20241103DBContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
              return _context.Clientes != null ? 
                          View(await _context.Clientes.ToListAsync()) :
                          Problem("Entity set 'EISG20241103DBContext.Clientes'  is null.");
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(s=> s.DetalleClientes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewBag.Accion = "Details";
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            var cliente = new Cliente();
            cliente.DetalleClientes = new List<DetalleCliente>();
            cliente.DetalleClientes.Add(new DetalleCliente
            {
                Telefono = ""
            });
            ViewBag.Accion = "Create";
            return View(cliente);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Edad,DetalleClientes")] Cliente cliente)
        {
        //    cliente.DetalleClientes.Add(new DetalleCliente { Telefono = ""});
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            //return View(cliente);
        }
        [HttpPost]
        public ActionResult AgregarDetalles([Bind("Id,Nombre,Apellido,Email,Edad,DetalleClientes")] Cliente cliente, string accion)
        {
            cliente.DetalleClientes.Add(new DetalleCliente { Telefono=" " });
            ViewBag.Accion = accion;
            return View(accion, cliente);
        }
        public ActionResult EliminarDetalles([Bind("Id,Nombre,Apellido,Email,Edad,DetalleClientes")] Cliente cliente,
           int index, string accion)
        {
            var det = cliente.DetalleClientes[index];
            if (accion == "Edit" && det.Id > 0)
            {
                det.Id = det.Id;
            }
            else
            {
                cliente.DetalleClientes.RemoveAt(index);
            }

            ViewBag.Accion = accion;
            return View(accion, cliente);
        }
        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(s=>s.DetalleClientes).FirstAsync(s=>s.Id==id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Edit";
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,Edad,DetalleClientes")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

           
                try
                {
                var clienteUpdate = await _context.Clientes
                       .Include(s => s.DetalleClientes)
                       .FirstAsync(s => s.Id == cliente.Id);
                clienteUpdate.Nombre = cliente.Nombre;
                clienteUpdate.Apellido = cliente.Apellido;
                clienteUpdate.Email = cliente.Email;
                clienteUpdate.Edad = cliente.Edad;

                // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                var detNew = cliente.DetalleClientes.Where(s => s.Id == 0);
                foreach (var d in detNew)
                {
                    clienteUpdate.DetalleClientes.Add(d);
                }
                // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                var detUpdate = cliente.DetalleClientes.Where(s => s.Id > 0);
                foreach (var d in detUpdate)
                {
                    var det = clienteUpdate.DetalleClientes.FirstOrDefault(s => s.Id == d.Id);
                    det.Telefono = d.Telefono;
                   
                }
                // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                var delDet = cliente.DetalleClientes.Where(s => s.Id < 0).ToList();
                if (delDet != null && delDet.Count > 0)
                {
                    foreach (var d in delDet)
                    {
                        d.Id = d.Id * -1;
                        var det = clienteUpdate.DetalleClientes.FirstOrDefault(s => s.Id == d.Id);
                        _context.Remove(det);
                        // facturaUpdate.DetFacturaVenta.Remove(det);
                    }
                }
                _context.Update(clienteUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            //return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(s=>s.DetalleClientes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewBag.Accion = "Delete";
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'EISG20241103DBContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
