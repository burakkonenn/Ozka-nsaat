﻿using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User:IdentityUser<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
