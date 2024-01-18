using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class AdminService : IAdminService
    {
        private IEntityRepository<Admin> _repository;
        private IAdminRepository _adminRepository;
        private ICustomerRepository _customerRepository;
   //     private IEntityRepository<User> _repositoryUser;  private IEntityRepository<Role> _repositoryRole;

        public AdminService(IEntityRepository<Admin> repository, IAdminRepository adminRepository, ICustomerRepository customerRepository)
        {
            _repository = repository;
            _adminRepository = adminRepository;
            _customerRepository = customerRepository;
            //  _repositoryUser = repositoryUser;
            //_repositoryRole = repositoryRole;
        }
        public async Task AddAdmin(Admin admin)
        {
            await _repository.Insert(admin);
        }

        public async Task<bool> DeleteAdmin(int id)
        {
            var AdminToDelete = await _repository.GetById(id);
            if (AdminToDelete != null)
                AdminToDelete.Status = false;
                return await _repository.Update(AdminToDelete,id);
            throw new DataNotFoundExcpetion("No admin Data found");
        }

        public async Task<IEnumerable<Admin>> GetAll(string[] innerTables)
        {
            Expression<Func<Admin, bool>> filterPredicate = e => e.Status == true;
            var admins = await _repository.GetAll(innerTables,filterPredicate);
            if (admins.Count() > 0)
            {
                return admins;
            }
            throw new DataNotFoundExcpetion("No admin Data found");
        }

        public async Task<Admin> GetById(int Id)
        {
            string[] innerTables = { };
            Expression<Func<Admin, bool>> filterPredicate = e => e.Status == true && e.UserId==Id;
            var admin = await _repository.GetById(filterPredicate,innerTables);
            if (admin != null)
            {
                return admin;
            }
            throw new DataNotFoundExcpetion("No admin Data found");
        }

        public async Task<bool> UpdateAdmin(Admin admin)
        {
            var adminToUpdate = await _repository.GetById(admin.AdminId);
            if (adminToUpdate != null)
            {
                return await _repository.Update(admin, admin.AdminId);
            }
            throw new DataNotFoundExcpetion("No admin Data found");
        }

        public async Task AddAdminWithUser(Admin admin, User user)
        {
            int id = await _adminRepository.AddUser(user);
            admin.UserId= id;
            await _adminRepository.AddAdminWithUser(admin);
        }

        //-------------------------------delete from here

        //public async Task<Admin> GetByUserId(int Id, string[] innerTables)

        //{
        //    Expression<Func<Admin, bool>> filterPredicate = e => e.Status == true && e.UserId == Id;
        //    var user = await _customerRepository.GetById(filterPredicate, innerTables);
        //    if (user != null)
        //    {
        //        return user;
        //    }
        //    throw new DataNotFoundExcpetion("No user Data found");
        //}


    }
}
