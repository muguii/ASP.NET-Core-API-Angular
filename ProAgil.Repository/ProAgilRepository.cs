using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilDbContext Context;

        public ProAgilRepository(ProAgilDbContext context)
        {
            Context = context;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //GERAIS
        public void Add<T>(T entity) where T : class
        {
            Context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            int affectedRows = await Context.SaveChangesAsync();
            return affectedRows > 0;
        }


        //EVENTO
        public async Task<List<Evento>> GetAllEventosAsync(bool includePalestrantes)
        {
            IQueryable<Evento> query = Context.EVENTO.Include(evento => evento.Lotes).Include(evento => evento.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(evento => evento.PalestrantesEventos).ThenInclude(palestrante => palestrante.Palestrante);
            }

            List<Evento> eventos = await query.AsNoTracking().OrderBy(evento => evento.Id).ToListAsync();
            return eventos;
        }

        public async Task<List<Evento>> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = Context.EVENTO.Include(evento => evento.Lotes).Include(evento => evento.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(evento => evento.PalestrantesEventos).ThenInclude(palestrante => palestrante.Palestrante);
            }

            List<Evento> eventos = await query.AsNoTracking().Where(evento => evento.Tema.Contains(tema, StringComparison.OrdinalIgnoreCase)).OrderByDescending(evento => evento.DataEvento).ToListAsync();
            return eventos;
        }

        public async Task<Evento> GetEventoAsyncById(int id, bool includePalestrantes)
        {
            IQueryable<Evento> query = Context.EVENTO.Include(evento => evento.Lotes).Include(evento => evento.RedesSociais);

            if (includePalestrantes)
            {
                query = query.Include(evento => evento.PalestrantesEventos).ThenInclude(palestrante => palestrante.Palestrante);
            }

            Evento evento = await query.AsNoTracking().Where(evento => evento.Id.Equals(id)).OrderByDescending(evento => evento.DataEvento).SingleOrDefaultAsync();
            return evento;
        }


        //PALESTRANTE
        public async Task<List<Palestrante>> GetAllPalestrantesAsyncByName(string name, bool includeEventos)
        {
            IQueryable<Palestrante> query = Context.PALESTRANTE.Include(palestrante => palestrante.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(palestrante => palestrante.PalestrantesEventos).ThenInclude(evento => evento.Evento);
            }

            List<Palestrante> palestrantes = await query.AsNoTracking().Where(palestrante => palestrante.Nome.Equals(name, StringComparison.OrdinalIgnoreCase)).OrderByDescending(palestrante => palestrante.Nome).ToListAsync();
            return palestrantes;
        }

        public async Task<Palestrante> GetPalestranteAsyncById(int id, bool includeEventos)
        {
            IQueryable<Palestrante> query = Context.PALESTRANTE.Include(palestrante => palestrante.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(palestrante => palestrante.PalestrantesEventos).ThenInclude(evento => evento.Evento);
            }

            Palestrante palestrante = await query.AsNoTracking().Where(palestrante => palestrante.Id.Equals(id)).SingleOrDefaultAsync();
            return palestrante;
        }    
    }
}
