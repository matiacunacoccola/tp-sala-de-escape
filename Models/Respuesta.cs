namespace tp_sala_de_escape.Models;

public class Respuesta
{
    public int IdRespuesta{get; set;}
    public int IdPartida{get; set;}
    public int IdSala{get;set;}
    public string RespuestaIngresada {get; set;}
    public bool Correcta {get; set;}
    public DateTime Fecha{get;set;}
}