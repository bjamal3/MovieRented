using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IMovieRepository? _movieRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IMovieRepository Movie
        {
            get
            {
                if (_movieRepository == null)
                    _movieRepository = new MovieRepository(_repositoryContext);

                return _movieRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}