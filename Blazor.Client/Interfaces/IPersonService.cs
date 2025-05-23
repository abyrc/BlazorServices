using BlazorCRUD.Shared;

namespace Blazor.Client.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonDTO>> GetPersonas();
        Task<bool> DeletePersona(int id);
        Task<bool> PutPersona(int id, RegisterDTO persona);
        Task<Person> GetPersona(int id);
        Task<PersonDTO?> PostPersona(RegisterDTO persona);
        Task<PagedResponse<PersonDTO>> GetPersonasPagination(int pageNumber = 1, int pageSize = 10);
    }
}
