using Newtonsoft.Json;
using SelamRover.Common;
using SelamRoverClient.Service.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SelamRoverClient.Service
{
    public class RoverService : IRoverService
    {
        public void Run()
        {
            try
            {
                Console.WriteLine("Enter plateau width - height 'W H'");

                var fieldInput = Console.ReadLine();
                FieldModel plateu = CreateFieldModel(fieldInput);

                List<CommandModel> commands = new List<CommandModel>();
                AddCommand(commands);

                MoveRequestModel moveRequestModel = new MoveRequestModel() { Field = plateu, Commands = commands };
                var response = Post<MoveRequestModel, MoveResponseModel>(moveRequestModel, "http://localhost:41526/move");

                if (response.Failed)
                {
                    Console.WriteLine($"Error: { response.Message}");
                    return;
                }

                foreach (var result in response.Data.Results)
                {
                    Console.WriteLine($"{result.Ox} {result.Oy} {result.Pole}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: { ex.Message}");
            }

            Console.ReadLine();
        }

        private void AddCommand(List<CommandModel> commands)
        {
            Console.WriteLine("Enter rover position to 'X Y D' format.");
            var positionInput = Console.ReadLine();
            Console.WriteLine("Please enter rover commands.");
            var commandInput = Console.ReadLine();

            commands.Add(CreateCommandModel(positionInput, commandInput));

            Console.WriteLine("Do you any commands? (Y)");
            string haveAny = Console.ReadLine();
            if (haveAny.ToUpper() == "Y") AddCommand(commands);
        }

        private FieldModel CreateFieldModel(string fieldInput)
        {
            try
            {
                var size = fieldInput.Split(' ');
                if (size.Length < 2) throw new Exception("Plase check plateau width - height");
                return new FieldModel()
                {
                    Width = Convert.ToInt32(size[0]),
                    Height = Convert.ToInt32(size[1])
                };
            }
            catch
            {
                throw new Exception("Plase check plateau width - height");
            }
        }

        private CommandModel CreateCommandModel(string positionInput, string commandInput)
        {
            try
            {
                var position = positionInput.Split(' ');
                if (position.Length < 2) throw new Exception("Plase check rover position");
                return new CommandModel()
                {
                    Ox = Convert.ToInt32(position[0]),
                    Oy = Convert.ToInt32(position[1]),
                    Pole = position[2].ToUpper(),
                    Command = commandInput
                };
            }
            catch
            {
                throw new Exception("Plase check plateau width - height");
            }
        }

        #region Helper
        private DataResult<TRes> Post<TReq, TRes>(TReq request, string url) where TReq : class where TRes : class
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestString = JsonConvert.SerializeObject(request);

                    var contentData = new StringContent(requestString, System.Text.Encoding.UTF8, "application/json");
                    using (HttpResponseMessage res = client.PostAsync(url, contentData).Result)
                    using (HttpContent content = res.Content)
                    {
                        string responseString = content.ReadAsStringAsync().Result;
                        if (string.IsNullOrEmpty(responseString)) throw new Exception("RESPONSE-EMPTY");
                        return JsonConvert.DeserializeObject<DataResult<TRes>>(responseString);
                    }
                }
            }
            catch (Exception ex)
            {
                return new DataResult<TRes>()
                {
                    Failed = true,
                    Message = ex.ToString()
                };
            }
        }
        #endregion
    }
}
