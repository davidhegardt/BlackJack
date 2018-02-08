using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black_Jack
{
    public class EndEvent : EventArgs
    {
        private int playerID;
        private String endStatus;
        private int endScore;

        public EndEvent(int playerID, String endStatus, int endScore)
        {
            this.playerID = playerID;
            this.endStatus = endStatus;
            this.endScore = endScore;
        }

        public int PlayerID
        {
            get { return this.playerID; }
            set
            {
                this.playerID = value;
            }
        }

        public String Status
        {
            get { return endStatus; }
        }

        public int Score
        {
            get { return this.endScore; }
        }
    }
}
