using WsApiexamen.Dtos;
using System.Net.Http.Json;

namespace ExamenBansi.Cliente.Services
{
    public class ExamenService : IExamenService
    {
        private readonly HttpClient _http;

        public ExamenService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ExamenDto>> Consultar()
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<List<ExamenDto>>>("api/Examen/Consultar");

            if (result!.EstatusProceso)
                return result.Respuesta!;
            else
                throw new Exception(result.MensajeProceso);
        }

        public async Task<ExamenDto> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseApi<ExamenDto>>($"api/Examen/Buscar/{id}");

            if (result!.EstatusProceso)
                return result.Respuesta!;
            else
                throw new Exception(result.MensajeProceso);
        }

        public async Task<int> Agregar(ExamenDto examen)
        {
            var result = await _http.PostAsJsonAsync("api/Examen/Agregar", examen);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EstatusProceso)
                return response.Respuesta!;
            else
                throw new Exception(response.MensajeProceso);
        }

        public async Task<int> Actualizar(ExamenDto examen)
        {
            var result = await _http.PutAsJsonAsync($"api/Examen/Actualizar/{examen.IdExamen}", examen);
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EstatusProceso)
                return response.Respuesta!;
            else
                throw new Exception(response.MensajeProceso);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Examen/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseApi<int>>();

            if (response!.EstatusProceso)
                return response.EstatusProceso!;
            else
                throw new Exception(response.MensajeProceso);
        }
    }
}
