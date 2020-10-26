using System;
using System.Collections.Generic;
using System.Linq;
using Entidad;
using Logica;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using pulsacionesdotnet.Models;

namespace WebPulsaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {

        private readonly PersonaService _personaService;
        public IConfiguration Configuration { get; }
        public PersonaController(IConfiguration configuration)
        {
            Configuration = configuration;
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _personaService = new PersonaService(connectionString);
        }
        // GET: api/Persona
        [HttpGet]
        public IEnumerable<PersonaViewModel> Gets()
        {
            var personas = _personaService.ConsultarTodos().Select(p => new PersonaViewModel(p));
            return personas;
        }

        // GET: api/Persona/5
        [HttpGet("{identificacion}")]
        public ActionResult<PersonaViewModel> Get(string identificacion)
        {
            var persona = _personaService.BuscarxIdentificacion(identificacion);
            if (persona == null) return NotFound();
            var personaViewModel = new PersonaViewModel(persona);
            return personaViewModel;
        }

        // POST: api/Persona
        [HttpPost]
        public ActionResult<PersonaViewModel> Post(PersonaInputModel personaInput)
        {
            Persona persona = MapearPersona(personaInput);
            var response = _personaService.Guardar(persona);
            if (response.Error) 
            {
                return BadRequest(response.Mensaje);
            }
            return Ok(response.Persona);
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


        // DELETE: api/Persona/5
        [HttpDelete("{identificacion}")]
        public ActionResult<string> Delete(string identificacion)
        {
            string mensaje = _personaService.Eliminar(identificacion);
            return Ok(mensaje);
        }

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
        private Persona MapearPersona(PersonaInputModel personaInput)
        {
            var persona = new Persona
            {
                Identificacion = personaInput.Identificacion,
                Nombre = personaInput.Nombre,
                edad = personaInput.edad,
                sexo = personaInput.sexo,
                departamento = personaInput.departamento,
                ciudad = personaInput.ciudad,
                
                /*modalidad = personaInput.modalidad,
                Aporte = personaInput.Aporte,
                fecha = personaInput.fecha*/

            };
            return persona;
        }
        // PUT: api/Persona/5
        [HttpPut("{identificacion}")]
        public ActionResult<string> Put(string identificacion, PersonaInputModel personaInputModel)
        {
            Persona persona = _personaService.BuscarxIdentificacion(identificacion);
            persona = MapearPersona(personaInputModel);
            string mensaje = _personaService.Modificar(persona);
            return Ok(mensaje);
        }
    }
}