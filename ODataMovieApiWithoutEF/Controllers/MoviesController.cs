using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataMovieApiWithoutEF.Models;
using ODataMovieApiWithoutEF.Repository;

namespace ODataMovieApiWithoutEF.Controllers
{
    public class MoviesController:ODataController
    {
       MovieRepository _movieRepo = new MovieRepository();
        
        /// <summary>
        /// Get list of all Movies
        /// </summary>
        [EnableQuery]
        public IEnumerable<Movie> Get()
        {
            return _movieRepo.GetMovies().ToList();
        }

        [EnableQuery]
        [HttpGet("v1/Movies/{Id}")]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IEnumerable<Movie> GetById(int Id)
        {
            if(Id < 0)
            {
                return (IEnumerable<Movie>)BadRequest();
            }
            var movie = _movieRepo.GetMovies().Where(model => model.Id == Id);

            if (movie == null)
            {
                ModelState.AddModelError("", $"Movie with Id: {Id} is not available in DB");
                return (IEnumerable<Movie>)StatusCode(404, ModelState);
            }

            return movie;
        }

        /// <summary>
        /// Create a new movie
        /// </summary>
        /// <param name="movie"></param>
        [EnableQuery]
        [ProducesResponseType(201, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Movie movie)
        {
            if(movie == null)
            {
                return BadRequest(ModelState);
            }

            if (_movieRepo.MovieExists(movie.Id))
            {
                ModelState.AddModelError("", "Movie already Exist");
                return StatusCode(500, ModelState);
            }

            if (!_movieRepo.Add(movie))
            {
                ModelState.AddModelError("", $"Something went wrong while saving movie record of {movie.Title}");
                return StatusCode(500, ModelState);
            }
            
            return Created(movie);
            
        }

        /// <summary>
        /// Update a movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        [HttpPut("v1/Movies/{id}")]
        [EnableQuery]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {
            if(movie == null || id != movie.Id)
            {
                return BadRequest(ModelState);
            }

            if(!_movieRepo.Update(id, movie))
            {
                ModelState.AddModelError("", $"Something went wrong while updating movie : {movie.Title}");
                return StatusCode(500, ModelState);
            }

            return Updated(movie);
        }


        /// <summary>
        /// Delete a movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpDelete("v1/Movies/{movieId}")]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int movieId)
        {
            if (movieId < 0)
            {
                return BadRequest();
            }
            if(!_movieRepo.MovieExists(movieId))
            {
                ModelState.AddModelError("", "Movie to be deleted doesn't Exist");
                return StatusCode(500, ModelState);
            }
            if(!_movieRepo.Delete(movieId))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting movie");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

    }
}
