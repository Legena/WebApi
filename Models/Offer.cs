using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public partial class Offer
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public string Town { get; set; }    

        public decimal Price { get; set; }

        public virtual User Author { get; set; }
    }
}
