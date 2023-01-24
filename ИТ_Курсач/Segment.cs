using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ИТ_Курсач
{
    public struct Segment
    {
        
        public readonly long Offset;
        public readonly long Length;
        public readonly byte[] Hash;

        internal Segment(long offset, long length, byte[] hash)
        {
            this.Offset = offset;
            this.Length = length;
            this.Hash = hash;
        }
    }

}

