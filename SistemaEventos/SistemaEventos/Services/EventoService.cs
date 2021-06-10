using Microsoft.EntityFrameworkCore;
using SistemaEventos.DAL;
using SistemaEventos.Domain.DTO;
using SistemaEventos.Domain.Entity;
using SistemaEventos.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEventos.Services
{
    public class EventoService
    {
        private readonly SistemaEventosContext _dbContext;
        public EventoService(SistemaEventosContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceResponse<EventoResponse> CadastrarNovo(EventoCreateRequest model)
		{
            var resultadoCategoria = _dbContext.CategoriaEventos.FirstOrDefault(x => x.IdCategoriaEvento == model.Categoria);  // Valida se a categoria existe no banco de dados.

            if (resultadoCategoria == null)
            {
                return new ServiceResponse<EventoResponse>("Categoria inválida.");
            }

            if (model.DataHoraInicio.Date <= DateTime.Now)
            {
                return new ServiceResponse<EventoResponse>("A data de ínicio deve ser superior ao dia atual.");
			}
            else if (model.DataHoraFim.Date != model.DataHoraInicio.Date)
            {
                return new ServiceResponse<EventoResponse>("A data final do evento deve ser no mesmo dia da data de ínicio");
            }

            var novoEvento = new Evento()
            {
                Nome = model.Nome,
                DataHoraInicio = model.DataHoraInicio,
                DataHoraFim = model.DataHoraFim,
                Local = model.Local,
                Descricao = model.Descricao,
                LimiteVagas = model.LimiteVagas.Value,
                IdCategoriaEvento = model.Categoria.Value,
                IdEventoStatus = 1
            };
            _dbContext.Add(novoEvento);
            _dbContext.SaveChanges();
            var retorno = new EventoResponse(novoEvento);
            return new ServiceResponse<EventoResponse>(retorno);
        }

        public IEnumerable<EventoResponse> ListarTodosDisponiveis()
        {
            var retornoDoBanco = _dbContext.Eventos.Where(x => x.IdEventoStatus == 1).ToList();

            IEnumerable<EventoResponse> lista = retornoDoBanco.Select(x => new EventoResponse(x));

            return lista;
        }

        public ServiceResponse<EventoResponse> PesquisarPorCategoria(int idCategoria)
        {
            var resultado = _dbContext.Eventos.FirstOrDefault(x => x.IdCategoriaEvento == idCategoria);
            if (resultado == null)
                return new ServiceResponse<EventoResponse>("Não encontrado!");
            else
                return new ServiceResponse<EventoResponse>(new EventoResponse(resultado));
        }

        public ServiceResponse<EventoResponse> PesquisarPorData(DateTime data)
        {
            var resultado = _dbContext.Eventos.FirstOrDefault(x => x.DataHoraInicio.Date == data.Date);
            if (resultado == null)
                return new ServiceResponse<EventoResponse>("Não encontrado!");
            else
                return new ServiceResponse<EventoResponse>(new EventoResponse(resultado));
        }
        public ServiceResponse<EventoResponse> AlterarStatus(int id, EventoUpdateRequest model)
        {
            var resultadoStatus = _dbContext.StatusEventos.FirstOrDefault(x => x.IdEventoStatus == model.EventoStatus);  // Valida se a categoria existe no banco de dados.

            if (resultadoStatus == null)
            {
                return new ServiceResponse<EventoResponse>("Status inválido.");
            }
           
            var evento = _dbContext.Eventos.FirstOrDefault(x => x.IdEvento == id);
            if (evento == null)
            {
                return new ServiceResponse<EventoResponse>("Não encontrado!");
            }

            evento.IdEventoStatus = model.EventoStatus.Value;
            _dbContext.Eventos.Add(evento).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return new ServiceResponse<EventoResponse>(new EventoResponse(evento));
        }
    }

}
