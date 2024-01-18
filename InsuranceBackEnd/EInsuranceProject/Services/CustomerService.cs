using BankingAPI.Kernel.Wrapper;
using EInsuranceProject.DTO;
using EInsuranceProject.Exceptions;
using EInsuranceProject.Model;
using EInsuranceProject.Repository;
using EInsuranceProject.Wrapper;
using System.Linq.Expressions;

namespace EInsuranceProject.Services
{
    public class CustomerService : ICustomerService
    {

        private ICustomerRepository _repository;
     // / private ICustomerService _customerService;

        private IEntityRepository<Customer> _customerRepository;

        public CustomerService(IEntityRepository<Customer> customerRepository, ICustomerRepository repository/*, ICustomerService customerService*/)
        {
            _customerRepository= customerRepository;
            _repository = repository;
       //     _customerService= customerService;
        }
        public async Task<IEnumerable<Customer>> GetAll(string[] innerTables)
        {
            Expression<Func<Customer, bool>> filterPredicate = e => e.Status != null;
            var customers = await _customerRepository.GetAll(innerTables, filterPredicate);
            if (customers.Count() > 0)
            {
                return customers;
            }
            throw new DataNotFoundExcpetion("No Customer Data found");
        }
        public async Task AddCustomer(Customer customer)
        {
            await _customerRepository.Insert(customer);
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var CustomerToDelete = await _customerRepository.GetById(id);
            if (CustomerToDelete != null)
                return await _customerRepository.Delete(id);
            throw new DataNotFoundExcpetion("No Customer Data Found To Delete");
        }



        public async Task<Customer> GetById(int Id)
        {
            var customer = await _customerRepository.GetById(Id);
            if (customer != null)
                return customer;
            throw new DataNotFoundExcpetion("No Customer Data Found");
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var CustomerToUpdate = await _customerRepository.GetById(customer.CustomerId);
            if (CustomerToUpdate != null)
                return await _customerRepository.Update(customer, customer.CustomerId);
            throw new DataNotFoundExcpetion("No Customer Data Found To Update");
        }

        public async Task<List<Customer>> GetCustomersByAgentIdAsync(int agentId)
        {
            var cust= await _repository.GetCustomersByAgentIdAsync(agentId);
            if (cust != null) {
                return cust;
                    }
            throw new DataNotFoundExcpetion("No such agent details found");
        }
        public async Task AddCustomerWithUser(Customer customer, User user)
        {
            int id = await _repository.AddUser(user);
            customer.UserId = id;
            await _repository.AddCustomerWithUser(customer);
        }



        public async Task<PageList<Customer>> GetAsync(Parameter parameter)
        {
            Expression<Func<Customer, bool>> filterPredicate = e => e.Status != null;
            if (parameter.Id != null)
            {
                filterPredicate = e => e.Status != null && e.CustomerId == parameter.Id;
            }
            else if (parameter.FirstName != null)
            {
                filterPredicate = e => e.Status != null && e.CustomerFirstName.Contains(parameter.FirstName);
            }
            else
            {
                filterPredicate = e => e.Status != null;
            }
            var customers = await _repository.GetAsync(filterPredicate);
            return PageList<Customer>.ToPagedList(customers, parameter.PageNumber, parameter.PageSize);
        }
        public async Task<Customer> GetByUserId(int Id, string[] innerTables)

        {
            Expression<Func<Customer, bool>> filterPredicate = e => e.Status != null && e.UserId == Id;
            var user = await _repository.GetById(filterPredicate, innerTables);
            if (user != null)
            {
                return user;
            }
            throw new DataNotFoundExcpetion("No user Data found");
        }
        public async Task<Customer> GetByCustomerId(int Id, string[] innerTables)

        {
            Expression<Func<Customer, bool>> filterPredicate = e => e.Status != null && e.CustomerId == Id;
            var user = await _repository.GetById(filterPredicate, innerTables);
            if (user != null)
            {
                return user;
            }
            throw new DataNotFoundExcpetion("No customer Data found");
        }
        public Customer GetByPhone(string username)
        {
            var roleName = _repository.GetByPhone(username);

            return roleName;

            //  throw new Exception("No user found ");
        }

        public Customer GetByEmail(string username)
        {
            var roleName = _repository.GetByEmail(username);

            return roleName;

            //  throw new Exception("No user found ");
        }
        ///--------------------------------------------------------------------------------
        ///
        public async Task<Customer> GetCustomerByUserIDAsync(int Id)
        {
            Expression<Func<Customer, bool>> filterPredicate = e => e.Status == true && e.UserId == Id;
            var customer = await _repository.GetByUserId(filterPredicate);
            if (customer != null)
                return customer;
            throw new DataNotFoundExcpetion("No Such customer exist");
        }
        public async Task<PageList<Policy>> GetCustomerWithPolicy(int userID,Parameter filterParameter)
        {
            var policies = await _repository.GetCustomerPoliciesAsync(userID, filterParameter);
            return PageList<Policy>.ToPagedList(policies, filterParameter.PageNumber, filterParameter.PageSize);
        }

    }
}
