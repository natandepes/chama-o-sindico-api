using ChamaOSindico.Domain.Entities;
using ChamaOSindico.Domain.Interfaces;
using ChamaOSindico.Infra.Context;
using MongoDB.Driver;

namespace ChamaOSindico.Infra.Repository
{
    public class ComplaintRepository : IComplaintRepository
    {

        private readonly IMongoCollection<Complaint> _context;
        private readonly IResidentRepository _residentRepository;

        public ComplaintRepository(
            MongoAppDbContext context,
            IResidentRepository residentRepository)
        {
            _context = context.GetCollection<Complaint>();
            _residentRepository = residentRepository;
        }

        public async Task<List<Complaint>> GetAllComplaintsAsync()
        {
            var allComplaints = _context.Find(_ => true).ToListAsync();
            return await _context.Find(_ => true).ToListAsync();
        }

        public async Task<List<Complaint>> GetAllComplaintsAsyncByUserId(string idUser)
        {
            return await _context.Find(c => c.CreatedByUserId == idUser).ToListAsync();
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
