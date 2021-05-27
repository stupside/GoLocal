using System;

namespace GoLocal.Core.Domain.ValueObjects
{
    public class TimeRange
    {
        public TimeSpan Max { get; }
        public TimeSpan Min { get; }

        public TimeRange() {}

        public TimeRange(TimeSpan begin, TimeSpan end)
            : this()
        {

            Min = begin;
            Max = end;
        }

        public TimeSpan GetDifference()
            => Max.Subtract(Min);
    }
}