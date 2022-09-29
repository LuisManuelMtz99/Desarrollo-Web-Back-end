using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationBackEnd.Entidades
{
    public class Videojuego
    {
        public int id { get; set; }
        [Required (ErrorMessage ="El campo Nombre es requerido")]
        [StringLength(maximumLength:15, ErrorMessage = "El campo nombre solo puede tener un maximo de 15 caracteres")]

        public string nombre { get; set; }

        [Range(1952,2022, ErrorMessage ="El año no se encuentra entre el rango 1952 y 2022")]
        [NotMapped]
        public int año { get; set; }

        [CreditCard]
        [NotMapped]
        public string tarjeta { get; set; }

        [Url]
        [NotMapped]
        public string URL  { get; set; }    

        public List<Datos> datos { get; set; }

    }
}
