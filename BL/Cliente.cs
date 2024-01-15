using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cliente
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenZermatContext context = new DL.AdelCastilloExamenZermatContext())
                {
                    var query = context.Clientes.FromSqlRaw("ClienteGetAll").ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();

                        foreach(DL.Cliente resultCliente in query)
                        {
                            ML.Cliente cliente = new ML.Cliente();
                            cliente.IdCliente = resultCliente.IdCliente;
                            cliente.Nombre = resultCliente.Nombre;
                            cliente.ApellidoPaterno = resultCliente.ApellidoPaterno;
                            cliente.ApellidoMaterno = resultCliente.ApellidoMaterno;

                            result.Objects.Add(cliente);
                        }

                        result.Correct = true;
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los registros." + ex.Message;
            }

            return result;
        }
    }
}
