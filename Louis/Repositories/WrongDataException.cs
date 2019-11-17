
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Louis.Repositories
{
    public class WrongDataException : ArgumentException
    {
        public override string Message{ get { return "Can't find product  or Code already exists."; }  }
    }
}
