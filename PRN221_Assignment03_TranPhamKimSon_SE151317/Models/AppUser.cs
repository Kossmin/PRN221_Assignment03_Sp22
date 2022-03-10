using System;
using System.Collections.Generic;

#nullable disable

namespace PRN221_Assignment03_TranPhamKimSon_SE151317.Models
{
    public partial class AppUser
    {
        public AppUser()
        {
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
