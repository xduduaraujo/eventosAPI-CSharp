using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaEventos.Domain.DTO;
using SistemaEventos.Domain.Entity;
using SistemaEventos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            this.categoriaService = categoriaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(categoriaService.ListarTodasCategorias());
        }
    }
}
