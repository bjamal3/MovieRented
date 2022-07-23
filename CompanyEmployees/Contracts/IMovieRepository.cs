using Entities.Models;

namespace Contracts
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies(bool trackChanges);
        Movie GetMovieById(Guid id, bool trackChanges);
        void CreateMovie(Movie movie);
        void DeleteMovie(Movie movie);
        void UpdateMovie(Movie movie);
    }
}
