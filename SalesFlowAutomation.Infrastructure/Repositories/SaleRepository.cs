
using Microsoft.EntityFrameworkCore;
using SalesFlowAutomation.Application.Sales.Interfaces;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Infrastructure.Persistence;

namespace SalesFlowAutomation.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            var sales = await _context.Sales
                .Include(s => s.Details)
                .Include(s => s.Payment)
                .ToListAsync();

            return sales;
        }

        public async Task<Sale?> GetByIdAsync(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.Details)
                .Include(s => s.Payment)
                .FirstOrDefaultAsync(s => s.Id == id);

            return sale;
        }
    }
}
