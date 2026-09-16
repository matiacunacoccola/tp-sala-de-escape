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
            Partida partida = bd.ObtenerPartidaPorNombre(nombre);

            HttpContext.Session.SetString("NombreParticipante",nombre);
            HttpContext.Session.SetString("PartidaId", partida.IdPartida.ToString());
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
            string nombre = HttpContext.Session.GetString("NombreParticipante");
            if(nombre == null)
            {
                return RedirectToAction("Index");
            }
            Partida partida = bd.ObtenerPartidaPorNombre(nombre);
            if(partida == null)
            {
                return RedirectToAction("Index");
            }


            if(numero != partida.IdSalaActual)
            {
                return RedirectToAction("Sala",new{numero = partida.IdSalaActual});
            }
            Sala sala= bd.ObtenerSalaPorNumero(numero);

            if(sala== null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.SalaNumero = numero;

            if(numero== 1)
            {
                List<Palabra> palabras = bd.ObtenerPalabras(sala.IdSala);
                ViewBag.Palabras= palabras;
            }

            if(numero == 2)
            {
                PalabrasAhorcado palabrasAhorcado = new PalabrasAhorcado();
                ViewBag.palabra = palabrasAhorcado.obtenerPalabra();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Resolver(int numero, string respuesta)
        {
            string nombre = HttpContext.Session.GetString("NombreParticipante");
            string idPartida= HttpContext.Session.GetString("PartidaId");

            Partida partida= bd.ObtenerPartidaPorNombre(nombre);

            if(partida == null)
            {
                return RedirectToAction("Index");
            }

            Sala sala = bd.ObtenerSalaPorNumero(numero);
            bool correcta = false;

            if(respuesta.ToUpper()== sala.RespuestaCorrecta.ToUpper())
            {
                correcta = true;
            }

            bd.GuardarRespuesta(idPartida,sala.IdSala,respuesta, correcta);

            if(!correcta)
            {
                ViewBag.Error = "Respuesta incorrecta, probala de nuevo.";
                ViewBag.SalaNumero = numero;

                if(numero == 1)
                {
                    List<Palabra> palabras = bd.ObtenerPalabras(sala.IdSala);
                    ViewBag.Palabras= palabras;
                }

                return View("Sala");
            }

            if(numero == 4)
            {
                bd.FinalizarPartida(idPartida);
                return RedirectToAction("Victoria");
            }

            int siguiente= numero + 1;

            Sala salaSiguiente= bd.ObtenerSalaPorNumero(siguiente);

            if(salaSiguiente == null)
            {
                bd.FinalizarPartida(idPartida);
                return RedirectToAction("Victoria");
            }

            bd.ActualizarSalaActual(idPartida, siguiente);
            HttpContext.Session.SetString("SalaActual", siguiente.ToString());

            return RedirectToAction("Sala",new{numero= siguiente});
        }

        [HttpPost]
        public IActionResult ResolverOrdenar(int numero, string respuesta1, string respuesta2, string respuesta3, string respuesta4, string respuesta5, string respuesta6, string respuesta7, string respuesta8)
        {
            string nombre = HttpContext.Session.GetString("NombreParticipante");
            string idPartida = HttpContext.Session.GetString("PartidaId");

            Sala sala = bd.ObtenerSalaPorNumero(numero);
            List<Palabra> palabras = bd.ObtenerPalabras(sala.IdSala);

            string[] respuestas ={respuesta1, respuesta2, respuesta3, respuesta4, respuesta5, respuesta6, respuesta7, respuesta8};

            bool correcta = true;

            for(int i= 0; i<8; i++)
            {
                if(respuestas[i].ToUpper() != palabras[i].PalabraCorrecta.ToUpper())
                {
                    correcta = false;
                }
            }

            bd.GuardarRespuesta(idPartida,sala.IdSala,"ORDENAR PALABRAS", correcta);

            if(!correcta)
            {
                ViewBag.Error= "Hay alguna palabra incorrecta. Revisalas y proba de nuevo.";
                ViewBag.Palabras= palabras;
                ViewBag.SalaNumero= numero;
                return View("Sala");
            }

            bd.ActualizarSalaActual(idPartida,2);
            HttpContext.Session.SetString("SalaActual","2");

            return RedirectToAction("Sala",new{numero = 2});
        }

        [HttpGet]
        public IActionResult ResolverAhorcado()
        {
            string nombre= HttpContext.Session.GetString("NombreParticipante");
            string idPartida= HttpContext.Session.GetString("PartidaId");

            Partida partida = bd.ObtenerPartidaPorNombre(nombre);

            if(partida == null)
            {
                return RedirectToAction("Index");
            }
            Sala sala = bd.ObtenerSalaPorNumero(2);

            bd.GuardarRespuesta(idPartida,sala.IdSala,"CARCEL",true);
            bd.ActualizarSalaActual(idPartida,3);
            HttpContext.Session.SetString("SalaActual","3");

            return RedirectToAction("Sala",new{numero= 3});
        }

        [HttpGet]
        public IActionResult Victoria()
        {
            string nombre = HttpContext.Session.GetString("NombreParticipante");

            Partida partida = bd.ObtenerPartidaPorNombre(nombre);

            ViewBag.FechaInicio= partida.FechaInicio;
            ViewBag.FechaFin= partida.FechaFin;

            return View();
        }
    }
}