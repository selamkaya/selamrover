using SelamRover.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common
{
    public class TurnLeftCommand : ICommand
    {
        public void Run(Device device)
        {
            switch (device.Pole.ToUpper())
            {
                case "N":
                    device.Pole = "W";
                    break;
                case "E":
                    device.Pole = "N";
                    break;
                case "S":
                    device.Pole = "E";
                    break;
                case "W":
                    device.Pole = "S";
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
