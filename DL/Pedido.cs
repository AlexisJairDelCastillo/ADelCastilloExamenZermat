using System;
using System.Collections.Generic;

namespace DL;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int? IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public int? IdProducto { get; set; }

    public string? NombreProducto { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }

    public int? Cantidad { get; set; }

    public string? Cupon { get; set; }

    public decimal? Total { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
