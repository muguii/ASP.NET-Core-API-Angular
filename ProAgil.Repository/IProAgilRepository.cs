using ProAgil.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangeAsync();

        //EVENTO
        Task<List<Evento>> GetAllEventosAsync(bool includePalestrantes);
        Task<List<Evento>> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);
        Task<Evento> GetEventoAsyncById(int id, bool includePalestrantes);

        //PALESTRANTE
        Task<List<Palestrante>> GetAllPalestrantesAsyncByName(string name, bool includeEventos);
        Task<Palestrante> GetPalestranteAsyncById(int id, bool includeEventos);
    }
}
