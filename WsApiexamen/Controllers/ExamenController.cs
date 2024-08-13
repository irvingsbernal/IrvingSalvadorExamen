using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WsApiexamen.Models;
using WsApiexamen.Dtos;
using Microsoft.EntityFrameworkCore;

namespace WsApiexamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly BdiExamenContext _dbContext;

        public ExamenController(BdiExamenContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Consultar")]
        public async Task<IActionResult> ConsultarExamen()
        {
            var responseApi = new ResponseApi<List<ExamenDto>>();
            var listaExamenesDto = new List<ExamenDto>();

            try
            {
                foreach (var item in await _dbContext.TblExamen.ToListAsync())
                {
                    listaExamenesDto.Add(new ExamenDto
                    {
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion
                    });
                }

                responseApi.EstatusProceso = true;
                responseApi.Respuesta = listaExamenesDto;
            }
            catch (Exception ex)
            {
                responseApi.EstatusProceso = false;
                responseApi.MensajeProceso = "Error: " + ex.ToString();
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseApi<ExamenDto>();
            var ExamenDto = new ExamenDto();

            try
            {
                var dbExamen = await _dbContext.TblExamen.FirstOrDefaultAsync(x => x.IdExamen == id);

                if (dbExamen != null)
                {
                    ExamenDto.IdExamen = dbExamen.IdExamen;
                    ExamenDto.Nombre = dbExamen.Nombre;
                    ExamenDto.Descripcion = dbExamen.Descripcion;


                    responseApi.EstatusProceso = true;
                    responseApi.Respuesta = ExamenDto;
                }
                else
                {
                    responseApi.EstatusProceso = false;
                    responseApi.MensajeProceso = "Examen no encontrado.";
                }

            }
            catch (Exception ex)
            {

                responseApi.EstatusProceso = false;
                responseApi.MensajeProceso = ex.Message.ToString();
            }

            return Ok(responseApi);
        }


        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> AgregarExamen(ExamenDto examen)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbExamen = new Examen
                {
                    Nombre = examen.Nombre,
                    Descripcion = examen.Descripcion
                };

                _dbContext.TblExamen.Add(dbExamen);
                await _dbContext.SaveChangesAsync();

                if (dbExamen.IdExamen != 0)
                {
                    responseApi.EstatusProceso = true;
                    responseApi.Respuesta = dbExamen.IdExamen;
                }
                else
                {
                    responseApi.EstatusProceso = false;
                    responseApi.MensajeProceso = "No se guardó el registro del examen.";
                }

                
            }
            catch (Exception ex)
            {
                responseApi.EstatusProceso = false;
                responseApi.MensajeProceso = "Error: " + ex.ToString();
            }

            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public async Task<IActionResult> ActualizarExamen(ExamenDto examen, int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbExamen = await _dbContext.TblExamen.FirstOrDefaultAsync(e => e.IdExamen == id);

                if (dbExamen != null) 
                {
                    dbExamen.Nombre = examen.Nombre;
                    dbExamen.Descripcion = examen.Descripcion;

                    _dbContext.TblExamen.Update(dbExamen);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EstatusProceso = true;
                    responseApi.Respuesta = dbExamen.IdExamen;
                }
                else
                {
                    responseApi.EstatusProceso = false;
                    responseApi.MensajeProceso = "Examen no encontrado.";
                }

            }
            catch (Exception ex)
            {
                responseApi.EstatusProceso = false;
                responseApi.MensajeProceso = "Error: " + ex.ToString();
            }

            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> EliminarExamen(int id)
        {
            var responseApi = new ResponseApi<int>();

            try
            {
                var dbExamen = await _dbContext.TblExamen.FirstOrDefaultAsync(e => e.IdExamen == id);

                if (dbExamen != null)
                {
                    _dbContext.TblExamen.Remove(dbExamen);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EstatusProceso = true;
                }
                else
                {
                    responseApi.EstatusProceso = false;
                    responseApi.MensajeProceso = "Examen no encontrado.";
                }

            }
            catch (Exception ex)
            {
                responseApi.EstatusProceso = false;
                responseApi.MensajeProceso = "Error: " + ex.ToString();
            }

            return Ok(responseApi);
        }

    }
}
