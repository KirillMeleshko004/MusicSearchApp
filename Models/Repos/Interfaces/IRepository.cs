namespace MusicSearchApp.Models.Repos.Interfaces
{
    public interface IRepository <T, I>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T?> GetByIdAsync(I id);
        Task<T?> DeleteAsync(I id);
        Task<T?> ChangeAsync(T data, I id);
        Task<bool> AddAsync(T data);
    }
}