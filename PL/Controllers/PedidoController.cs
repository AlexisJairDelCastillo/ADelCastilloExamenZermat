using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PedidoController : Controller
    {
        private IHostEnvironment environment;
        private IConfiguration configuration;

        public PedidoController(IHostEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Pedido pedido = new ML.Pedido();
            ML.Result resultPedido = new ML.Result();
            resultPedido.Objects = new List<object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("pedido/getall");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach(var item in readTask.Result.Objects)
                    {
                        ML.Pedido itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Pedido>(item.ToString());
                        resultPedido.Objects.Add(itemList);
                    }
                }

                pedido.Pedidos = resultPedido.Objects;
            }

            return View(pedido);
        }

        [HttpGet]
        public ActionResult Form(int? idPedido)
        {
            ML.Result resultCliente = BL.Cliente.GetAll();
            ML.Result resultCategoria = BL.Categoria.GetAll();

            ML.Pedido pedido = new ML.Pedido();
            pedido.Cliente = new ML.Cliente();
            pedido.Producto = new ML.Producto();
            pedido.Producto.Categoria = new ML.Categoria();

            pedido.Cliente.Clientes = resultCliente.Objects;
            pedido.Producto.Categoria.Categorias = resultCategoria.Objects;

            ViewBag.Titulo = "Agregar un nuevo registro.";
            ViewBag.Action = "Agregar";

            return View(pedido);
        }

        [HttpPost]
        public ActionResult Form(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();

            using(HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync<ML.Pedido>("pedido/add", pedido);
                postTask.Wait();

                HttpResponseMessage resultTask = postTask.Result;

                if (resultTask.IsSuccessStatusCode)
                {
                    result.Correct = true;

                    ViewBag.Titulo = "El registro se inserto correctamente.";
                    ViewBag.Message = result.Message;

                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al insertar el registro.";
                    ViewBag.Message = result.Message;

                    return View("Modal");
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(ML.Pedido pedido)
        {
            ML.Result resultPedido = new ML.Result();

            int idPedido = pedido.IdPedido;

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApi"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.DeleteAsync("pedido/delete/" + idPedido);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Titulo = "El registro se elimino correctamente.";

                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Ocurrio un error al eliminar el registro.";

                    return View("Modal");
                }
            }
        }

        [HttpGet]
        public JsonResult GetProductos(int idCategoria)
        {
            ML.Result result = BL.Producto.GetByIdCategoria(idCategoria);

            return Json(result);
        }
    }
}
