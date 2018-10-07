using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewTestApi.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime JoiningDate { get; set; }
        public int Age { get; set; }
    }
}