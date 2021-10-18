using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWSCall2
{
    public class Shelf
    {
        public List<Book> Book { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
    }
}
