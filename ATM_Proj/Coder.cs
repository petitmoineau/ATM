using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    interface eCoder
    {
    }

    public enum Result
    {
        ERROR = -1,
        FAIL = 0,
        SUCCESS
    }

    class eEncoder : eCoder
    { 
        public string encode(eLog _payload) { return null; } 
    }

    class eDecoder : eCoder 
    {
        static public Result decode(eLog toDecode) 
        {
            
            return Result.FAIL;
        }
    }
}
