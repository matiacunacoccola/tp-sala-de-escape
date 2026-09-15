namespace tp_sala_de_escape.Models;

public class Partida
{
    public int IdPartida{get;set;}
    public string NombreParticipante{get;set;}
    public DateTime FechaInicio{get;set;}
    public DateTime FechaFin{get;set;}
    public string Estado{get;set;}
    public int IdSalaActual {get;set;}
}