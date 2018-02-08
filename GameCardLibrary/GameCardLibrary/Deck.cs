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
    /// Model class of a Deck of cards.
    /// Each deck should contain 52 Cards
    /// </summary>
    public class Deck
    {
        private List<Card> cards;
        private int deckID;

        public Deck()
        {
            cards = new List<Card>();
        }

        public List<Card> getDeck()
        {
            return cards;
        }

        public Deck(int id)
        {
            this.deckID = id;
            cards = new List<Card>();
        }

        public int CardCount
        {
            get { return cards.Count; }
        }

        /// <summary>
        /// Function to fill the deck with new cards
        /// </summary>
        public void FillDeck()
        {
            foreach (SuiteEnum color in Enum.GetValues(typeof(SuiteEnum)))      // Suite enum contains 4 colors
            {
                for (int i = 1; i < 14; i++)
                {                                  // Each suite contains 14 cards
                    Card newCard = CardFactory.CreateCard(i, color);            // Call factory to create each card
                    cards.Add(newCard);                                     // Add the new card to the deck
                }
            }

        }

        /// <summary>
        /// Shuffles the cards in the deck.
        /// A Fisher-Yates implementation for Randomization
        /// </summary>
        public void Shuffle()
        {
            int n = CardCount;
            Random random = new Random();
            while (n > 1)
            {
                int k = (random.Next(0, n) % n);
                n--;
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        /// <summary>
        /// Draws a new card from the list of cards
        /// </summary>
        /// <returns></returns>
        public Card drawNextCard()
        {
            Card nextCard = cards[0];
            cards.RemoveAt(0);

            return nextCard;
        }

        public void DiscardCards()
        {
            cards.Clear();
        }
    }
}
