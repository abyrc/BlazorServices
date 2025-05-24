using Blazor.Server.UseCases.Interfaces;
using BlazorCRUD.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Blazor.Server.UseCases.Services
{
    public class GeminiUseCase : IGeminiUseCase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiUseCase(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
            _apiKey = configuration["GoogleGemini:ApiKey"]!.Trim();
        }

        public async Task<List<Person>> geminiSeed()
        {
            try
            {
                string endpoint = $"v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";

                var json = $$"""
                            {
                              "contents": [
                                {
                                  "parts": [
                                    {
                                      "text": "Genera un arreglo con 100 objetos que contengan los campos: Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento y Sexo(M o F). No incluyas ninguna explicación ni menciones la palabra JSON en la respuesta. Solo muestra el contenido directamente."
                                    }
                                  ]
                                }
                              ]
                            }
                            """;
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(endpoint, content);

                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                // Aquí debes extraer el JSON con los datos reales, porque la respuesta incluye metadata.
                // Supongamos que el JSON esperado viene en responseString dentro de un campo "candidates"[0]. "content"

                using var doc = JsonDocument.Parse(responseString);

                var root = doc.RootElement;

                if (root.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                {
                    var contentElement = candidates[0].GetProperty("content");

                    // Obtener "parts"
                    var parts = contentElement.GetProperty("parts");

                    if (parts.GetArrayLength() > 0)
                    {
                        // Obtener la propiedad "text" del primer elemento
                        var textJson = parts[0].GetProperty("text").GetString();
                        // Limpia el string
                        textJson = textJson?.Trim().Replace("```", "");

                        if (!string.IsNullOrEmpty(textJson))
                        {
                            // Ahora textJson es un string con el JSON que quieres deserializar
                            // Ejemplo: textJson = "[{ \"Nombre\": \"Maria\", ... }]"

                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            // Deserializar directamente
                            var people = JsonSerializer.Deserialize<List<Person>>(textJson, options);

                            return people ?? new List<Person>();
                        }
                    }
                }

                return new List<Person>();

            }
            catch (Exception ex)
            {
                return new List<Person>();
            }
        }
    }
}
