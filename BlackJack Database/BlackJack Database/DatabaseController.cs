/// <summary>
/// Data Access Layer
/// Assignment 2 - C# III Course
/// David Hegardt
/// 2017-10-09
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_Database
{
    /*  DAL - data access layer
     *  Contains CRUD methods to manage the database
     *  
    */
    public static class DatabaseController
    {
        public static Player findPlayerByName(String pName)
        {
            using (var context = new ScoringContext())
            {
                var player = context.Players
                    .Where(p => p.Name == pName)
                    .FirstOrDefault();

                return player;
            }
        }

        public static int getPlayerId(String pName)
        {
            using (var context = new ScoringContext())
            {
                var player = context.Players.Where(p => p.Name == pName).FirstOrDefault();

                return player.PlayerId;
            }
        }

        public static void clearAll()
        {
            using (var context = new ScoringContext())
            {
                context.Scores.RemoveRange(context.Scores);
                context.Players.RemoveRange(context.Players);

                context.SaveChanges();
            }
        }

        public static List<Tuple<int, string>> getScoreValues()
        {
            var myList = new List<Tuple<int, string>>();
            using (var db = new ScoringContext())
            {
                var scoreQuery = from s in db.Scores
                                 orderby s.CountValue descending
                                 select s;

                foreach (var item in scoreQuery)
                {
                    myList.Add(Tuple.Create(item.CountValue, item.Player.Name));
                }
            }
            return myList;
        }

        public static void deletePlayer(int playerID)
        {
            using (var context = new ScoringContext())
            {
                var result = context.Scores.SingleOrDefault(p => p.UserID == playerID);
                var player = context.Players.SingleOrDefault(v => v.PlayerId == playerID);

                context.Scores.Remove(result);

                context.Players.Remove(player);

                context.SaveChanges();
            }
        }

        public static int getPlayerScore(String pName)
        {

            using (var context = new ScoringContext())
            {
                var d = context.Players.Where(o => o.Name == pName).Select(o => o.Score.CountValue).FirstOrDefault();

                return d;

            }
        }

        public static int getPlayerMoney(String pName)
        {
            using (var context = new ScoringContext())
            {
                var money = context.Players.Where(o => o.Name == pName).Select(o => o.Money).FirstOrDefault();

                return money;

            }
        }

        public static int getHighestScore()
        {
            using (var db = new ScoringContext())
            {
                int highest = db.Scores.Max(u => u.CountValue);

                return highest;
            }
        }

        public static void updatePlayerScore(int playerID, Score newScore)
        {
            using (var db = new ScoringContext())
            {
                var result = db.Scores.SingleOrDefault(p => p.UserID == playerID);
                if (result != null)
                {
                    db.Entry(result).State = System.Data.Entity.EntityState.Modified;

                    result.Player.Score = newScore;

                    db.SaveChanges();
                }
            }
        }

        public static void updatePlayerMoney(String playerName, int pMoney)
        {
            var foundPlayer = findPlayerByName(playerName);

            using (var db = new ScoringContext())
            {
                db.Entry(foundPlayer).State = System.Data.Entity.EntityState.Modified;

                foundPlayer.Money = pMoney;

                db.SaveChanges();
            }
        }

        public static Score createPlayerScore(List<int> Hand, int ScoreValue)
        {
            Score newScore = new Score { CountValue = ScoreValue, Handvalue = Hand };

            return newScore;
        }

        public static bool addPlayer(String playerName, Score playerScore, int playerMoney)
        {
            Player newPlayer = new Player { Name = playerName, Score = playerScore, Money = playerMoney };

            using (var db = new ScoringContext())
            {
                var result = db.Players.SingleOrDefault(p => p.Name == playerName);
                if (result == null)
                {
                    db.Players.Add(newPlayer);

                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}


