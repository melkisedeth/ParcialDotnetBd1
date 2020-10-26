using System;
using System.Collections.Generic;
using System.Linq;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ParcialDotnet.Models;

namespace WebPulsaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApoyoController : ControllerBase
    {

        private readonly ApoyoService _apoyoService;
        public IConfiguration Configuration { get; }
        public ApoyoController(IConfiguration configuration)
        {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _apoyoService = new ApoyoService(connectionString);
        }

        [HttpGet]
        public IEnumerable<ApoyoViewModel> Gets()
        {
            var apoyos = _apoyoService.ConsultarTodos().Select(p => new ApoyoViewModel(p));
            return apoyos;
        }


        // GET: api/Persona/5


        // POST: api/Persona
        [HttpPost]
        public ActionResult<ApoyoViewModel> Post(ApoyoInputModel apoyoInput)
        {
            Apoyo apoyo = MapearApoyo(apoyoInput);
            var response = _apoyoService.Guardar(apoyo);
            if (response.Error)
            {
                return BadRequest(response.Mensaje);
            }
            return Ok(response.Apoyo);
            /*if (persona.modalidad == "Economico")
            {
                valorTotal = calcularValorTotal(persona, personas) + Convert.ToInt32(persona.Aporte);
                persona.TotalAporteEconomico = valorTotal;
            }

            if (valorTotal <= 600000000)
            {
                _personaService.Guardar(persona);
                mensaje = "Registrado correctamente";
            }
            else
            {
                mensaje = "Se acabaron los recursos";
            }*/

        }

        private Apoyo MapearApoyo(ApoyoInputModel apoyoInput)
        {
            var apoyo = new Apoyo
            {
                modalidad = apoyoInput.modalidad,
                Aporte = apoyoInput.aporte,
                fecha = apoyoInput.fecha,
            };
            return apoyo;
        }

       


        // DELETE: api/Persona/5


        /* private int calcularValorTotal(Persona persona, List<Persona> valores)
         {
             int suma = 0;

             for (int i = 0; i < valores.Count; i++)
             {
                 if (valores[i].modalidad == "Economico")
                 {
                     suma = suma + Convert.ToInt32(valores[i].Aporte);
                 }
             }

             return suma;
         }
         */

        // PUT: api/Persona/5

    }
}