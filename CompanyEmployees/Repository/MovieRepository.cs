using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateMovie(Movie movie) =>
            Create(movie);

        public void DeleteMovie(Movie movie) =>
            Delete(movie);

        public IEnumerable<Movie> GetAllMovies(bool trackChanges) =>
           FindAll(trackChanges)
           .OrderBy(c => c.YearPublished)
           .ToList();

        public Movie GetMovieById(Guid id, bool trackChanges) =>
            FindByCondition(x => x.Id == id, trackChanges).FirstOrDefault() ?? new Movie();

        public void UpdateMovie(Movie movie) =>
            Update(movie);
    }
}
