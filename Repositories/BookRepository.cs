using Contracts;
using Entities;
using Entities.Models.Book;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public override async Task<IEnumerable<Book>> GetAll()
        {
            try
            {
                return await dbset.ToListAsync();
            }

            catch(Exception ex)
            {
                logger.LogError(ex, "GetAll method failed", typeof(BookRepository));
                return new List<Book>();
            }
            
        }

        public override async Task<Book> GetById(Guid id)
        {
            try
            {
                return await dbset.FindAsync(id);
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "GetById method failed", typeof(BookRepository));
                return new Book();
            }
        }

        public override async Task<bool> Create(Book entity)
        {
            try
            {

                await dbset.AddAsync(entity);
                return true;
            }

            catch(Exception ex)
            {
                logger.LogError(ex, "Create method failed", typeof(BookRepository));
                return false;
            }
        }

        public override async Task<bool> Update(Book entity)
        {
            try
            {
                var obj = await dbset.Where(w => w.Id.Equals(entity.Id)).FirstOrDefaultAsync();
                if (obj == null)
                {
                    await dbset.AddAsync(entity);
                }

                obj.Name = entity.Name;
                obj.Author = entity.Author;
                obj.Year = entity.Year;
                return true;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Update method failed", typeof(BookRepository));
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
            catch(Exception ex)
            {
                logger.LogError(ex, "Delete method failed", typeof(BookRepository));
                return false;
            }
        }
        
    }
}
