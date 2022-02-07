﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NetReact.Application.Common.Exceptions
{
     public class BadRequestException : ApiException
     {
          public BadRequestException(string message) 
               : base(HttpStatusCode.BadRequest, message)
          {
          }
     }
}
