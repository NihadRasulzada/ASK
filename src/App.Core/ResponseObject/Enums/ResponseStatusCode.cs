using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.ResponseObject.Enums;

public enum ResponseStatusCode
{
    Success = 1,
    ValidationError = 2,
    NotFound = 3,
    Error = 4,
    BadRequest = 5,
    Unauthorized = 6,
    Forbidden = 7,
    Conflict = 8,
    InternalServerError = 9
}