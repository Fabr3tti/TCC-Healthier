﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }

    }
}
