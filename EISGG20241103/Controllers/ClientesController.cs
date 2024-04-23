using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using EISGG20241103.Models;
using EISGG20241103.LogicaDeNegocios;
using EISGG20241103.EntidadesDeNegocio;


namespace EISGG20241103.Controllers
{
    public class ClientesController : Controller
    {

        readonly ClienteBL _clienteBL;

        public ClientesController(ClienteBL clienteBL)
        {
            _clienteBL = clienteBL;
        }
        // GET: Clientes
        //Existe dal
        public async Task<IActionResult> Index()
        {
            return View(await _clienteBL.ObtenerTodos());
        }

        //Existe DAL
        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteBL.ObtenerPorId(new Cliente { Id = id });
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
        //EXISTE A DAL
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Edad,DetalleClientes")] Cliente cliente)
        {
            await _clienteBL.Crear(cliente);
            return RedirectToAction(nameof(Index));
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
                det.Id = det.Id * -1;
            }
            else
            {
                cliente.DetalleClientes.RemoveAt(index);
            }

            ViewBag.Accion = accion;
            return View(accion, cliente);
        }
        // GET: Clientes/Edit/5
        //EXISTE A DAL
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteBL.ObtenerPorId(new Cliente { Id = id });
            if (cliente == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Edit";
            return View(cliente);
        }
        //Existe dal
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
                    await _clienteBL.Modificar(cliente);
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
        //Existe dal
        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteBL.ObtenerPorId(new Cliente { Id = id });
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
            await _clienteBL.Eliminar(new Cliente { Id = id });
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _clienteBL.ClienteExists(id);
        }
    }
}
