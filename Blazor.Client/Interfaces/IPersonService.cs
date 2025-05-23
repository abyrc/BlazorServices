using BlazorCRUD.Shared;

namespace Blazor.Client.Interfaces
{
    public interface IPersonService
    {
        Task<List<PersonDTO>> GetPersonas();
    }
}
