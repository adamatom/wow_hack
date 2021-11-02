using System;
using System.Collections.Generic;
using System.Text;

namespace Woah
{
    public class MemoryRange: ICloneable
    {
        public uint start;
        public uint end;

        public uint Start
        {
            get
            {
                return start ;
            }
            set
            {
                start = value;
            }
        }

        public uint End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }

        public MemoryRange(uint address)
        {
            start = end = address;
        }
        public MemoryRange(uint rangestart, uint rangeend)
        {
            start = rangestart;
            end = rangeend;
        }

        public uint RangeSize
        {
            get
            {
                return end - start;
            }
        }

        public MemoryRange Intersect(MemoryRange r)
        {
            if (start > r.End || r.Start > end)
                return null;

            if(start >= r.Start)
            {
                if(end <= r.End)
                {
                    return (MemoryRange)Clone();
                }
                else
                {
                    return new MemoryRange(start, r.End);
                }
            }
            else
            {
                if(r.End <= end)
                {
                    return (MemoryRange)r.Clone();
                }
                else
                {
                    return new MemoryRange(r.Start, end);
                }
            }
        }

        public object Clone()
        {
            return new MemoryRange(start, end);
        }
    }
}
