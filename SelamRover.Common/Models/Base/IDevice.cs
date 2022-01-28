using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common.Base
{
    public interface IDevice
    {
        Field Field { get; set; }
        Position Position { get; set; }
        string Pole { get; set; }
    }
}
