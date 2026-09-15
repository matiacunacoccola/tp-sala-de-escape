namespace tp_sala_de_escape.Models;

public class PalabrasAhorcado
{
    private string palabra;
    public PalabrasAhorcado()
    {
        this.palabra="CARCEL";
    }
    public string obtenerPalabra()
    {
        return palabra;
    }
}