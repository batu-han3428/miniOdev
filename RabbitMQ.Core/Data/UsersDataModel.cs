using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Data
{
    public class UsersDataModel : IDataModel<User>
    {
        public IEnumerable<User> GetData()
        {
            return new List<User>
            {
                new User{UserId=1,FirstName="Batuhan",LastName="Fındık",Email="batu_6407@hotmail.com.tr"}
               //,new User{UserId=2,FirstName="Barış",LastName="Boy",Email="baris.boy@ericssonmsp.com"}
               //,new User{UserId=3,FirstName="Vacip",LastName="Derici",Email="vacip.derici@ericssonmsp.com"}
               //,new User{UserId=4,FirstName="Talha",LastName="Seçkin",Email="taha.seckin@ericssonmsp.com"}
            };
        }
    }
}
