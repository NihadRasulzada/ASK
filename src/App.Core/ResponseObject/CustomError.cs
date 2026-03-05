using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.ResponseObject;

public class CustomError
{
    public string Code { get; set; }
    public string Description { get; set; }
    public CustomError(string code, string description)
    {
        Code = code;
        Description = description;
    }
    public CustomError()
    {
    }
}