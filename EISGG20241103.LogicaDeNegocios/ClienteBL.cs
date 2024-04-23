using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EISGG20241103.AccesoADatos;
using EISGG20241103.EntidadesDeNegocio;

namespace EISGG20241103.LogicaDeNegocios
{
    public class ClienteBL
    {
       
            readonly ClienteDAL _clienteDAL;
            public ClienteBL(ClienteDAL clienteDAL)
            {
                _clienteDAL = clienteDAL;
            }
            public async Task<List<Cliente>> ObtenerTodos()
            {
                return await _clienteDAL.ObtenerTodos();
            }
            public async Task<Cliente> ObtenerPorId(Cliente cliente)
            {
                return await _clienteDAL.ObtenerPorId(cliente);
            }
            public async Task<int> Crear(Cliente cliente)
            {
                return await _clienteDAL.Crear(cliente);
            }
            public async Task<int> Modificar(Cliente cliente)
            {
                return await _clienteDAL.Modificar(cliente);
            }
            public async Task<int> Eliminar(Cliente cliente)
            {
                return await _clienteDAL.Eliminar(cliente);
            }
            public bool ClienteExists(int id)
            {
                return _clienteDAL.ClienteExists(id);
            }
        
    }
}
