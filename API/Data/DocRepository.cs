using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DocRepository : IDocRepository
    {
        private readonly DataContext _context;
        public DocRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Docs> GetPresDocByIdAsync(int Id)
        {
            return await _context.Documents
                .Include(d => d.Prescriptions)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Docs> GetPayDocByIdAsync(int Id)
        {
            return await _context.Documents
                .Include(s => s.ScheduleOfPayment)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }
    }
}