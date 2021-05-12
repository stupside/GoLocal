using System;

namespace GoLocal.Domain.ValueObjects
{
    public class TimeRange
    {
        public TimeSpan Max { get; set; }
        public TimeSpan Min { get; set; }

        public TimeRange() {}

        public TimeRange(TimeSpan begin, TimeSpan end)
            : this()
        {

            Min = begin;
            Max = end;
        }
        
        public TimeSpan GetDifference()
            => Max - Min;
    }
}