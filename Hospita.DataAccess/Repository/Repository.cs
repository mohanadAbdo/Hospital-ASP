using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Hospital.DataAccess.Data;
using Hospital.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
namespace Hospital.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbbSet = _db.Set<T>();
            _db.Doctors.Include(u => u.Category).Include( u => u.CategoryId);
        }

        public void Add(T entity)
        {
            dbbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbbSet.Remove(entity);
        }

        public void Deleterange(IEnumerable<T> entity)
        {
            dbbSet.RemoveRange(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbbSet; 
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split
                   (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbbSet;
            if(!string.IsNullOrEmpty(includeProperties))
            {
                 foreach(var includeProp in includeProperties.Split
                    (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList(); 
        }


    }
}
