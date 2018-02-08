/// <summary>
/// Game Card Library
/// Assignment 1 - C# III Course
/// David Hegardt
/// 2017-10-09
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    /// <summary>
    /// Model object of a Player, inherits IPerson interface.
    /// A player has a Hand of cards and competes for a hand
    /// as close to 21 as possible
    /// </summary>
    public class Player : IPerson
    {
        private bool isFinished;
        private static int limit = 21;
        private int currentBet;
        private string playerName;
        private int playerID;
        private Hand hand;
        private bool isThick;
        private int score;

        public Player(String pName) : this(pName, 1) { }

        public Player(String pName,int pId)
        {
            this.Name = pName;
            this.playerID = pId;
            this.hand = new Hand();
        }


        public bool Finished
        {
            get { return this.isFinished; }
            set
            {
                this.isFinished = value;
            }
        }

        public string Name
        {
            get { return this.playerName; }
            set
            {
                this.playerName = value;
            }
        }

        public int ID
        {
            get { return this.playerID; }
            set
            {
                this.playerID = value;
            }
        }

        public int Bet
        {
            get { return this.currentBet; }
            set
            {
                this.currentBet = value;
            }
        }

        public int Score {
            get { return score; }
            set
            {
                score = value;
            }
        }

        public bool IsThick
        {
            get { return this.isThick; }
            set
            {
                this.isThick = value;
            }
        }

        public Hand Hand { get { return hand; } set { hand = value; } }
    }
}
