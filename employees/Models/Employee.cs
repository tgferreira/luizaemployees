using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace employees.Models
{
    public class employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string department { get; set; }

        public employee(int id, string name, string email, string department)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.department = department;
        }
    }
}