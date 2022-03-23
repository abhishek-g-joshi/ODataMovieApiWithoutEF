using ODataMovieApiWithoutEF.Models;
using System.Data;
using System.Data.SqlClient;

namespace ODataMovieApiWithoutEF.Repository
{
    public class MovieRepository : IMovieRepository
    {
        string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Database=MovieApi;Trusted_Connection=True;MultipleActiveResultSets=true";
        

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
                                                Director = Convert.ToString(dr[4]),
                        });
                    }
                }
                
            }
            return movies;
        }
        
        //Post
        public bool Add(Movie movie)
        {
            string query = "insert into Movies values('"+ movie.Id + "','" + movie.Title + "','" + movie.Genre + "', '" + movie.ReleaseDate + "', '" + movie.Director + "')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query))
                {
                    command.Connection = connection;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int i = command.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        //update
        public bool Update(int id, Movie movie)
        {
            string query = "update Movies set Title= '" + movie.Title + "', Genre='" + movie.Genre + "', ReleaseDate='" + movie.ReleaseDate + "', Diector='" + movie.Director + "' where Id='" + id + "' ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query))
                {
                    command.Connection = connection;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int i = command.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        //Delete Movie object
        public bool Delete(int movieId)
        {
            string query = "delete Movies where Id='" + movieId + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query))
                {
                    command.Connection = connection;
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    int i = command.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool MovieExists(int movieId)
        {
            var result = GetMovies().Where(x => x.Id == movieId);
            if(result.Count() > 0)
                return true;
            else
                return false;
          //  return (result == null) ? false : true;
        }
    }
}
