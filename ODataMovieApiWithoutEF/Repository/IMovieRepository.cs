using ODataMovieApiWithoutEF.Models;

namespace ODataMovieApiWithoutEF.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetMovies();   
        bool MovieExists(int movieId);
        bool Add(Movie movie);
        bool Update(int movieId,Movie movie);
        bool Delete(int movieId);
    }
}
