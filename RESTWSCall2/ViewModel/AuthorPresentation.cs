using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWSCall2
{
    public class AuthorPresentation
    {
        public int Id { get; set; }
        public string Name
        {
             get {return FirstName + " " + LastName; }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
