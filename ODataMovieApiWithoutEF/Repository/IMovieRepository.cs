using ODataMovieApiWithoutEF.Models;

namespace ODataMovieApiWithoutEF.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetMovies();    
        bool Add(Movie movie);
        bool Update(int id, Movie movie);
        bool Delete(int movieId);
    }
}
