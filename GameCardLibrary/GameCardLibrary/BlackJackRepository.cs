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
using BlackJack_Database;

namespace GameCardLibrary
{
    /// <summary>
    /// Repository class to access the DAL and communicate with the Black Jack class.
    /// Calls function to perform CRUD operations
    /// </summary>
    class BlackJackRepository : IRepository
    {
        private ScoringContext context;

        public BlackJackRepository(ScoringContext db)
        {
            this.context = db;
        }

        /// <summary>
        /// Clears all data in the DB
        /// </summary>
        public void clearAll()
        {
            DatabaseController.clearAll();
        }

        /// <summary>
        /// Retrieves score from the database
        /// </summary>
        /// <param name="player">for this player</param>
        /// <returns>the player score</returns>
        public int getPlayerScore(String player)
        {
            int score = DatabaseController.getPlayerScore(player);

            return score;
        }

        /// <summary>
        /// Retrieves all the scores in the database, sorted descending
        /// </summary>
        /// <returns>List of player and their score</returns>
        public List<Tuple<int, string>> getScoreTable()
        {
            return DatabaseController.getScoreValues();
        }

        /// <summary>
        /// Add a new player to the database
        /// </summary>
        /// <param name="player">the player object</param>
        public void InsertPlayer(Player player)
        {
            List<int> handValues = getHandValues(player.Hand);
            Score newScore = DatabaseController.createPlayerScore(handValues, player.Score);
            DatabaseController.addPlayer(player.Name, newScore, 400);
        }

        public void updatePlayerMoney(Player player, int money)
        {
            DatabaseController.updatePlayerMoney(player.Name, money);
        }

        /// <summary>
        /// Call function to calculate score for the player, based on the hand
        /// </summary>
        /// <param name="player"></param>
        public void updatePlayerScore(Player player)
        {
            List<int> handValues = getHandValues(player.Hand);
            Score newScore = DatabaseController.createPlayerScore(handValues, player.Score);
            int playerID = DatabaseController.getPlayerId(player.Name);

            DatabaseController.updatePlayerScore(playerID, newScore);

        }

        /// <summary>
        /// Function to retrieve values for a hand
        /// </summary>
        /// <param name="thisHand">hand to retrieve values for</param>
        /// <returns>list of card values</returns>
        public List<int> getHandValues(Hand thisHand)
        {
            List<Card> cards = thisHand.GetHand;
            List<int> cardValues = new List<int>();

            foreach(Card c in cards)
            {
                cardValues.Add(c.Value);
            }

            return cardValues;
        }

        /// <summary>
        /// Returns how much money a player currently has
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns></returns>
        public int getPlayerMoney(String playerName)
        {
            int money = DatabaseController.getPlayerMoney(playerName);

            return money;
        }

        /// <summary>
        /// Function to delete a player completely, including their money and score
        /// </summary>
        /// <param name="pName"></param>
        public void deletePlayer(String pName)
        {
            int id = DatabaseController.getPlayerId(pName);
            DatabaseController.deletePlayer(id);
        }
    }
}
