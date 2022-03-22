using ODataMovieApiWithoutEF.Models;
using System.Data;
using System.Data.SqlClient;

namespace ODataMovieApiWithoutEF.Repository
{
    public class MovieRepository : IMovieRepository
    {
        string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Database=MovieApi;Trusted_Connection=True;MultipleActiveResultSets=true";
        public bool Add(Movie movie)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int movieId)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            string query = "Select * from Movies";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query))
                {

                    command.Connection = connection;
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        movies.Add(new Movie { Id = Convert.ToInt32(dr[0]),
                                                Title = Convert.ToString(dr[1]),
                                                Genre = Convert.ToString(dr[2]),
                                                ReleaseDate = Convert.ToDateTime(dr[3]),
                                                Diector = Convert.ToString(dr[4]),
                        });
                    }
                }
                
            }
            return movies;
        }

        public bool Update(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
