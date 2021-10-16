using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EmptyListException : Exception
    {
        public EmptyListException(string msg): base(msg)
        {
                
        }
    }
}
