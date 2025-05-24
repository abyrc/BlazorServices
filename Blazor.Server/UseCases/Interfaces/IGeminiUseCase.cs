using BlazorCRUD.Shared;

namespace Blazor.Server.UseCases.Interfaces
{
    public interface IGeminiUseCase
    {
        Task<List<Person>> geminiSeed();
    }
}
