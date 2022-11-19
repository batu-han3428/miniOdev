using DAL.Models;
using DOMAIN.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T>
       where T : class, new()
    {
        protected SqlDbContext context;
        public BaseRepository()
        {
            context = new SqlDbContext();
        }
        public int Add(T model)
        {
            try
            {
                context.Set<T>().Add(model);
                context.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int Update(T model)
        {

            try
            {
                context.Entry<T>(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                context.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<T> AllList(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
            {
                return context.Set<T>().AsNoTracking().ToList();
            }
            else
            {
                return context.Set<T>().Where(filter).ToList();
            }
        }

        public T List(int id)
        {
            return context.Set<T>().Find(id);
        }

        public int Delete(T model)
        {
            try
            {
                context.Entry<T>(model).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
