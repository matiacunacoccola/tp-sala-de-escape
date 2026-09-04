using Microsoft.AspNetCore.Mvc;
using tp_sala_de_escape.Models;
namespace tp_sala_de_escape.Controllers
{
    public class JuegoController : Controller
    {
        private BD bd = new BD();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ingresar(string nombre)
        {
            bd.CrearPartida(nombre);
            HttpContext.Session.SetString("NombreJugador", nombre);
            HttpContext.Session.SetString("SalaActual", "1");
            return RedirectToAction("Tutorial");
        }
        [HttpGet]
        public IActionResult Tutorial()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Sala(int numero)
        {
            string salaActual= HttpContext.Session.GetString("SalaActual");
            if(numero.ToString()!=salaActual)
            {
                return RedirectToAction("Sala", new{numero = salaActual});
            }
            Sala sala= bd.ObtenerSalaPorNumero(numero);
            return View(sala);
        }
        [HttpPost]
        public IActionResult Resolver(int numero, string respuesta)
        {
            string nombre= HttpContext.Session.GetString("NombreJugador");
            Partida partida= bd.ObtenerPartidaPorNombre(nombre);
            int idPartida= partida.IdPartida;

            Sala sala= bd.ObtenerSalaPorNumero(numero);
            bool correcta= false;
            if(respuesta.ToUpper()==sala.RespuestaCorrecta.ToUpper())
            {
                correcta= true;
            }
            bd.GuardarRespuesta(idPartida, sala.IdSala, respuesta, correcta);
            if(!correcta)
            {
                ViewBag.Error="Respuesta incorrecta, probala de nuevo.";
                return View("Sala", sala);
            }
            int siguiente= numero + 1;
            Sala salaSiguiente= bd.ObtenerSalaPorNumero(siguiente);
            if(salaSiguiente==null)
            {
                bd.FinalizarPartida(idPartida);
                return RedirectToAction("Victoria");
            }
            bd.ActualizarSalaActual(idPartida, siguiente);
            HttpContext.Session.SetString("SalaActual", siguiente.ToString());
            return RedirectToAction("Sala", new{ numero = siguiente});
        }
        [HttpGet]
        public IActionResult Victoria()
        {
            return View();
        }
    }
}