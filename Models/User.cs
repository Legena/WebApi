using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AuthCode { get; set; }

        public string AccessToken { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }
    }
}
