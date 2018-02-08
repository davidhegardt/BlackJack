using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCardLibrary;

namespace Black_Jack
{
    public class HitEvent : EventArgs
    {
        private Hand playerHand;
        private int playerId;

        public HitEvent(Hand playerHand, int currPlayerID)
        {
            this.playerHand = playerHand;
            this.playerId = currPlayerID;
        }

        public Hand Playerhand
        {
            get { return this.playerHand; }
            set
            {
                this.playerHand = value;
            }
        }

        public int PlayerID
        {
            get { return this.playerId; }
            set
            {
                this.playerId = value;
            }
        }

    }
}
