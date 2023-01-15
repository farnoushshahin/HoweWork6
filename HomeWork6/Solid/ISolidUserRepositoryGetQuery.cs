using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork6.Solid
{
    public interface ISolidUserRepositoryGetQuery<T>
    {
        public T GetUser(int id);
    }
}
