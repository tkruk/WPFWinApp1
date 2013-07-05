using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiments.Other
{
    public class UnixTime
    {
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long FromDateTime(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return (long)dateTime.Subtract(epoch).TotalSeconds;
            }

            throw new ArgumentException("Input date time must be in UTC!");
        }

        public static DateTime ToDateTime(long unixTime)
        {
            return epoch.AddSeconds(unixTime);
        }

        public static long Now
        {
            get
            {
                return FromDateTime(DateTime.UtcNow);
            }

        }
    }
}
