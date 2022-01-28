using SelamRover.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelamRover.Operation
{
    public interface IMoveOperation
    {
        DataResult<MoveResponseModel> Initialize(MoveRequestModel moveRequest);
    }
}
