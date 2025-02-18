using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScape.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public Role Role { get; set; }
        public List<UserItem> UserItems { get; set; }
        public List<Trade> SentTrades { get; set; }
        public List<Trade> ReceivedTrades { get; set; }
    }
}
