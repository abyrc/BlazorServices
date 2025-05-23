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

        public async Task<bool> DeletePersona(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/person/{id}");
            if (!response.IsSuccessStatusCode) return false;
            var content = await response.Content.ReadFromJsonAsync<bool>();
            return content;
        }

        public async Task<Person?> GetPersona(int id)
        {
            var respuesta = await _httpClient.GetFromJsonAsync<Person>($"/api/person/{id}");
            return respuesta;
        }

        public async Task<List<PersonDTO>> GetPersonas()
        {
            var respuesta = await _httpClient.GetFromJsonAsync<List<PersonDTO>>("/api/person");
            if (respuesta == null) return new List<PersonDTO>();
            return respuesta;
        }

        public async Task<PagedResponse<PersonDTO>> GetPersonasPagination( int pageNumber = 1, int pageSize = 10, string filtro = "")
        {
            string url = $"/api/person/pagination?pageNumber={pageNumber}&pageSize={pageSize}&search={Uri.EscapeDataString(filtro)}";
            var respuesta = await _httpClient.GetFromJsonAsync<PagedResponse<PersonDTO>>(url);
            return respuesta ?? new PagedResponse<PersonDTO>
            {
                TotalRecords = 0,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = new List<PersonDTO>()
            };
        }



        public async Task<PersonDTO?> PostPersona(RegisterDTO persona)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/person", persona);

            if (!response.IsSuccessStatusCode)
                return null;

            var createdPerson = await response.Content.ReadFromJsonAsync<PersonDTO>();
            return createdPerson;
        }

        public async Task<bool> PutPersona(int id, RegisterDTO persona)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/person/{id}", persona);
            if (!response.IsSuccessStatusCode) return false;

            var content = await response.Content.ReadFromJsonAsync<bool>();
            return content;

        }
    }
}
