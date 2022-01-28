using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common
{
    public class DataResult<TData> : ResultBase
    {
        public TData Data { get; set; }
    }
}
