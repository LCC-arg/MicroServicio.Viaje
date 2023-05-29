﻿using Application.Exceptions;
using Application.Interfaces.IServices;
using Application.Request;
using Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio_Viaje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasajeroController : ControllerBase
    {
        private readonly IPasajeroServices _pasajeroService;

        public PasajeroController(IPasajeroServices pasajeroService)
        {
            _pasajeroService = pasajeroService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Pasajero), 201)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 409)]
        public IActionResult CreatePasajero(PasajeroRequest request) 
        {
            try
            {
                var result = _pasajeroService.CreatePasajero(request);
                return new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
            }
            catch(BadRequestException ex) 
            {
                return new JsonResult(new BadRequest { message = ex.Message }) { StatusCode = 400 };
            }
            catch(HasConflictException ex)
            {
                return new JsonResult(new BadRequest { message = ex.Message }) { StatusCode = 409 };
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Pasajero), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult GetPasajeroById(int id)
        {
            try
            {

                if (!int.TryParse(id.ToString(), out _))
                {
                    throw new BadRequestException("El formato de id ingresado es invalido");
                }

                var result = _pasajeroService.GetPasajeroById(id);


                return Ok(result);
            }
            catch(BadRequestException ex)
            {
                return new JsonResult(new BadRequest { message = ex.Message }){ StatusCode = 400 };
            }
            catch(NotFoundException ex)
            {
                return new JsonResult(new BadRequest { message = ex.Message }) { StatusCode = 404 };
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PasajeroResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult DeletePasajero(int id)
        {
            try
            {
                if (!int.TryParse(id.ToString(), out _))
                {
                    throw new BadRequestException("El formato de id ingresado es invalido");
                }

                var result = _pasajeroService.DeletePasajero(id);
                return Ok(result);
            }
            catch(BadRequestException ex)
            {
                return new JsonResult(new BadRequest { message = ex.Message }) { StatusCode = 400 };
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PasajeroResponse), 200)]
        [ProducesResponseType(typeof(BadRequest), 404)]
        public IActionResult UpdatePasajero(int id, PasajeroRequest request)
        {
            try
            {
                if (!int.TryParse(id.ToString(), out _))
                {
                    throw new BadRequestException("El formato de id ingresado es invalido");
                }

                var result = _pasajeroService.UpdatePasajero(id, request);

                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (NotFoundException ex)
            {
                return new JsonResult(new BadRequest { message = ex.Message }) { StatusCode = 404 };
            }
            catch (HasConflictException ex)
            {
                return new JsonResult(new BadRequest { message = ex.Message }) { StatusCode = 409 };
            }
            catch(BadRequestException ex)
            {
                return new JsonResult(new BadRequest { message = ex.Message }) { StatusCode = 400 };
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PasajeroResponse>), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        public IActionResult GetPasajeros(string? nombre, string? apellido, DateTime? fechaNacimiento,int? dni, string? nacionalidad, string? genero)
        {
            try
            {
                var result = _pasajeroService.GetPasajeros(nombre, apellido, fechaNacimiento, dni, genero, nacionalidad);
                return Ok(result);
            }
            catch(BadRequestException ex)
            {
                return new JsonResult(new BadRequest { message = ex.Message }) { StatusCode = 400 };
            }
        }

    }
}
