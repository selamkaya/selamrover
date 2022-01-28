using SelamRover.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common
{
    public class MoveCommand : ICommand
    {
        public void Run(Device device)
        {
            switch (device.Pole.ToUpper())
            {
                case "N":
                    device.Position.Oy += 1;
                    break;
                case "E":
                    device.Position.Ox += 1;
                    break;
                case "S":
                    device.Position.Oy -= 1;
                    break;
                case "W":
                    device.Position.Ox -= 1;
                    break;
            }
        }
    }
}
