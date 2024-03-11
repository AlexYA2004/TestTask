using Microsoft.EntityFrameworkCore;
using TestTask.DAL.Interfaces;
using TestTask.Entities;

namespace TestTask.DAL.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        } 
        public async Task CreateAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _dbContext.Users.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<User> GetAll()
        {
            return _dbContext.Users.AsQueryable();
        }

        public async Task UpdateAsync(User entity)
        {
            _dbContext.Users.Update(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
