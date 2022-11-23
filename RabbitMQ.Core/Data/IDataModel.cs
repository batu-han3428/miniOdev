using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Data
{
    public interface IDataModel<T>
    {
        IEnumerable<T> GetData();
    }
}
