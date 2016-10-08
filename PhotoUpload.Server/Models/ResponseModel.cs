using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoUpload.Server.Models
{
    public class ResponseModel
    {
        public string Message { get; set; }
        public Object Data { get; set; }
        public bool IsSuccess { get; set; }

        public ResponseModel(object data = null, string message = "Success", bool isSuccess = true)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}