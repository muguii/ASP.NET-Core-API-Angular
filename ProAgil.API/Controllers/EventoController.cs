using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository Repository;

        public EventoController(IProAgilRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Evento> eventos = await Repository.GetAllEventosAsync(true);
                return Ok(eventos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro!");
            }
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                Evento evento = await Repository.GetEventoAsyncById(eventoId, true);
                return Ok(evento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro!");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                List<Evento> eventos = await Repository.GetAllEventosAsyncByTema(tema, true);
                return Ok(eventos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento inputModel)
        {
            try
            {
                Repository.Add(inputModel);

                if (await Repository.SaveChangeAsync())
                {
                    return Created($"/api/evento/{inputModel.Id}", inputModel);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro!");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, Evento inputModel)
        {
            try
            {
                Evento evento = await Repository.GetEventoAsyncById(eventoId, false);

                if (evento == null)
                {
                    return NotFound();
                }

                Repository.Update(inputModel);

                if (await Repository.SaveChangeAsync())
                {
                    return Created($"/api/evento/{eventoId}", inputModel);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro!");
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro!");
            }
        }
    }
}
