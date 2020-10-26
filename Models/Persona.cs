using Entidad;

namespace pulsacionesdotnet.Models
{
    public class PersonaInputModel
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string sexo { get; set; }
        public int edad { get; set; }
        public string departamento { get; set; }
        public string ciudad { get; set; }

    }

    public class PersonaViewModel : PersonaInputModel
    {
        public PersonaViewModel()
        {

        }
        public PersonaViewModel(Persona persona)
        {
            Identificacion = persona.Identificacion;
            Nombre = persona.Nombre;
            edad = persona.edad;
            sexo = persona.sexo;
            departamento = persona.departamento;
            ciudad = persona.ciudad;
           

        }
    }
}