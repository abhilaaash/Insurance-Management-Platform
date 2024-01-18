using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class ClaimService : IClaimService
    {
        private IEntityRepository<Claim> _repository;
        private IClaimRepository _claimRepository;
        public ClaimService(IEntityRepository<Claim> repository, IClaimRepository claimRepository)
        {
            _repository = repository;
            _claimRepository = claimRepository;
        }
        public async Task AddClaim(Claim claim)
        {
            await _repository.Insert(claim);
        }

        public async Task<bool> DeleteClaim(int id)
        {
            var ClaimToDelete = await _repository.GetById(id);
            if (ClaimToDelete != null)
                return await _repository.Delete(id);
            throw new DataNotFoundExcpetion("No Claim Data found");
        }

        public async Task<IEnumerable<Claim>> GetAll(string[] innerTables)
        {
            Expression<Func<Claim, bool>> filterPredicate = e => e.CustomerId!=null;
            var claims = await _repository.GetAll(innerTables, filterPredicate);
            if (claims.Any())
            {
                return claims;
            }
            throw new DataNotFoundExcpetion("No Claim Data found");
        }

        public async Task<List<Claim>> GetById(int Id)
        {
            string[] innerTable = { };
            Expression<Func<Claim, bool>> filterPredicate = e => e.ClaimId == Id;
            var Claim = await _claimRepository.GetClaimByCustomerIdAsync(Id);
            if (Claim != null)
            {
                return Claim;
            }
            throw new DataNotFoundExcpetion("No Claim Data Found");
        }

        public async Task<bool> UpdateClaim(Claim claim)
        {
            var ClaimToUpdate = await _repository.GetById(claim.ClaimId);
            if (ClaimToUpdate != null)
                return await _repository.Update(claim, claim.ClaimId);
            throw new DataNotFoundExcpetion("No Claim Data found");
        }

        public async Task<PageList<Claim>> GetAsync(PageParameters pageParameter)
        {
            Expression<Func<Claim, bool>> filterPredicate = e => e.Status == true;
            var customers = await _repository.GetAsync(filterPredicate);
            return PageList<Claim>.ToPagedList(customers, pageParameter.PageNumber, pageParameter.PageSize);
        }
    }
}
