namespace Contracts
{
    public interface IRepositoryManager
    {
        IMovieRepository Movie { get; }
        void Save();
    }
}
