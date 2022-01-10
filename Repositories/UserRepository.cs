using Contracts;
using Entities;
using Entities.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await dbset.ToListAsync();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "GetAll method failed", typeof(UserRepository));
                return new List<User>();
            }

        }

        public override async Task<User> GetById(Guid id)
        {
            try
            {
                return await dbset.FindAsync(id);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "GetById method failed", typeof(UserRepository));
                return new User();
            }
        }

        public override async Task<bool> Create(User entity)
        {
            try
            {

                await dbset.AddAsync(entity);
                return true;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Create method failed", typeof(UserRepository));
                return false;
            }
        }

        public override async Task<bool> Update(User entity)
        {
            try
            {
                var obj = await dbset.Where(w => w.Id.Equals(entity.Id)).FirstOrDefaultAsync();
                if (obj == null)
                {
                    await dbset.AddAsync(entity);
                }

                obj.Username = entity.Username;
                obj.Email = entity.Email;
                obj.Password = entity.Password;
                obj.UserRole = entity.UserRole;
                return true;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Update method failed", typeof(UserRepository));
                return false;
            }
        }
        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var obj = await dbset.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
                if (obj == null) return false;

                dbset.Remove(obj);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete method failed", typeof(UserRepository));
                return false;
            }
        }

    }
}
