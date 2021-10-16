using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BannedAccountException : Exception
    {
        public BannedAccountException(string msg):base(msg)
        {

        }
    }
}
