using SistemaEventos.DAL;
using System;
using SistemaEventos.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaEventos.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace SistemaEventos.Services.Base
{
    public class ParticipacaoService
    {
        private readonly SistemaEventosContext _dbContext;
        public ParticipacaoService(SistemaEventosContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<ParticipacaoResponse> ListarTodos()
        {

            List<Participacao> participantes = _dbContext.Participacaos.ToList();
            List<ParticipacaoResponse> participacaoResponse = participantes.ConvertAll(categoria => new ParticipacaoResponse(categoria));

            return participacaoResponse;
        }
        public List<ParticipacaoResponse> ParticipantePorEvento(int id)
        {
            List<Participacao> retorno = _dbContext.Participacaos.Where(x => x.IdEvento == id).ToList();


            List<ParticipacaoResponse> eventoResponse = retorno.ConvertAll(evento => new ParticipacaoResponse(evento));

            return eventoResponse;
        }
        public ServiceResponse<Participacao> ParticipantePorId(int id)
        {
            var participante = _dbContext.Participacaos.FirstOrDefault(x => x.IdParticipacao == id);
            if (participante == null)
            {
                return new ServiceResponse<Participacao>("Participante não encontrado!");
            }
            else
            {
                return new ServiceResponse<Participacao>(participante);
            }
        }
    }
}
