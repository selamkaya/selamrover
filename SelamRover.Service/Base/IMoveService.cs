using SelamRover.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Service
{
    public interface IMoveService
    {
        Location Process(Device device, int x, int y, string direction, string command);
    }
}
