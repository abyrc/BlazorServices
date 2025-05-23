using System.ComponentModel.DataAnnotations;

namespace BlazorCRUD.Shared
{
    public class PersonDTO
    {
        public int ID { get; set; }
        public string nombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }
    }

    public class RegisterDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre debe tener máximo 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido paterno debe tener máximo 100 caracteres")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El apellido materno es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido materno debe tener máximo 100 caracteres")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [RegularExpression("[MF]", ErrorMessage = "Sexo debe ser 'M' o 'F'")]
        public char Sexo { get; set; }
    }

    public class Person
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public char Sexo { get; set; }
    }

    public class PagedResponse<T>
    {
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> Data { get; set; }
    }

}
