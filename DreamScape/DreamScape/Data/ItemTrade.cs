using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScape.Data
{
    public class ItemTrade
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int TradeId { get; set; }
        public int Quantity { get; set; }
        public bool IsSenderItem { get; set; }

        public Item Item { get; set; }
        public Trade Trade { get; set; }
    }
}
