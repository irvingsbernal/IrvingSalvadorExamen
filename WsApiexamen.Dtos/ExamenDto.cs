namespace WsApiexamen.Dtos
{
    using System.ComponentModel.DataAnnotations;
    public class ExamenDto
    {
        public int IdExamen { get; set; }
        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }
    }
}