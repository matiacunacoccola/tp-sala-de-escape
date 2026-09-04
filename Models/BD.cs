using Dapper;
using Microsoft.Data.SqlClient;
namespace tp_sala_de_escape.Models
{
    public class BD
    {
        private string _connectionString =
            @"Server=localhost; Database= salaEscape; Integrated Security= True; TrustServerCertificate=True;";
        public List<Sala> ObtenerSalas()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Salas ORDER BY Numero";
                List<Sala> lista = connection.Query<Sala>(query).ToList();
                return lista;
            }
        }
        public Sala ObtenerSalaPorNumero(int numero)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query= "SELECT * FROM Salas WHERE Numero = @pNumero";
                Sala sala = connection.QueryFirstOrDefault<Sala>(query, new {pNumero=numero});
                return sala;
            }
        }
        public Sala ObtenerSalaPorId(int idSala)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Salas WHERE IdSala = @pIdSala";
                Sala sala = connection.QueryFirstOrDefault<Sala>(query, new {pIdSala=idSala});
                return sala;
            }
        }
        public void GuardarRespuesta(int idPartida, int idSala, string respuesta, bool correcta)
        {
            using (SqlConnection connection= new SqlConnection(_connectionString))
            {
                string query= "INSERT INTO Respuestas (IdPartida, IdSala, RespuestaIngresada, Correcta, Fecha) VALUES (@pIdPartida, @pIdSala, @pRespuesta, @pCorrecta, GETDATE())";
                connection.Execute(query, new{pIdPartida=idPartida, pIdSala=idSala, pRespuesta=respuesta, pCorrecta=correcta});
            }
        }
        public void ActualizarSalaActual(int idPartida, int numeroSala)
        {
            using (SqlConnection connection= new SqlConnection(_connectionString))
            {
                string query= "UPDATE Partidas SET IdSalaActual = @pNumero WHERE IdPartida = @pIdPartida";
                connection.Execute(query, new {pNumero=numeroSala, pIdPartida=idPartida});
            }
        }
        public void FinalizarPartida(int idPartida)
        {
            using (SqlConnection connection= new SqlConnection(_connectionString))
            {
                string query= "UPDATE Partidas SET Estado = 'Finalizada', FechaFin = GETDATE() WHERE IdPartida = @pIdPartida";
                connection.Execute(query, new {pIdPartida=idPartida});
            }
        }
        public void CrearPartida(string nombre)
        {
             using (SqlConnection connection= new SqlConnection(_connectionString))
            {
                 string query= "INSERT INTO Partidas (NombreParticipante, FechaInicio, Estado, IdSalaActual) VALUES (@pNombre, GETDATE(), 'EnCurso', 1)";
                 connection.Execute(query, new{pNombre=nombre});
            }
        }
        public Partida ObtenerPartidaPorNombre(string nombre)
        {
             using (SqlConnection connection= new SqlConnection(_connectionString))
            {
                string query= "SELECT * FROM Partidas WHERE NombreParticipante = @pNombre";
                Partida partida =connection.QueryFirstOrDefault<Partida>(query, new{pNombre=nombre});
                return partida;
            }
        }
    }
}