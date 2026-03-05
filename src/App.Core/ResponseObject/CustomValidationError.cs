using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.ResponseObject;

public class CustomValidationError
{
    public string ErrorMessage { get; set; }
    public string PropertyName { get; set; }

    public CustomValidationError()
    {
    }

    public CustomValidationError(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
}