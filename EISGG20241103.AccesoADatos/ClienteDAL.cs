using Microsoft.EntityFrameworkCore;
using EISGG20241103.EntidadesDeNegocio;
using System;

namespace EISGG20241103.AccesoADatos
{
    public class ClienteDAL
    {
        readonly EISG20241103DBContext _context;
        public ClienteDAL(EISG20241103DBContext appDBContext)
        {
            _context = appDBContext;
        }
        public async Task<List<Cliente>> ObtenerTodos()
        {
            return _context.Clientes != null ?
                await _context.Clientes.ToListAsync() :
                new List<Cliente>();
        }
        public async Task<Cliente> ObtenerPorId(Cliente cliente)
        {

            var clienteResult = await _context.Clientes
                .Include(s => s.DetalleClientes)
                .FirstOrDefaultAsync(m => m.Id == cliente.Id);
            if (clienteResult != null)
                return clienteResult;
            else
                return new Cliente();
        }
        public async Task<int> Crear(Cliente cliente)
        {
            _context.Add(cliente);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Modificar(Cliente cliente)
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
            var delDetIds = cliente.DetalleClientes.Where(s => s.Id < 0).Select(s => -s.Id).ToList();
            if (delDetIds != null && delDetIds.Count > 0)
            {
                foreach (var detalleId in delDetIds) // Cambiado de 'id' a 'detalleId'
                {
                    var det = await _context.DetalleClientes.FindAsync(detalleId); // Cambiado de 'id' a 'detalleId'
                    if (det != null)
                    {
                        _context.DetalleClientes.Remove(det);
                    }
                }
            }
            _context.Update(clienteUpdate);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Eliminar(Cliente cliente)
        {
            var clienteDel = await _context.Clientes.FindAsync(cliente.Id);
            if (cliente != null)
            {
                _context.Clientes.Remove(clienteDel);
            }

            return await _context.SaveChangesAsync();
        }

        public bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }

}
