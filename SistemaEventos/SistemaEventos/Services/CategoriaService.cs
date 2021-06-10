using SistemaEventos.DAL;
using SistemaEventos.Domain.DTO;
using SistemaEventos.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEventos.Services
{
    public class CategoriaService
    {
        private readonly SistemaEventosContext _dbContext;
        public CategoriaService(SistemaEventosContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<CategoriaResponse> ListarTodasCategorias()
        {

            List<CategoriaEvento> categorias = _dbContext.CategoriaEventos.ToList();
            List<CategoriaResponse> result = categorias.ConvertAll(categoria => new CategoriaResponse(categoria));

            return result;
        }
    }
}
