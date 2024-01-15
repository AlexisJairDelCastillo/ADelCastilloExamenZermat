using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pedido
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenZermatContext context = new DL.AdelCastilloExamenZermatContext())
                {
                    var query = context.Pedidos.FromSqlRaw("PedidoGetAll").ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.Pedido resultPedido in query)
                        {
                            ML.Pedido pedido = new ML.Pedido();
                            pedido.IdPedido = resultPedido.IdPedido;
                            pedido.Cliente = new ML.Cliente();
                            pedido.Cliente.IdCliente = resultPedido.IdCliente.Value;
                            pedido.Cliente.Nombre = resultPedido.Nombre;
                            pedido.Cliente.ApellidoPaterno = resultPedido.ApellidoPaterno;
                            pedido.Cliente.ApellidoMaterno = resultPedido.ApellidoMaterno;
                            pedido.Producto = new ML.Producto();
                            pedido.Producto.IdProducto = resultPedido.IdProducto.Value;
                            pedido.Producto.NombreProducto = resultPedido.NombreProducto;
                            pedido.Producto.Descripcion = resultPedido.Descripcion;
                            pedido.Producto.Precio = resultPedido.Precio;
                            pedido.Cantidad = resultPedido.Cantidad;
                            pedido.Cupon = resultPedido.Cupon;
                            pedido.Total = resultPedido.Total;

                            result.Objects.Add(pedido);
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

        public static ML.Result Add(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenZermatContext context = new DL.AdelCastilloExamenZermatContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"PedidoAdd {pedido.Cliente.IdCliente}, {pedido.Producto.IdProducto}, {pedido.Cantidad}, '{pedido.Cupon}'");

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se inserto correctamente.";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al insertar el registro." + ex.Message;
            }

            return result;
        }

        public static ML.Result Delete(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenZermatContext context = new DL.AdelCastilloExamenZermatContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"PedidoDelete {pedido.IdPedido}");

                    if(query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se elimino correctamente.";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar el registro." + ex.Message;
            }

            return result;
        }
    }
}
