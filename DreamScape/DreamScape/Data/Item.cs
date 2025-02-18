using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScape.Data
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Tradeable { get; set; }
        public int RarityId { get; set; }
        public int TypeId { get; set; }

        public Rarity Rarity { get; set; }
        public Type Type { get; set; }
        public ICollection<UserItem> UserItems { get; set; }
        public ICollection<ItemTrade> ItemTrades { get; set; }
    }
}
