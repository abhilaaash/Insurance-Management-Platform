using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EInsuranceProject.Repository
{
    public interface IEntityRepository<T>
    {
        public Task<IEnumerable<T>> GetAll(string[] innerTable, Expression<Func<T, bool>> predicate);
       
        public Task<T> GetById(Expression<Func<T, bool>> predicate, string[] innerTables);
        public Task<T> GetById(object Id);

        public Task Insert(T entity);
        public Task<bool> Update(T entity,int id);
        public  Task<bool> Delete(object id);

        public T FindUser(Func<T, bool> predicate);

        public string GetRoleName(T entity, Func<T, string> roleSelector);

        public Task<T> InsertTest(T entity);
        public Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);


        public Task<T> GetByUserId(Expression<Func<T, bool>> predicate);
    }
}
