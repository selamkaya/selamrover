using System;
using System.Collections.Generic;

namespace SelamRover.Common
{
    public class MoveRequestModel
    {

        public FieldModel Field { get; set; }

        public List<CommandModel> Commands { get; set; }
    }
}
