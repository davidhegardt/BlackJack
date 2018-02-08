/// <summary>
/// Game Card Library
/// Assignment 1 - C# III Course
/// David Hegardt
/// 2017-10-09
/// </summary>
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using BlackJack_Database;

namespace GameCardLibrary
{
    /// <summary>
    /// Handles game logic and rules and conditions for the game BlackJack.
    /// Uses all other classes to implement game logic
    /// </summary>
    public class BlackJack
    {
        private static int limit = 21;
        private Dealer dealer;
        //private Player player;
        private List<Player> players;
        private List<Player> stayList;
        private Card tempCard;
        private int finishCount = 0;
        private int playerCount = 0;
        private BlackJackRepository repository;

        public BlackJack()
        {
            repository = new BlackJackRepository(new ScoringContext());
            //repository.clearAll();

        }
        /// <summary>
        /// Function to compare player agains another player.
        /// Uses inheritance, can be used to compare player vs player and player vs dealer
        /// </summary>
        /// <param name="oponentOne"></param>
        /// <param name="oponentTwo"></param>
        /// <returns></returns>
        public IPerson compareScore(IPerson oponentOne,IPerson oponentTwo)
        {
            oponentOne.Score = calcScore(oponentOne);
            oponentTwo.Score = calcScore(oponentTwo);

            if(oponentOne is Player)
            {
                if (oponentOne.Score >= limit)
                {
                    oponentOne.IsThick = true;
                }
            }

            if (oponentTwo is Player)
            {
                if (oponentTwo.Score >= limit)
                {
                    oponentTwo.IsThick = true;
                }
            }

            if (oponentOne.Score > oponentTwo.Score)
            {
                return oponentOne;
            }
            if(oponentOne.Score == oponentTwo.Score)
            {
                return null;
            }
            if (oponentTwo.Score > oponentOne.Score)
            {
                return oponentTwo;
            }

            return null;
        }

        /// <summary>
        /// Function returns who won in Player vs Dealer.
        /// This is only called if player score is below or exactly 21
        /// </summary>
        /// <param name="player">current player for comparison</param>
        /// <param name="dealer">The dealer</param>
        /// <returns></returns>
        public int compareDealer(Player player, Dealer dealer)
        {
            dealer.Score = calcScore(dealer);
            player.Score = calcScore(player);

            if(player.Score > limit)
            {
                player.IsThick = true;
                return 0;
            }
            
            if(player.Score == limit)
            {
                player.IsThick = true;
                player.Finished = true;
                return 1;
            }

            if(dealer.Score > limit)
            {
                return 1;
            }

            if(dealer.Score == limit)
            {
                return 0;
            }
            
            return 3;
        }

        public void deletePlayer(String PlayerName)
        {
            repository.deletePlayer(PlayerName);
        }

        /// <summary>
        /// If both player and dealer made it to the final draw, this function is run
        /// </summary>
        /// <param name="player"></param>
        /// <param name="dealer"></param>
        /// <returns>if player won or not</returns>
        public bool gameEnd(Player player,Dealer dealer)
        {
            dealer.Score = calcScore(dealer);
            player.Score = calcScore(player);

            if (player.Score > dealer.Score)
            {
                return true;
            } else
            {
                return false;
            }
        }
        /// <summary>
        /// Calculate score of player or dealer.
        /// Uses interface so inherited objects can be sent
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int calcScore(IPerson player)
        {
            Hand currentHand = player.Hand;

            List<Card> cards = currentHand.GetHand;

            int totalHand = 0;

            foreach(Card c in cards)
            {
                int currValue = c.Value;
                totalHand = currValue + totalHand;
            }

            return totalHand;
        }

        /// <summary>
        /// Calculate score for a Hand
        /// </summary>
        /// <param name="currHand"></param>
        /// <returns>total score for hand sent</returns>
        public int calcHandScore(Hand currHand)
        {
            List<Card> cards = currHand.GetHand;

            int totalSum = 0;

            foreach(Card c in cards)
            {
                int currValue = c.Value;
                totalSum = currValue + totalSum;
            }

            return totalSum;
        }

