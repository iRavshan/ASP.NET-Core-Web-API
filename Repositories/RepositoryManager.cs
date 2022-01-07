using Contracts;
using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager, IDisposable
    {
        private readonly AppDbContext dbContext;
        private readonly ILogger _logger;
        public IBookRepository Book { get; private set; }

        public RepositoryManager(AppDbContext dbContext, ILoggerFactory logger)
        {
            this.dbContext = dbContext;
            _logger = logger.CreateLogger("logs");
            Book = new BookRepository(dbContext, _logger);
        }
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
