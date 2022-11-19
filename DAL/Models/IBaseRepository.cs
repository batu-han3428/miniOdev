using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public interface IBaseRepository<T>
       where T : class, new()
    {
        int Add(T model);
        int Delete(T model);
        int Update(T model);
        T List(int id);
        List<T> AllList(Expression<Func<T, bool>> filter = null);

    }
}
