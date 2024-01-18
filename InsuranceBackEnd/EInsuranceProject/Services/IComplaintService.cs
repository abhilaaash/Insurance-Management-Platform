using EInsuranceProject.Model;

namespace EInsuranceProject.Services
{
    public interface IComplaintService
    {
        public Task<IEnumerable<Complaint>> GetAll(string[] innerTables);
        public Task<IEnumerable<Complaint>> GetById(int Id);
        public Task AddComplaint(Complaint complaint);
        public Task<bool> UpdateComplaint(Complaint complaint);
        public Task<bool> DeleteComplaint(int id);
    }
}
