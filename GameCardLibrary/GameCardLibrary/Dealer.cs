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
    /// Model object of a Dealer, inherits IPerson interface.
    /// Dealer is used to compete agains player, deals cards
    /// and draws cards to his hand.
    /// </summary>
    public class Dealer : IPerson
    {
        private Hand hand;
        private int score;
        private String name;
        private List<Deck> decks;

        public Dealer()
        {
            this.name = "Dealer";
            decks = new List<Deck>();
            hand = new Hand();
        }

        public Hand Hand { get { return hand; } set { hand = value; } }
        public int Score { get { return score; } set { score = value; } }
        public string Name { get { return name; } set { name = value; } }

        public bool IsThick { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Adds a new deck of cards
        /// </summary>
        /// <param name="currDeck">Deck to add to list of decks</param>
        public void addDeck(Deck currDeck)
        {
            decks.Add(currDeck);
        }

        /// <summary>
        /// Shuffles both of the dealers decks
        /// </summary>
        public void shuffleDeck()
        {
            if (decks[0].CardCount > 0)
            {
                decks[0].Shuffle();
            }
            decks[1].Shuffle();
        }

        /// <summary>
        /// Creates new deck of cards for the dealer
        /// </summary>
        public void setupDeck()
        {
            Deck one = new Deck();
            Deck two = new Deck();
            one.FillDeck();                                     // Fill deck with 52 cards
            two.FillDeck();
            addDeck(one);
            addDeck(two);
        }

        /// <summary>
        /// Draws a card from the decks
        /// </summary>
        /// <returns>Card drawn from deck</returns>
        public Card drawCard()
        {
            Card nextcard = null;
            if (decks[0].CardCount > 0)
            {
                nextcard = decks[0].drawNextCard();
            }
            else
            {
                nextcard = decks[1].drawNextCard();
            }

            return nextcard;
        }

        /// <summary>
        /// Returns the number of cards left
        /// </summary>
        /// <param name="whichDeck">The deck to count</param>
        /// <returns>nr of cards in this deck</returns>
        public int getDeckCount(int whichDeck)
        {
            return decks[whichDeck].CardCount;
        }

    }
}
