using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6
{
    public interface IUserRepository<T>
    {
       public bool cterateUser(T obj);
        public T GetUser(int id);
        public bool UpdateUser(T obj);
        public bool DeleteUser(int id);
    }
}
