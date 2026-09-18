using Dapper;
using Microsoft.Data.SqlClient;

namespace tp_sala_de_escape.Models
{
    public class BD
    {
        private string _connectionString =@"Server=localhost; Database=salaEscape; Integrated Security=True; TrustServerCertificate=True;";

       

        public Sala ObtenerSalaPorNumero(int numero)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Salas WHERE Numero = @pNumero";
                Sala sala = connection.QueryFirstOrDefault<Sala>(query, new{pNumero = numero});
                return sala;
            }
        }


        public List<Palabra> ObtenerPalabras(int idSala)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Palabras WHERE IdSala = @pIdSala";
                List<Palabra> lista = connection.Query<Palabra>(query, new{pIdSala = idSala}).ToList();
                return lista;
            }
        }
        public void GuardarRespuesta(string idPartida, int idSala, string respuesta, bool correcta)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Respuestas (IdPartida, IdSala, RespuestaIngresada, Correcta, Fecha) VALUES (@pIdPartida, @pIdSala, @pRespuesta, @pCorrecta, GETDATE())";
                connection.Execute(query, new{pIdPartida = idPartida,pIdSala = idSala,pRespuesta = respuesta,pCorrecta = correcta});
            }
        }

        public void ActualizarSalaActual(string idPartida, int numeroSala)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Partidas SET IdSalaActual = @pNumero WHERE IdPartida = @pIdPartida";
                connection.Execute(query, new {pNumero = numeroSala,pIdPartida = idPartida});
            }
        }
        public void FinalizarPartida(string idPartida)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Partidas SET Estado = 'Finalizada', FechaFin = GETDATE() WHERE IdPartida = @pIdPartida";
                connection.Execute(query, new{pIdPartida = idPartida});
            }
        }
        public void CrearPartida(string nombre)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Partidas (NombreParticipante, FechaInicio, Estado, IdSalaActual) VALUES (@pNombre, GETDATE(), 'EnCurso', 1)";
                connection.Execute(query, new{pNombre = nombre});
            }
        }

        public Partida ObtenerPartidaPorNombre(string nombre)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 1 * FROM Partidas WHERE NombreParticipante = @pNombre ORDER BY IdPartida DESC";
                Partida partida = connection.QueryFirstOrDefault<Partida>(query,new{pNombre = nombre});
                return partida;
            }
        }
    }
}