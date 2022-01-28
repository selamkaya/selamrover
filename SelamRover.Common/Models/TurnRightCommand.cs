using SelamRover.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common
{
    public class TurnRightCommand : ICommand
    {
        public void Run(Device device)
        {
            switch (device.Pole.ToUpper())
            {
                case "N":
                    device.Pole = "E";
                    break;
                case "E":
                    device.Pole = "S";
                    break;
                case "S":
                    device.Pole = "W";
                    break;
                case "W":
                    device.Pole = "N";
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
