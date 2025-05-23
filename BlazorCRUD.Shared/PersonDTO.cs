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
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
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
}
