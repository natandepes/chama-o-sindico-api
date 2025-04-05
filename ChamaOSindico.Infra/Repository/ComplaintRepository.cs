using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class ComplaintRepository
    {

        private readonly IMongoCollection<Complaint> _context;

        public ComplaintRepository(MongoAppDbContext context)
        {
            _context = context.GetCollection<Complaint>();
        }

        public async Task<List<Complaint>> GetAllComplaints()
        {
            return await _context.Find(_ => true).ToListAsync();
        }

        public async Task<Complaint> GetComplaintById(string idComplaint)
        {
            return await _context.Find(c => c.Id == idComplaint).FirstOrDefaultAsync();
        }

        public async Task CreateComplaint(Complaint complaint)
        {
            await _context.InsertOneAsync(complaint);
        }

        public async Task UpdateComplaitAsync(string idCcomplaint, Complaint complaint)
        {
            await _context.ReplaceOneAsync(c => c.Id == idCcomplaint, complaint);
        }

        public async Task DeleteComplaintAsync(string complaint)
        {
            await _context.DeleteOneAsync(c => c.Id == complaint);
        }
    }
}
