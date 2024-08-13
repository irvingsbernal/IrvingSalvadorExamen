using WsApiexamen.Dtos;

namespace ExamenBansi.Cliente.Services
{
    public interface IExamenService
    {
        Task<List<ExamenDto>> Consultar();
        Task<ExamenDto> Buscar(int id);
        Task<int> Agregar(ExamenDto examen);
        Task<int> Actualizar(ExamenDto examen);
        Task<bool> Eliminar(int id);
    }
}
