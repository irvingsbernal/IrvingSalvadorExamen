namespace WsApiexamen.Dtos
{
    public class ResponseApi<T>
    {
        public bool EstatusProceso {  get; set; }
        public T? Respuesta {  get; set; }
        public string? MensajeProceso { get; set; }
}
}
