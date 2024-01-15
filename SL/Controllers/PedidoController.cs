﻿using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Pedido.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ML.Pedido pedido)
        {
            ML.Result result = BL.Pedido.Add(pedido);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("delete/{idPedido}")]
        public IActionResult Delete(int idPedido)
        {
            ML.Pedido pedido = new ML.Pedido();
            pedido.IdPedido = Convert.ToInt32(idPedido);
            ML.Result result = BL.Pedido.Delete(pedido);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
