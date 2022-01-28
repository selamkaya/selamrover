using SelamRover.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common.Base
{
    public interface ICommand
    {
        void Run(Device device);
    }
}
