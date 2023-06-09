﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Wrappers
{
    public partial class ServiceResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

    }

    public partial class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
    }
}
