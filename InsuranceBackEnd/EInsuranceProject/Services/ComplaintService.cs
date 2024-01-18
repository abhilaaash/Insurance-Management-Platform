using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EInsuranceProject.Services
{
    public class ComplaintService : IComplaintService
    {
        private IEntityRepository<Complaint> _repository;

        public ComplaintService(IEntityRepository<Complaint> repository)
        {
            _repository = repository;
        }
        public async Task AddComplaint(Complaint complaint)
        {
          await _repository.Insert(complaint);
        }

       

        public async Task<bool> DeleteComplaint(int id)
        {

            var ComplaintToDelete = await _repository.GetById(id);
            if (ComplaintToDelete != null)
            {
                return await _repository.Delete(id);
            }
            throw new DataNotFoundExcpetion("No complaint Data found");
        }

        public async Task<IEnumerable<Complaint>> GetAll(string[] innerTables)
        {
            Expression<Func<Complaint, bool>> filterPredicate = e => e.Status == true;
            var complaints = await _repository.GetAll(innerTables, filterPredicate);
            if (complaints.Any())
            {
                return complaints;
            }
            throw new DataNotFoundExcpetion("No complaint Data found");
        }

        public async Task<IEnumerable<Complaint>> GetById(int Id)
        {
           
            Expression<Func<Complaint, bool>> filterPredicate = e => e.CustomerId == Id;
            var complaint = await _repository.GetAsync(filterPredicate);
            if (complaint != null)
            {
                return complaint;
            }
            throw new DataNotFoundExcpetion("No complaint Data found");
        }

        

        public async Task<bool> UpdateComplaint(Complaint complaint)
        {
            var ComplaintToUpdate = await _repository.GetById(complaint.ComplaintId);
            if (ComplaintToUpdate != null)
            {
                return await _repository.Update(complaint, complaint.ComplaintId);
            }
            throw new DataNotFoundExcpetion("No complaint Data found");
        }
    }
}
