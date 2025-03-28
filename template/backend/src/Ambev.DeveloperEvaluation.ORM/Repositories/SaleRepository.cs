using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    using global::Ambev.DeveloperEvaluation.Domain.Entities;
    using global::Ambev.DeveloperEvaluation.Domain.Repositories;
    using Microsoft.EntityFrameworkCore;
    
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _dbContext;

        public SaleRepository(DefaultContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Sale sale)
        {
            await _dbContext.Set<Sale>().AddAsync(sale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid saleId)
        {
            return await _dbContext.Set<Sale>()
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == saleId);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync(int page, int size)
        {
            return await _dbContext.Set<Sale>()
                .Include(s => s.Items)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _dbContext.Set<Sale>().Update(sale);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _dbContext.Sales.CountAsync();
        }
    }
}