        /// <summary>
        /// Starts a new game, calls function to setup game logic
        /// </summary>
        /// <param name="pPlayerCount">Nr of players in the game</param>
        /// <param name="playerNames">Name of players in the game</param>
        public void startGame(int pPlayerCount, List<String> playerNames)
        {
            dealer = new Dealer();
            players = new List<Player>();
            stayList = new List<Player>();


            this.playerCount = pPlayerCount;

            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player(playerNames[i],i));
            }

            dealer.setupDeck();
            dealer.shuffleDeck();

            
        }

        /// <summary>
        /// Function called when one round is finished
        /// </summary>
        /// <param name="playerNames">Name of players in this game</param>
        public void resetGame(List<String> playerNames)
        {
            dealer.shuffleDeck();
            dealer.Hand.Clear();

            //UPDATE SCORES

            players = new List<Player>();
            stayList = new List<Player>();
            finishCount = 0;

            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player(playerNames[i], i));
            }
        }

        /// <summary>
        /// Function called to update and calculate score of all players in the game
        /// </summary>
        public void updateScores()
        {
            foreach (Player p in players)
            {
                int currScore = calcScore(p);
                p.Score = currScore;
            }
            //int currScore = calcScore(player);
            //player.Score = currScore;
        }

        /// <summary>
        /// Functiont to check if the dealer has hit 17 or not
        /// </summary>
        public void checkDealer()
        {
            int currScore = calcScore(dealer);
            while (currScore <= 17)
            {
                Card newCard = dealCard();
                dealer.Hand.AddCard(newCard);
                currScore = calcScore(dealer);
                dealer.Score = currScore;
            }

        }

        /// <summary>
        /// Check if the dealer score has hit 17 or above
        /// </summary>
        /// <returns></returns>
        public bool dealerCount()
        {
            int dealerScore = calcScore(dealer);
            if (dealerScore >= 17){
                return true;
            } else return false;
        }

        // <summary>
        /// Deals a card to the dealer and returns this card
        /// </summary>
        /// <returns></returns>
        public Card dealCard()
        {
            Card card = dealer.drawCard();

            tempCard = card;

            return card;
        }

        public Dealer TheDealer
        {
            get { return this.dealer; }
        }

        public Player getPlayerOne
        {
            get { return players[0]; }
        }

        /// <summary>
        /// Uses lambda to find a players based on ID
        /// </summary>
        /// <param name="playerID">id of the player</param>
        /// <returns>found player with this id</returns>
        public Player getPlayerByID(int playerID)
        {
            Player result = players.First(obj => obj.ID == playerID);

            return result;

        }

        /// <summary>
        /// Adds a card to the dealers hand
        /// </summary>
        public void addCardDealer()
        {
            dealer.Hand.AddCard(dealCard());
        }

        /// <summary>
        /// Clears list of players who hit stay button- called after one round
        /// </summary>
        public void clearStay()
        {
            stayList.Clear();
        }

        /// <summary>
        /// When the deal starts, dealer gives every player 2 cards faceup
        /// and gives himself one facedown card and one faceup
        /// </summary>
        public void StartDeal()
        {
            
            // Loopa för alla spelare
            foreach (Player p in players)
            {
                p.Hand.AddCard(dealCard());
                p.Hand.AddCard(dealCard());
                // NEW CODE
                repository.InsertPlayer(p);
            }


            dealer.Hand.AddCard(dealCard()); // facedown
            dealer.Hand.AddCard(dealCard());

        }

        public void Addbet(int playerID, int betValue)
        {
            Player foundPlayer = players.FirstOrDefault(p => p.ID == playerID);

            int currentPot = foundPlayer.Bet;
            int newPot = foundPlayer.Bet + betValue;

            int money = getPlayerMoney(foundPlayer.Name);
            //int moneyValue;
            //Int32.TryParse(money, out moneyValue);
            if (newPot <= money)
            {
                foundPlayer.Bet = newPot;
            }
            
        }

        public List<Tuple<int, string>> HighScoreList()
        {
            return repository.getScoreTable();
        }

        public int getTotalBet(int playerID)
        {
            Player foundPlayer = players.FirstOrDefault(p => p.ID == playerID);

            return foundPlayer.Bet;
            
        }

        public int getPlayerMoney(String playerName)
        {
            return repository.getPlayerMoney(playerName);
        }

        public int getPlayerWinHand(String playerName)
        {
            return repository.getPlayerScore(playerName);
        }

        /// <summary>
        /// When hit is called during game. Give player a new card
        /// </summary>
        /// <param name="chosenID">id of the player who should recieve card</param>
        /// <returns>the updated Hand with a new card</returns>
        public Hand hit(int chosenID)
        {
            Player foundPlayer = players.FirstOrDefault(p => p.ID == chosenID);
            if (foundPlayer != null)
            {
                foundPlayer.Hand.AddCard(dealCard());
                return foundPlayer.Hand;
            }
            else return null;
            
        }


        /// <summary>
        /// When player wants to stay, add him to list of stayers
        /// </summary>
        /// <param name="stayPlayer">player who wants to Stay</param>
        public void stay(Player stayPlayer)
        {            
            stayList.Add(stayPlayer);
        }

        /// <summary>
        /// Check the list of stayers, if all players made a choice, continue
        /// </summary>
        /// <returns>if dealer should proceed with dealing cards or not</returns>
        public bool checkProceed()
        {
            if (stayList.Count != players.Count)
            {
                return false;
            }
            else return true;
        }

        /// <summary>
        /// When a player lost or won, they should be removed from the list of stayers
        /// and from the players list.
        /// </summary>
        /// <param name="playerID">Id of player who should be removed</param>
        public void removePlayer(int playerID)
        {
            stayList.RemoveAll(p => p.ID == playerID);
            players.RemoveAll(p => p.ID == playerID);
        }



        public Card getTempCard()
        {
            return tempCard;
        }

        /// <summary>
        /// Check if player should win / lose / continue
        /// </summary>
        /// <param name="playerStatus">Player to compare</param>
        /// <returns>If should continue or not</returns>
        public String checkStatus(Player playerStatus)
        {
            int status = compareDealer(playerStatus, dealer);

            return Helper.getStatus(status);
        }

        public void Payout(String status, int PlayerID)
        {
            Player foundPlayer = players.FirstOrDefault(p => p.ID == PlayerID);
            int finalbet = foundPlayer.Bet;
            int playerMoney = getPlayerMoney(foundPlayer.Name);
            switch (status)
            {
                case "LOSE":
                    int finalMoney = playerMoney - finalbet;
                    repository.updatePlayerMoney(foundPlayer, finalMoney);
                    // update player money, get money - minalbet

                    break;
                case "WIN":
                    int winMoney = finalbet * 2;
                    int newWinTotal = playerMoney + winMoney;
                    repository.updatePlayerMoney(foundPlayer, newWinTotal);
                    //UPDATE PLAYER SCORE
                    repository.updatePlayerScore(foundPlayer);                    

                    break; // Double the bet and add to player money
            }
        }

        /// <summary>
        /// When game is done, check Player VS Dealer
        /// </summary>
        /// <param name="currPlayer">player to compare to</param>
        /// <returns>win or lose</returns>
        public String competeDealer(Player currPlayer)
        {
            dealer.Score = calcScore(dealer);
            currPlayer.Score = calcScore(currPlayer);

            bool win;

            if (currPlayer.Score > dealer.Score || dealer.Score > limit)
            {
                win = true;
            } else
            {
                win = false;
            }

            if (win)
            {
                return "WIN";
            }
            else return "LOSE";
        }

        /// <summary>
        /// When a player is finished, add this to the list of players finished
        /// </summary>
        public void addFinish()
        {
            finishCount++;
        }

        /// <summary>
        /// When all players have finished, the game is finished
        /// </summary>
        /// <returns>if round is finished or not</returns>
        public bool GameFinished()
        {
            if (finishCount == playerCount)
            {
                return true;
            }
            else return false;
        }

    }
}
