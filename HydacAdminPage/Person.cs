using System;
using System.Collections.Generic;
using System.Text;

namespace HydacAdminPage
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public int Id { get; set; }
        public int CheckIn_Id { get; set; }
    }
}
