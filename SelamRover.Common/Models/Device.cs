using SelamRover.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common
{
    public class Device : IDevice
    {
        private Field _field;
        private Position _position;
        private string _pole;
        public Device(Field field, Position position, string pole)
        {
            _field = field;
            _position = position;
            _pole = pole;
        }

        public Field Field
        {
            get { return _field; }
            set { _field = value; }
        }

        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public string Pole
        {
            get { return _pole; }
            set { _pole = value; }
        }
    }
}
