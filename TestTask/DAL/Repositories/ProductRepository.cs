using TestTask.DAL.Interfaces;
using TestTask.Entities;

namespace TestTask.DAL.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(Product entity)
        {
            await _dbContext.Products.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

               
        public async Task DeleteAsync(Product entity)
        {
            _dbContext.Products.Remove(entity);

            await _dbContext.SaveChangesAsync();    
        }

        public IQueryable<Product> GetAll()
        {
            return _dbContext.Products.AsQueryable();   
        }

        public async Task UpdateAsync(Product entity)
        {
            _dbContext.Products.Update(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
