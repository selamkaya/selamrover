using SelamRover.Common.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common
{
    public class Position : IPosition
    {
        private int _ox;
        private int _oy;
        public Position(int ox, int oy)
        {
            _ox = ox;
            _oy = oy;
        }
        public int Ox
        {
            get { return _ox; }
            set { _ox = value; }
        }
        public int Oy
        {
            get { return _oy; }
            set { _oy = value; }
        }
    }
}
