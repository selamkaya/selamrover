using SelamRover.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Common
{
    public class MoveResponseModel
    {
        public MoveResponseModel()
        {
            Results = new List<ResultModel>();
        }
        public List<ResultModel> Results { get; set; }
    }
}
