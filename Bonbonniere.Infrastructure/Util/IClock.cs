using System;

namespace Bonbonniere.Infrastructure.Util
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
