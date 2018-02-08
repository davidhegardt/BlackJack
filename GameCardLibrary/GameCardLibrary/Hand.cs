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
    /// Model object for a Hand of cards.
    /// Used by both dealer and all players
    /// </summary>
    public class Hand
    {
        private List<Card> cards;
        private int score;

        public Hand()
        {
            cards = new List<Card>();
        }

        public int NumberOfCards
        {
            get { return cards.Count; }
        }
                

        public int Score
        {
            get { return this.score; }
            set
            {
                this.score = value;
            }
        }

        public void AddCard(Card newCard)
        {
            cards.Add(newCard);
        }

        public void removeCard(Card deleteCard)
        {
            cards.Remove(deleteCard);
        }

        public void Clear()
        {
            cards.Clear();
        }

        public List<Card> GetHand
        {
            get { return cards; }
        }

        public override string ToString()
        {
            String handString = "";
            foreach (Card i in cards)
            {
                handString = i.ToString() + "\n";
            }
            return handString;
        }

    }
}
