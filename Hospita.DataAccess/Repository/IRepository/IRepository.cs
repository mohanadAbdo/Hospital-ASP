﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>?filter=null,string? includeProperties = null);
        T Get(Expression<Func<T,bool>>filter, string? includeProperties = null,bool tracked = false);
 
        void Delete(T entity);
        void Add(T entity);
        void Deleterange(IEnumerable<T>entity);
    }
}
