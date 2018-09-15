using System;
using System.Security.Principal;
using Actio.Common.Exceptions;

namespace Actio.Common.Core
{
    public static class Extensions
    {
        public static long ToTimeStamp(this DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var time = dateTime.Subtract(new TimeSpan(epoch.Ticks));

            return time.Ticks / 10000;
        }

        public static Guid GetUserIdIfExist(this IIdentity identity)
        {
            if (Guid.TryParse(identity.Name, out var guid))
            {
                return guid;
            }

            throw new ActioException(ErrorCode.InvalidGuid(identity.Name));
        }
    }
}
