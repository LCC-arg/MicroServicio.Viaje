﻿using Application.Exceptions;
using Application.Interfaces.IApi;
using Application.Interfaces.ICommands;
using Application.Interfaces.IQuerys;
using Application.Interfaces.IServices;
using Application.Request;
using Application.Response;
using Application.Response.Ciudad;
using Application.Response.Pais;
using Application.Response.Provincia;
using Application.Response.ViajeCiudad;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class PasajeroServices : IPasajeroServices
    {
        private readonly IPasajeroCommand _pasajeroCommand;
        private readonly IPasajeroQuery _pasajeroQuery;
        private readonly IViajeQuery _viajeQuery;
        private readonly ITransporteApi _transporteApi;
        private readonly IDestinoApi _destinoApi;

        public PasajeroServices(IPasajeroCommand pasajeroCommand, IPasajeroQuery pasajeroQuery, IViajeQuery viajeQuery, ITransporteApi transporteApi,IDestinoApi destinoApi)
        {
            _pasajeroCommand = pasajeroCommand;
            _pasajeroQuery = pasajeroQuery;
            _viajeQuery = viajeQuery;
            _transporteApi = transporteApi;
            _destinoApi = destinoApi;
        }

        public PasajeroResponse AddPasajero(Pasajero pasajero)
        {
            throw new NotImplementedException();
        }

        public PasajeroResponse CreatePasajero(PasajeroRequest pasajeroRequest)
        {
            if (!ValidarInt(pasajeroRequest.dni))
            {
                throw new BadRequestException("Formato de dni ingresado incorrecto");
            }
            else if(!ValidarInt(pasajeroRequest.numContactoEmergencia))
            {
                throw new BadRequestException("Formato de numero de emergencia incorrecto");
            }

            IEnumerable<Pasajero> pasajeros = _pasajeroQuery.GetAll();

            foreach(Pasajero pasajero in pasajeros)
            {
                if(pasajero.ViajeId == pasajeroRequest.viajeId && pasajero.Dni == pasajeroRequest.dni)
                {
                    throw new HasConflictException("Ya existe otro pasajero con su mismo dni en este viaje");
                }
            }

            Viaje viaje = _viajeQuery.GetById(pasajeroRequest.viajeId);

            if (viaje == null)
            {
                throw new HasConflictException("El viaje no existe en la base de datos");
            }

            Pasajero newPasajero = new Pasajero
            {
                Nombre = pasajeroRequest.nombre,
                Apellido = pasajeroRequest.apellido,
                Dni = pasajeroRequest.dni,
                FechaNacimiento = pasajeroRequest.fechaNacimiento,
                Genero = pasajeroRequest.genero,
                NumContactoEmergencia = pasajeroRequest.numContactoEmergencia,
                ViajeId = pasajeroRequest.viajeId,
                Nacionalidad = pasajeroRequest.nacionalidad,
                Viaje = viaje
            };

            dynamic response = Response(newPasajero.Viaje.TransporteId);
            dynamic responseOrigen = ResponseOrigen(newPasajero.Viaje.CiudadOrigen);
            dynamic responseDestino = ResponseDestino(newPasajero.Viaje.CiudadDestino);

            var result = _pasajeroCommand.Insert(newPasajero);
            

            PasajeroResponse pasajeroResponse = new PasajeroResponse
            {
                id = result.PasajeroId,
                nombre = result.Nombre,
                apellido = result.Apellido,
                dni = result.Dni,
                fechaNacimiento = result.FechaNacimiento,
                genero = result.Genero,
                numContactoEmergencia = result.NumContactoEmergencia,
                viaje = new ViajeResponse
                {
                    id = result.ViajeId,
                    ciudadOrigen = new ViajeCiudadResponse
                    {
                        Id = responseOrigen.id,
                        ViajeId = responseOrigen.id,
                        Ciudad = new CiudadResponse
                        {
                            Id = responseOrigen.ciudad.id,
                            Nombre = responseOrigen.ciudad.Nombre,
                            Provincia = new ProvinciaResponse
                            {
                                Id = responseOrigen.ciudad.provincia.id,
                                Nombre = responseOrigen.ciudad.provincia.Nombre,
                                Pais = new PaisResponse
                                {
                                    Id = responseOrigen.ciudad.provincia.pais.id,
                                    Nombre = responseOrigen.ciudad.provincia.pais.Nombre
                                }
                            }

                        }
                    },
                    ciudadDestino = new ViajeCiudadResponse
                    {
                        Id = responseDestino.id,
                        ViajeId = responseDestino.id,
                        Ciudad = new CiudadResponse
                        {
                            Id = responseDestino.ciudad.id,
                            Nombre = responseDestino.ciudad.Nombre,
                            Provincia = new ProvinciaResponse
                            {
                                Id = responseDestino.ciudad.provincia.id,
                                Nombre = responseDestino.ciudad.provincia.Nombre,
                                Pais = new PaisResponse
                                {
                                    Id = responseDestino.ciudad.provincia.pais.id,
                                    Nombre = responseDestino.ciudad.provincia.pais.Nombre
                                }
                            }

                        }
                    },
                    transporte = new TransporteResponse
                    {
                        id = response.id,
                        tipoTransporte = new TipoTransporteResponse
                        {
                            id = response.tipoTransporteResponse.id,
                            descripcion = response.tipoTransporteResponse.descripcion
                        },
                        companiaTransporte = new CompaniaTransporteResponse
                        {
                            id = response.companiaTransporteResponse.id,
                            razonSocial = response.companiaTransporteResponse.razonSocial,
                            cuit = response.companiaTransporteResponse.cuit
                        }
                    },
                    duracion = result.Viaje.Duracion,
                    horarioSalida = result.Viaje.HorarioSalida,
                    horarioLlegada = result.Viaje.HorarioLlegada,
                    fechaSalida = result.Viaje.FechaSalida,
                    fechaLlegada = result.Viaje.FechaLlegada,
                    tipoViaje = result.Viaje.TipoViaje
                }
            };
            return pasajeroResponse;
        }


        public PasajeroResponse DeletePasajero(int pasajeroId)
        {
            if(!int.TryParse(pasajeroId.ToString(), out _))
            {
                throw new BadRequestException("Formato del Id de pasajero incorrecto");
            }

            var pasajero = _pasajeroCommand.Delete(pasajeroId);

            if (pasajero == null)
            {
                throw new BadRequestException("No existe pasajero con ese Id");
            }
            dynamic response = Response(pasajero.Viaje.TransporteId);
            dynamic responseOrigen = ResponseOrigen(pasajero.Viaje.CiudadOrigen);
            dynamic responseDestino = ResponseDestino(pasajero.Viaje.CiudadDestino);
            return new PasajeroResponse
            {
                id = pasajero.PasajeroId,
                nombre = pasajero.Nombre,
                apellido = pasajero.Apellido,
                dni = pasajero.Dni,
                fechaNacimiento = pasajero.FechaNacimiento,
                genero = pasajero.Genero,
                numContactoEmergencia = pasajero.NumContactoEmergencia,
                viaje = new ViajeResponse
                {
                    id = pasajero.ViajeId,
                    ciudadOrigen = new ViajeCiudadResponse
                    {
                        Id = responseOrigen.id,
                        ViajeId = responseOrigen.id,
                        Ciudad = new CiudadResponse
                        {
                            Id = responseOrigen.ciudad.id,
                            Nombre = responseOrigen.ciudad.Nombre,
                            Provincia = new ProvinciaResponse
                            {
                                Id = responseOrigen.ciudad.provincia.id,
                                Nombre = responseOrigen.ciudad.provincia.Nombre,
                                Pais = new PaisResponse
                                {
                                    Id = responseOrigen.ciudad.provincia.pais.id,
                                    Nombre = responseOrigen.ciudad.provincia.pais.Nombre
                                }
                            }

                        }
                    },
                    ciudadDestino = new ViajeCiudadResponse
                    {
                        Id = responseDestino.id,
                        ViajeId = responseDestino.id,
                        Ciudad = new CiudadResponse
                        {
                            Id = responseDestino.ciudad.id,
                            Nombre = responseDestino.ciudad.Nombre,
                            Provincia = new ProvinciaResponse
                            {
                                Id = responseDestino.ciudad.provincia.id,
                                Nombre = responseDestino.ciudad.provincia.Nombre,
                                Pais = new PaisResponse
                                {
                                    Id = responseDestino.ciudad.provincia.pais.id,
                                    Nombre = responseDestino.ciudad.provincia.pais.Nombre
                                }
                            }

                        }
                    },
                    transporte = new TransporteResponse
                    {
                        id = response.id,
                        tipoTransporte = new TipoTransporteResponse
                        {
                            id = response.tipoTransporteResponse.id,
                            descripcion = response.tipoTransporteResponse.descripcion
                        },
                        companiaTransporte = new CompaniaTransporteResponse
                        {
                            id = response.companiaTransporteResponse.id,
                            razonSocial = response.companiaTransporteResponse.razonSocial,
                            cuit = response.companiaTransporteResponse.cuit
                        }
                    },
                    duracion = pasajero.Viaje.Duracion,
                    horarioSalida = pasajero.Viaje.HorarioSalida,
                    horarioLlegada = pasajero.Viaje.HorarioLlegada,
                    fechaSalida = pasajero.Viaje.FechaSalida,
                    fechaLlegada = pasajero.Viaje.FechaLlegada,
                    tipoViaje = pasajero.Viaje.TipoViaje
                }
            };

        }

        public IEnumerable<PasajeroResponse> GetAllPasajeros()
        {
            throw new NotImplementedException();
        }

        public PasajeroResponse GetPasajeroById(int pasajeroId)
        {
            if(!int.TryParse(pasajeroId.ToString(), out _))
            {
                throw new BadRequestException("Formato de id del pasajero invalido");
            }

            var pasajero = _pasajeroQuery.GetById(pasajeroId);
            var viaje = _viajeQuery.GetById(pasajero.ViajeId);
            if(pasajero != null) 
            {
                dynamic response = Response(viaje.TransporteId);
                dynamic responseOrigen = ResponseOrigen(viaje.CiudadOrigen);
                dynamic responseDestino = ResponseDestino(viaje.CiudadDestino);
                PasajeroResponse pasajeroResponse = new PasajeroResponse
                {
                    id = pasajero.PasajeroId,
                    nombre = pasajero.Nombre,
                    apellido = pasajero.Apellido,
                    dni = pasajero.Dni,
                    fechaNacimiento = pasajero.FechaNacimiento,
                    genero = pasajero.Genero,
                    numContactoEmergencia = pasajero.NumContactoEmergencia,
                    viaje = new ViajeResponse
                    {
                        id = viaje.ViajeId,
                        ciudadOrigen = new ViajeCiudadResponse
                        {
                            Id = responseOrigen.id,
                            ViajeId = responseOrigen.id,
                            Ciudad = new CiudadResponse
                            {
                                Id = responseOrigen.ciudad.id,
                                Nombre = responseOrigen.ciudad.Nombre,
                                Provincia = new ProvinciaResponse
                                {
                                    Id = responseOrigen.ciudad.provincia.id,
                                    Nombre = responseOrigen.ciudad.provincia.Nombre,
                                    Pais = new PaisResponse
                                    {
                                        Id = responseOrigen.ciudad.provincia.pais.id,
                                        Nombre = responseOrigen.ciudad.provincia.pais.Nombre
                                    }
                                }

                            }
                        },
                        ciudadDestino = new ViajeCiudadResponse
                        {
                            Id = responseDestino.id,
                            ViajeId = responseDestino.id,
                            Ciudad = new CiudadResponse
                            {
                                Id = responseDestino.ciudad.id,
                                Nombre = responseDestino.ciudad.Nombre,
                                Provincia = new ProvinciaResponse
                                {
                                    Id = responseDestino.ciudad.provincia.id,
                                    Nombre = responseDestino.ciudad.provincia.Nombre,
                                    Pais = new PaisResponse
                                    {
                                        Id = responseDestino.ciudad.provincia.pais.id,
                                        Nombre = responseDestino.ciudad.provincia.pais.Nombre
                                    }
                                }

                            }
                        },
                        transporte = new TransporteResponse
                        {
                            id = response.id,
                            tipoTransporte = new TipoTransporteResponse
                            {
                                id = response.tipoTransporteResponse.id,
                                descripcion = response.tipoTransporteResponse.descripcion
                            },
                            companiaTransporte = new CompaniaTransporteResponse
                            {
                                id = response.companiaTransporteResponse.id,
                                razonSocial = response.companiaTransporteResponse.razonSocial,
                                cuit = response.companiaTransporteResponse.cuit
                            }
                        },
                        duracion = viaje.Duracion,
                        horarioSalida = viaje.HorarioSalida,
                        horarioLlegada = viaje.HorarioLlegada,
                        fechaSalida = viaje.FechaSalida,
                        fechaLlegada = viaje.FechaLlegada,
                        tipoViaje = viaje.TipoViaje
                    }
                };
                return pasajeroResponse;
            }
            throw new NotFoundException("No existe pasajero con ese id");
        }

        public IEnumerable<PasajeroResponse> GetPasajeros(string? nombre, string? apellido, DateTime? fechaNacimiento, int? dni, string? nacionalidad, string? genero)
        {
            if (!int.TryParse(dni.ToString(), out _) && dni != null)
            {
                throw new BadRequestException("El formato de dni es invalido");
            }
            if(!DateTime.TryParse(fechaNacimiento.ToString(), out _) && fechaNacimiento != null)
            {
                throw new BadRequestException("El formato de la fecha de nacimiento es invalido");
            }

            var pasajeros = _pasajeroQuery.GetPasajeros(nombre, apellido, fechaNacimiento, dni, genero, nacionalidad);

            List<PasajeroResponse> pasajerosResponses = new List<PasajeroResponse>();    
            if (pasajeros != null) 
            {
                foreach(Pasajero pasajero in pasajeros)
                {
                    dynamic response = Response(pasajero.Viaje.TransporteId);
                    dynamic responseOrigen = Response(pasajero.Viaje.CiudadOrigen);
                    dynamic responseDestino = Response(pasajero.Viaje.CiudadDestino);
                    PasajeroResponse pasajeroResponse = new PasajeroResponse
                    {
                        id = pasajero.PasajeroId,
                        nombre = pasajero.Nombre,
                        apellido = pasajero.Apellido,
                        dni = pasajero.Dni,
                        fechaNacimiento = pasajero.FechaNacimiento,
                        genero = pasajero.Genero,
                        numContactoEmergencia = pasajero.NumContactoEmergencia,
                        viaje = new ViajeResponse
                        {
                            id = pasajero.Viaje.ViajeId,
                            ciudadOrigen = new ViajeCiudadResponse
                            {
                                Id = responseOrigen.id,
                                ViajeId = responseOrigen.id,
                                Ciudad = new CiudadResponse
                                {
                                    Id = responseOrigen.ciudad.id,
                                    Nombre = responseOrigen.ciudad.Nombre,
                                    Provincia = new ProvinciaResponse
                                    {
                                        Id = responseOrigen.ciudad.provincia.id,
                                        Nombre = responseOrigen.ciudad.provincia.Nombre,
                                        Pais = new PaisResponse
                                        {
                                            Id = responseOrigen.ciudad.provincia.pais.id,
                                            Nombre = responseOrigen.ciudad.provincia.pais.Nombre
                                        }
                                    }

                                }
                            },
                            ciudadDestino = new ViajeCiudadResponse
                            {
                                Id = responseDestino.id,
                                ViajeId = responseDestino.id,
                                Ciudad = new CiudadResponse
                                {
                                    Id = responseDestino.ciudad.id,
                                    Nombre = responseDestino.ciudad.Nombre,
                                    Provincia = new ProvinciaResponse
                                    {
                                        Id = responseDestino.ciudad.provincia.id,
                                        Nombre = responseDestino.ciudad.provincia.Nombre,
                                        Pais = new PaisResponse
                                        {
                                            Id = responseDestino.ciudad.provincia.pais.id,
                                            Nombre = responseDestino.ciudad.provincia.pais.Nombre
                                        }
                                    }

                                }
                            },
                            transporte = new TransporteResponse
                            {
                                id = response.id,
                                tipoTransporte = new TipoTransporteResponse
                                {
                                    id = response.tipoTransporteResponse.id,
                                    descripcion = response.tipoTransporteResponse.descripcion
                                },
                                companiaTransporte = new CompaniaTransporteResponse
                                {
                                    id = response.companiaTransporteResponse.id,
                                    razonSocial = response.companiaTransporteResponse.razonSocial,
                                    cuit = response.companiaTransporteResponse.cuit
                                }
                            },
                            duracion = pasajero.Viaje.Duracion,
                            horarioSalida = pasajero.Viaje.HorarioSalida,
                            horarioLlegada = pasajero.Viaje.HorarioLlegada,
                            fechaSalida = pasajero.Viaje.FechaSalida,
                            fechaLlegada = pasajero.Viaje.FechaLlegada,
                            tipoViaje = pasajero.Viaje.TipoViaje
                        }
                    };
                    pasajerosResponses.Add(pasajeroResponse);  
                }
                return pasajerosResponses;
            }
            return null;
        }

        public PasajeroResponse UpdatePasajero(int pasajeroId, PasajeroRequest pasajeroRequest)
        {
            _pasajeroCommand.Update(pasajeroId, pasajeroRequest);
            Pasajero pasajero = _pasajeroQuery.GetById(pasajeroId);
            Viaje viaje = _viajeQuery.GetById(pasajeroRequest.viajeId);
            dynamic response = Response(pasajero.Viaje.TransporteId);
            dynamic responseOrigen = Response(pasajero.Viaje.CiudadOrigen);
            dynamic responseDestino = Response(pasajero.Viaje.CiudadDestino);
            return new PasajeroResponse
            {
                id = pasajero.PasajeroId,
                nombre = pasajero.Nombre,
                apellido = pasajero.Apellido,
                dni = pasajero.Dni,
                fechaNacimiento = pasajero.FechaNacimiento,
                genero = pasajero.Genero,
                numContactoEmergencia = pasajero.NumContactoEmergencia,
                viaje = new ViajeResponse
                {
                    id = viaje.ViajeId,
                    ciudadOrigen = new ViajeCiudadResponse
                    {
                        Id = responseOrigen.id,
                        ViajeId = responseOrigen.id,
                        Ciudad = new CiudadResponse
                        {
                            Id = responseOrigen.ciudad.id,
                            Nombre = responseOrigen.ciudad.Nombre,
                            Provincia = new ProvinciaResponse
                            {
                                Id = responseOrigen.ciudad.provincia.id,
                                Nombre = responseOrigen.ciudad.provincia.Nombre,
                                Pais = new PaisResponse
                                {
                                    Id = responseOrigen.ciudad.provincia.pais.id,
                                    Nombre = responseOrigen.ciudad.provincia.pais.Nombre
                                }
                            }

                        }
                    },
                    ciudadDestino = new ViajeCiudadResponse
                    {
                        Id = responseDestino.id,
                        ViajeId = responseDestino.id,
                        Ciudad = new CiudadResponse
                        {
                            Id = responseDestino.ciudad.id,
                            Nombre = responseDestino.ciudad.Nombre,
                            Provincia = new ProvinciaResponse
                            {
                                Id = responseDestino.ciudad.provincia.id,
                                Nombre = responseDestino.ciudad.provincia.Nombre,
                                Pais = new PaisResponse
                                {
                                    Id = responseDestino.ciudad.provincia.pais.id,
                                    Nombre = responseDestino.ciudad.provincia.pais.Nombre
                                }
                            }

                        }
                    },
                    transporte = new TransporteResponse
                    {
                        id = response.id,
                        tipoTransporte = new TipoTransporteResponse
                        {
                            id = response.tipoTransporteResponse.id,
                            descripcion = response.tipoTransporteResponse.descripcion
                        },
                        companiaTransporte = new CompaniaTransporteResponse
                        {
                            id = response.companiaTransporteResponse.id,
                            razonSocial = response.companiaTransporteResponse.razonSocial,
                            cuit = response.companiaTransporteResponse.cuit
                        }
                    },
                    duracion = viaje.Duracion,
                    horarioSalida = viaje.HorarioSalida,
                    horarioLlegada = viaje.HorarioLlegada,
                    fechaSalida = viaje.FechaSalida,
                    fechaLlegada = viaje.FechaLlegada,
                    tipoViaje = viaje.TipoViaje
                }
            };

        }

        private dynamic Response(int transporteId)
        {
            dynamic response = _transporteApi.GetTransporteById(transporteId);
            return response;
        }
        private dynamic ResponseDestino(int destinoId)
        {
            dynamic response = _destinoApi.GetDestinoById(destinoId);
            return response;

        }
        private dynamic ResponseOrigen(int origenId)
        {
            dynamic response = _destinoApi.GetDestinoById(origenId);
            return response;

        }

        public bool ValidarInt(int dato)
        {
           return int.TryParse(dato.ToString(), out _);
        }

    }
}
