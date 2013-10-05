using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class OfferDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string PhotoUrl { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public virtual UserDto Author { get; set; }

        public string Town { get; set; }

        public decimal Price { get; set; }
    }
}