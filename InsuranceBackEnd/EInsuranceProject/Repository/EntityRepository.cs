using EInsuranceProject.Data;
using EInsuranceProject.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace EInsuranceProject.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class

    {
        private readonly InsuranceContext _context;

        private readonly DbSet<T> _table;
        public EntityRepository(InsuranceContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public  async Task<IEnumerable<T>> GetAll(string[] innerTables, Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _table;
           query = query.Where(predicate); 
            if(innerTables!=null)
            {
                foreach (var innerTable in innerTables)
                {
                    query = query.Include(innerTable).Where(predicate); 
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetById( Expression<Func<T, bool>> predicate, string[] innerTables) 
        {
           IQueryable<T> query= _table;
            query = query.Where(predicate);

            if (innerTables != null)
            {
                foreach (var innerTable in innerTables)
                {
                    query = query.Include(innerTable).Where(predicate);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
        public async Task Insert(T entity)
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Update(T enitiy,int id)
        {
           T ExistingEntiy = await _table.FindAsync(id);
            if(ExistingEntiy != null)
            {
                _context.Entry(ExistingEntiy).State = EntityState.Detached;
                 _table.Update(enitiy);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(object id)
        {


            T ExistingEntity = await _table.FindAsync(id);
            if (ExistingEntity != null)
            {
                _table.Remove(ExistingEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<T> GetById(object Id)
        {
            return await _table.FindAsync(Id);
        }



        //--------------------------------------------------
        public T FindUser(Func<T, bool> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public string GetRoleName(T entity, Func<T, string> roleSelector)
        {
            if (entity != null)
            {
                var roleName = roleSelector(entity);
                return roleName;
            }

            return null; // Handle if the entity is null
        }
        ////-----------------------------------------delte from here
        ///
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _table;
            query = query.Where(predicate);

            return await query.ToListAsync();
        }
        public async Task<T> InsertTest(T entity)
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> GetByUserId(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _table;
            query = query.Where(predicate);
            return await query.FirstOrDefaultAsync();

        }

    }
}
