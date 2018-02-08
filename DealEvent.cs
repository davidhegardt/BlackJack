using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCardLibrary;

namespace Black_Jack
{
    public class DealEvent : EventArgs
    {
        private Hand playerHand;
        private Hand dealerHand;

        public DealEvent(Hand player,Hand dealer)
        {
            this.dealerHand = dealer;
            this.playerHand = player;
            
        }

        public Hand PlayerHand
        {
            get { return this.playerHand; }
            set
            {
                this.playerHand = value;
            }
        }

        public Hand DealerHand
        {
            get { return this.dealerHand; }
            set
            {
                this.dealerHand = value;
            }
        }
    }
}
