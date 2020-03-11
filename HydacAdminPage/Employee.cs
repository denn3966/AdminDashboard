using System;
using System.Collections.Generic;
using System.Text;

namespace HydacAdminPage
{
    public class Employee : Person
    {
        public string IMG { get; set; }
        public string Initials { get; set; }
        public string LandlinePhone { get; set; }
        public string PinCode { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
    }
}
