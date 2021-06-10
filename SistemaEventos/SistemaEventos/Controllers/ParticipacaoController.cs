using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaEventos.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipacaoController : ControllerBase
    {
        private readonly ParticipacaoService participacaoService;
        public ParticipacaoController(ParticipacaoService participacaoService)
        {
            this.participacaoService = participacaoService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(participacaoService.ListarTodos());
        }
        [HttpGet("evento={id}")]
        public IActionResult ParticipantePorEvento(int id)
        {
            return Ok(participacaoService.ParticipantePorEvento(id));
        }
        //erro
        [HttpGet("{id}")]
        public IActionResult ParticipantePorId(int id)
        {
            var retorno = participacaoService.ParticipantePorId(id);

            if (retorno.Sucesso)
            {
                return Ok(retorno.ObjetoRetorno);
            }
            else
            {
                return NotFound(retorno.Mensagem);
            }
        }
    }
}
