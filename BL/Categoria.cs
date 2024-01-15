using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenZermatContext context = new DL.AdelCastilloExamenZermatContext())
                {
                    var query = context.Categoria.FromSqlRaw("CategoriaGetAll").ToList();

                    if(query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.Categorium resultCategoria in query)
                        {
                            ML.Categoria categoria = new ML.Categoria();
                            categoria.IdCategoria = resultCategoria.IdCategoria;
                            categoria.NombreCategoria = resultCategoria.NombreCategoria;

                            result.Objects.Add(categoria);
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
