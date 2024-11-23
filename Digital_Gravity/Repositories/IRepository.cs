using Digital_Gravity.ViewModels;

namespace Digital_Gravity.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<PaginatedResponse<UserSubscriptionViewModel>> GetUserSubscriptionsAsync(int userId, string? searchTerm, int pageNumber, int pageSize);
    }
}
