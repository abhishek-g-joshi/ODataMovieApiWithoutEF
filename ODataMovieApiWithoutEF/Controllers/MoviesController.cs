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
        [HttpGet(nameof(GetById))]
        [ProducesResponseType(200, Type = typeof(Movie))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IEnumerable<Movie> GetById(int Id)
        {
            var result = _movieRepo.GetMovies().Where(model => model.Id == Id);

            if (result == null)
            {
                return (IEnumerable<Movie>)NotFound();
            }

            return result;
        }

        //Post
        public void Post([FromBody] Movie movie)
        {
            if (ModelState.IsValid == true)
            {
                _movieRepo.Add(movie);
            }
            else
            {

            }
        }

        [HttpPut("{id}")]
        [EnableQuery]
        public void Put(int id, [FromBody] Movie movie)
        {
            if (ModelState.IsValid == true)
            {
                _movieRepo.Edit(id, movie);
            }
        }


        //Delete 
        [HttpDelete("{movieId}")]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public void Delete(int movieId)
        {
            if (ModelState.IsValid == true)
            {
                _movieRepo.Delete(movieId);
            }
        }

    }
}
