using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelamRover.Common;
using SelamRover.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SelamRover.Api.Startup;

namespace SelamRover.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoveController : ControllerBase
    {
        #region Field
        private readonly IMoveOperation _moveOperation;
        #endregion

        #region Constructor
        public MoveController(IMoveOperation moveOperation, ServiceResolver serviceResolver)
        {
            #region Field
            _moveOperation = moveOperation;
            #endregion
        }
        #endregion

        #region Method
        [HttpPost]
        public ActionResult Post([FromBody] MoveRequestModel moveRequestModel)
        {
            return Ok(_moveOperation.Initialize(moveRequestModel));
        }
        #endregion
    }
}
