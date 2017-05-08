using System;
using System.Collections.Generic;
using System.Text;

namespace Bonbonniere.Infrastructure.Util
{
    public class Clock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}
