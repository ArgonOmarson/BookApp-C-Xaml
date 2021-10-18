using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWSCall2 
{
    public class Book
    {
        public List<Author> Author { get; set; }
        public Shelf Shelf { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int ShelfId { get; set; }
        public string ImageLink { get; set; }
    }
}
