﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCORE.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string About { get; set; }
        public string Interest { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }

    }
}
