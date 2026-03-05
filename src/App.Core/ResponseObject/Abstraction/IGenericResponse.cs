using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.ResponseObject.Abstraction;

public interface IResponse<T> : IResponse
{
    T Data { get; set; }
}