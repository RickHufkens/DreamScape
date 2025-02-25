﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamScape.Data
{
    public class Trade
    {
        public int Id { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
        public List<ItemTrade> ItemTrades { get; set; }
        public Item Item { get; set; }
    }
}
