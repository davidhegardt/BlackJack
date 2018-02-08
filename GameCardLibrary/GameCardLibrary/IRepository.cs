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
    public interface IRepository
    {
        void InsertPlayer(Player player);
        void updatePlayerScore(Player player);
        void updatePlayerMoney(Player player, int money);
        void deletePlayer(String pName);
        int getPlayerScore(String player);
        List<Tuple<int, string>> getScoreTable();
    }
}
