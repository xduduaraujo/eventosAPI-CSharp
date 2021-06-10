using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEventos.Services;
using SistemaEventos.Domain.DTO;

namespace SistemaEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly EventoService eventoService;

        public EventosController(EventoService eventoService)
        {
            this.eventoService = eventoService;
        }

        [HttpGet("disponiveis")]
        public IEnumerable<EventoResponse> Get()
        {
            return eventoService.ListarTodosDisponiveis();
        }


        [HttpGet("categoria={idCat}")]
        public IActionResult GetByCat(int idCat)
        {
            var retorno = eventoService.PesquisarPorCategoria(idCat);

            if (retorno.Sucesso)
            {
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return NotFound(retorno);
            }
        }


        [HttpGet("data={data}")]
        public IActionResult GetByData(DateTime data)
        {
            var retorno = eventoService.PesquisarPorData(data.Date);

            if (retorno.Sucesso)
            {
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return NotFound(retorno);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] EventoCreateRequest postModel)
        {
            if (ModelState.IsValid)
            {
                var retorno = eventoService.CadastrarNovo(postModel);
                if (!retorno.Sucesso)
                {
                    return BadRequest(retorno.Mensagem);
                }
                else
                {
                    return Ok(retorno);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut("status={id}")]
        public IActionResult Put(int id, [FromBody] EventoUpdateRequest putModel)
        {
            if (ModelState.IsValid)
            {
                var retorno = eventoService.AlterarStatus(id, putModel);
                if (!retorno.Sucesso)
                    return BadRequest(retorno);
                else
                    return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}