using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPeliculas.Models.Response;
using ApiPeliculas.Models;
using ApiPeliculas.Models.Request;
namespace ApiPeliculas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();


            try
            {
                using (peliculaContext db = new peliculaContext())
                {
                    var lst = db.Movies.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);
        }

        
        [HttpPost]
        public IActionResult Add(MovieRequest Models)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (peliculaContext db = new peliculaContext())
                {
                    Movie Movie = new Movie();
                    Movie.Titulo = Models.Titulo;
                    Movie.Director = Models.Director;
                    Movie.Genero = Models.Genero;
                    Movie.Puntuacion = Models.Puntuacion;
                    Movie.Rating = Models.Rating;
                    Movie.FechaPublicacion = Models.FechaPublicacion;
                    db.Movies.Add(Movie);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        
        //METODO PARA EDITAR
        [HttpPut]
        public IActionResult Editar(MovieRequest Models)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (peliculaContext db = new peliculaContext())
                {

                    //ID para modificar los datos
                    Movie Movie = db.Movies.Find(Models.Id);
                    Movie.Titulo = Models.Titulo;
                    Movie.Director = Models.Director;
                    Movie.Genero = Models.Genero;
                    Movie.Puntuacion = Models.Puntuacion;
                    Movie.Rating = Models.Rating;
                    Movie.FechaPublicacion = Models.FechaPublicacion;
                    //Indica que se modifico
                    db.Entry(Movie).State = Microsoft.EntityFrameworkCore.EntityState.Modified; 
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        //METODO PARA ELIMINAR EL ID
        [HttpDelete("{Id}")]
        public IActionResult Eliminar(int Id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (peliculaContext db = new peliculaContext())
                {

                    //para eliminar una pelicula con el ID
                    Movie Opeli = db.Movies.Find(Id);

                    //
                    //elimina los datos en el Registro
                    db.Remove(Opeli);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        
       
    }
}

