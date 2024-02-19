using TestTask.DAL.Interfaces;
using TestTask.Entities;

namespace TestTask.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Order entity)
        {
            await _dbContext.Orders.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order entity)
        {
            _dbContext.Orders.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return _dbContext.Orders.AsQueryable();    
        }

        public async Task UpdateAsync(Order entity)
        {
            _dbContext.Orders.Update(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
