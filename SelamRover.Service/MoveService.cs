using SelamRover.Common;
using SelamRover.Common.Base;
using System;

namespace SelamRover.Service
{
    public class MoveService : IMoveService
    {
        private readonly ServiceResolver _command;
        public MoveService(ServiceResolver serviceResolver )
        {
            _command = serviceResolver;
        }
            
        public Location Process(Device device, int x, int y, string pole, string command)
        {
            device.Position.Ox = x;
            device.Position.Oy = y;
            device.Pole = pole;

            var commandList = command.ToCharArray();

            foreach (var commandItem in commandList)
            {
                _command(commandItem.ToString().ToUpper()).Run(device);
            }

            return new Location()
            {
                Ox = device.Position.Ox,
                Oy = device.Position.Oy,
                Pole = device.Pole,
                InField = (device.Field.Width >= device.Position.Ox && device.Field.Height >= device.Position.Oy && device.Position.Ox >= 0 && device.Position.Oy >= 0)
            };
        }
    }
}
