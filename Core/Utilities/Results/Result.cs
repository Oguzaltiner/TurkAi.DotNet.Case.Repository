using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success,string message):this(success)
        {
            this.message = message;
        }

        public Result(bool success)
        {
            isSuccess = success;
        }
        public bool isSuccess { get; }

        public string message { get; }
    }
}
