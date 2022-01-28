using SelamRover.Common;
using SelamRover.Service;
using System;
using System.Collections.Generic;

namespace SelamRover.Operation
{
    public class MoveOperation : IMoveOperation
    {
        #region Fields
        private readonly IMoveService _moveService;
        #endregion

        #region Constructors
        public MoveOperation(IMoveService moveService)
        {
            #region Field
            _moveService = moveService;
            #endregion
        }
        #endregion

        #region Methods
        public DataResult<MoveResponseModel> Initialize(MoveRequestModel moveRequestModel)
        {
            DataResult<MoveResponseModel> dataResult = new DataResult<MoveResponseModel>();
            try
            {
                Field plateau = new Field(moveRequestModel.Field.Width, moveRequestModel.Field.Height);
                Position position = new Position(0, 0);
                Device rover = new Device(plateau, position, "N");

                dataResult.Data = new MoveResponseModel();
                foreach (var command in moveRequestModel.Commands)
                {
                    var currentLocation = _moveService.Process(rover, command.Ox, command.Oy, command.Pole, command.Command);

                    if (!currentLocation.InField)
                    {
                        dataResult.Failed = true;
                        dataResult.Title = "Warning";
                        dataResult.Message = "The device out of the plateau. Read DATA for last results";
                        return dataResult;
                    }

                    dataResult.Title = "Success";
                    dataResult.Message = "Read DATA for results:";
                    dataResult.Data.Results.Add(new ResultModel() { Ox = currentLocation.Ox, Oy = currentLocation.Oy, Pole = currentLocation.Pole });
                }
            }
            catch
            {
                dataResult.Failed = true;
                dataResult.Title = "Error";
                dataResult.Message = "Please try again.";
            }

            return dataResult;
        }
        #endregion
    }
}
