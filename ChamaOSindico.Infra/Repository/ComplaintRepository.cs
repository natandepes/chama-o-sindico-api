using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {

        private readonly IMongoCollection<Complaint> _context;

        public ComplaintRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<Complaint>();
        }

        public async Task<List<Complaint>> GetAllComplaintsAsync()
        {
            return await _context.Find(_ => true).ToListAsync();
        }

        public async Task<Complaint> GetComplaintByIdAsync(string idComplaint)
        {
            return await _context.Find(c => c.Id == idComplaint).FirstOrDefaultAsync();
        }

        public async Task CreateComplaintAsync(Complaint complaint)
        {
            await _context.InsertOneAsync(complaint);
        }

        public async Task UpdateComplaintAsync(string idCcomplaint, Complaint complaint)
        {
            await _context.ReplaceOneAsync(c => c.Id == idCcomplaint, complaint);
        }

        public async Task DeleteComplaintAsync(string complaint)
        {
            await _context.DeleteOneAsync(c => c.Id == complaint);
        }
    }
}
