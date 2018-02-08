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
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace BlackJack_Database
{
    /*  DAL - data access layer
 *  creates a database used by the Business Layer repository
 *  using DBContext
*/
    /// <summary>
    /// Player contains a one-to-one relation to Score
    /// </summary>
    public class Player
    {
        public int PlayerId { get; set; }

        public String Name { get; set; }

        public int Money { get; set; }

        public virtual Score Score { get; set; }

    }

    /// <summary>
    /// Score contains a one-to-one relation to Player
    /// </summary>
    public class Score
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public virtual Player Player { get; set; }

        public int CountValue { get; set; }
        public List<int> Handvalue { get; set; }
    }

    /// <summary>
    /// Class extending DBContext.
    /// This creates the database
    /// </summary>
    public class ScoringContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}
