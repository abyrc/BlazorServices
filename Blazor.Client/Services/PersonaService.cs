using Blazor.Client.Interfaces;
using BlazorCRUD.Shared;
using System.Net.Http.Json;

namespace Blazor.Client.Services
{
    public class PersonaService : IPersonService

    { 
        private readonly HttpClient _httpClient;


        public PersonaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PersonDTO>> GetPersonas()
        {
            var respuesta = await _httpClient.GetFromJsonAsync<List<PersonDTO>>("/api/person");
            if (respuesta == null) return new List<PersonDTO>();
            return respuesta;
        }
    }
}
