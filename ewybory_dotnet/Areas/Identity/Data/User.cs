﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ewybory_dotnet.Areas.Identity.Data
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
