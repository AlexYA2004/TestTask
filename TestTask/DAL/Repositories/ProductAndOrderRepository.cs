 using TestTask.DAL.Interfaces;
using TestTask.Entities;

namespace TestTask.DAL.Repositories
{
    public class ProductAndOrderRepository : IBaseRepository<ProductAndOrder>
    {
        private AppDbContext _dbContext;

        public ProductAndOrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task CreateAsync(ProductAndOrder entity)
        {     
            await _dbContext.ProductsAndOrders.AddAsync(entity);

            await _dbContext.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(ProductAndOrder entity)
        {
            _dbContext.ProductsAndOrders.Remove(entity);

            await _dbContext.SaveChangesAsync();    
        }

        public IQueryable<ProductAndOrder> GetAll()
        {
            return _dbContext.ProductsAndOrders.AsQueryable();
        }

        public async Task UpdateAsync(ProductAndOrder entity)
        {
            _dbContext.ProductsAndOrders.Update(entity);

             await _dbContext.SaveChangesAsync();
        }
    }
}
