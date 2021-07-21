using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.DTOs;
using ProAgil.Domain;
using ProAgil.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Controllers
{
    [ApiController] // Adicionando isso entendesse que o parâmetro está sendo passado via corpo do POST, assim não há necessidade de adicionar o [FromBody] no parâmetro do método -- ESTUDAR MELHOR ESSE DECORATOR
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository Repository;
        private readonly IMapper Mapper;

        public EventoController(IProAgilRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Evento> eventos = await Repository.GetAllEventosAsync(true);
                List<EventoDTO> eventosViewModel = Mapper.Map<List<EventoDTO>>(eventos);

                return Ok(eventosViewModel);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro! {exception.Message}");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                Evento evento = await Repository.GetEventoAsyncById(eventoId, true);
                EventoDTO eventoViewModel = Mapper.Map<EventoDTO>(evento);

                return Ok(eventoViewModel);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro! {exception.Message}");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                List<Evento> eventos = await Repository.GetAllEventosAsyncByTema(tema, true);
                List<EventoDTO> eventosViewModel = Mapper.Map<List<EventoDTO>>(eventos);

                return Ok(eventosViewModel);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro! {exception.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO inputModel) //TODO: Criar ViewModel e InputModel
        {
            try
            {
                Evento eventoInputModel = Mapper.Map<Evento>(inputModel);
                Repository.Add(eventoInputModel);

                if (await Repository.SaveChangeAsync())
                {
                    return Created($"/api/evento/{inputModel.Id}", eventoInputModel); //Tem necessidade de fazer um Map aqui? Unica divergencia é devido ao inputModel não ter o ID.
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro! {exception.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDTO inputModel)
        {
            try
            {
                Evento evento = await Repository.GetEventoAsyncById(eventoId, false);

                if (evento == null)
                {
                    return NotFound();
                }

                Mapper.Map(inputModel, evento);
                Repository.Update(evento);

                if (await Repository.SaveChangeAsync())
                {
                    return Created($"/api/evento/{eventoId}", inputModel); //Tem necessidade de fazer um Map aqui?
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro! {exception.Message}");
            }
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                Evento evento = await Repository.GetEventoAsyncById(eventoId, false);

                if (evento == null)
                {
                    return NotFound();
                }

                Repository.Delete(evento);

                if (await Repository.SaveChangeAsync())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro! {exception.Message}");
            }
        }
    }
}
