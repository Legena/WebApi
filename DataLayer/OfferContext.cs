using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OfferContext : DbContext
    {
        public OfferContext() :
            base("OffersDb")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Offer> Offers { get; set; }
    }
}
