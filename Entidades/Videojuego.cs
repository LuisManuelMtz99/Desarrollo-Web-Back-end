using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationBackEnd.Validaciones;

namespace WebApplicationBackEnd.Entidades
{
    public class Videojuego
    {
        public int id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo nombre solo puede tener un maximo de 15 caracteres")]
        //[PrimeraLetra]
        public string nombre { get; set; }

        [Range(1952, 2022, ErrorMessage = "El año no se encuentra entre el rango 1952 y 2022")]
        [NotMapped]
        public int año { get; set; }

        [CreditCard]
        [NotMapped]
        public string Tarjeta { get; set; }

        [Url]
        [NotMapped]
        public string URL { get; set; }

        public List<Datos> datos { get; set; }

        [NotMapped]
        public int Menor { get; set; }

        [NotMapped]
        public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Para que se ejecuten debe de primero cumplirse con las reglas por Atributo Ejemplo: Range
            // Tomar a consideración que primero se ejecutaran las validaciones mappeadas en los atributos
            // y posteriormente las declaradas en la entidad
            if (!string.IsNullOrEmpty(nombre))
            {
                var primeraLetra = nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayusculA",
                        new string[] { nameof(nombre) });
                }
            }

            if (Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el campo Mayor",
                    new string[] { nameof(Menor) });
            }
        }
    }
}

