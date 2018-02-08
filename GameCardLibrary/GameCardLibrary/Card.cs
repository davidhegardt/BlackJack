/// <summary>
/// Game Card Library
/// Assignment 1 - C# III Course
/// David Hegardt
/// 2017-11-01
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLibrary
{
    /// <summary>
    /// Model of a game-card. Uses a SuiteEnum for the current
    /// card color.
    /// </summary>
    public class Card
    {
        private SuiteEnum suite;
        private int value;
        private String suiteImage;
        private String downImage;
        private String cardValue;

        public Card()
        {

        }

        public Card(SuiteEnum pSuite,int pValue,String pSuiteImage)
        {
            this.suite = pSuite;
            this.value = pValue;
            this.suiteImage = pSuiteImage;
        }
        
        
        public String FaceUp
        {
            get { return this.suiteImage; }
            set
            {
                this.suiteImage = value;
            }
        }

        public String FaceDown
        {
            get { return this.downImage; }
            set
            {
                this.downImage = value;
            }
        }

        public SuiteEnum Suite
        {
            get { return this.suite; }
            set
            {
                this.suite = value;
            }
        }

        public int Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
            }
        }
        /// <summary>
        /// Denotes the values if the card is not of value 2-10.
        /// E.g a Card with value 1 is an Ace
        /// </summary>
        private void DenoteValue()
        {
            switch (value)
            {
                case 1: cardValue = "Ace";
                    break;
                case 11: cardValue = "Jack";
                    break;
                case 12: cardValue = "Queen";
                    break;
                case 13: cardValue = "King";
                    break;
                default: cardValue = value.ToString();
                    break;
            }
        }

        public override string ToString()
        {
            DenoteValue();
            String cardName = cardValue + " of " + Enum.GetName(typeof(SuiteEnum), suite);
            return cardName;
        }
    }
}
