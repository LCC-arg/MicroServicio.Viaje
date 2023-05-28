﻿using Application.Exceptions;
using Application.Interfaces.IApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Client
{


    public class DestinoApi : IDestinoApi
    {

        private readonly HttpClient _httpClient;

        public DestinoApi()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7018");
        }

        public dynamic GetDestinoById(int destinoId)
        {
            HttpResponseMessage response = _httpClient.GetAsync($"/api/Destinos/ViajeCiudad/{destinoId}").Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic destino = response.Content.ReadAsAsync<dynamic>().Result;
                return destino;
            }
            else
            {
                throw new NotFoundException($"Error al obtener el Destino. Código de respuesta: {response.StatusCode}");
            }

        }
    }
}

