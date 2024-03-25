using Contracts;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//https://code-maze.com/async-generic-repository-pattern/
namespace Application
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<T> FindAll()
        {
            return _applicationDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
           return _applicationDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _applicationDbContext.Set<T>().Add(entity);
         
        }

        public void Delete(T entity)
        {
            _applicationDbContext.Set<T>().Remove(entity);
        }        

        public void Update(T entity)
        {
            _applicationDbContext.Set<T>().Update(entity);
         
        }

        public void SaveChangesAsync()
        {
            _applicationDbContext.SaveChanges();
        }

        public void DeleteByCondition(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
