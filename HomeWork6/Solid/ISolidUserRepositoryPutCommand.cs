using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6.Solid
{
    public interface ISolidUserRepositoryPutCommand<T>
    {
        public bool CreateUser(T obj);
        public bool UpdateUser(T obj);
        public bool DeleteUser(int id);
    }
}
