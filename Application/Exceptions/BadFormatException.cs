using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BadFormatException : Exception
    {
        public BadFormatException(string msg):base(msg)
        {

        }
    }
}
