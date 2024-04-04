using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cpu_optimalizalo_BUQ6LY
{
    class AlreadyExistingItemException:Exception
    {
        public AlreadyExistingItemException(string msg):base(msg) { }
        
    }
}
