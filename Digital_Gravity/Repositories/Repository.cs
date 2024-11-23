using Dapper;
using Digital_Gravity.DBContext;
using Digital_Gravity.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;

namespace Digital_Gravity.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public Repository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity) 
        { 
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        { 
            var entity = await GetByIdAsync(id);
            _context.Remove(entity); 
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<UserSubscriptionViewModel>> GetUserSubscriptionsAsync(
        int userId,
        string searchTerm,
        int pageNumber,
        int pageSize)
        {
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        var refCursorName = "result_cursor";

                        // Step 1: Call the stored procedure
                        //    var callProcedureQuery = @"
                        // Build the SQL query string with parameters interpolated directly
                        var callProcedureQuery = $@"
                    CALL public.get_user_subscriptions(
                        {userId}, 
                        '{searchTerm?.Replace("'", "''") ?? string.Empty}', 
                        {pageNumber}, 
                        {pageSize}, 
                        '{refCursorName}'
                    );";

                        // Execute the CALL statement
                        await connection.ExecuteAsync(callProcedureQuery, transaction: transaction);


                        // Step 3: Fetch the cursor data
                        var fetchCursorQuery = $"FETCH ALL IN \"{refCursorName}\";";

                        var subscriptions = await connection.QueryAsync<UserSubscriptionViewModel>(
                            fetchCursorQuery,
                            transaction: transaction);

                        transaction.Commit();

                        // Step 3: Return paged result
                        var totalCount = subscriptions.Count();
                        return new PaginatedResponse<UserSubscriptionViewModel>(
                        subscriptions.ToList(),
                        totalCount,
                        pageNumber,
                        pageSize
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                throw;
            }
        }

    }
}
