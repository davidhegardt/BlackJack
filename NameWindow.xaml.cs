/// <summary>
/// Black Jack Game
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Black_Jack
{
    /// <summary>
    /// Publisher class
    /// publishes events for stay hit and end.
    /// Window is both used for entering nr of players and player name
    /// </summary>
    public partial class NameWindow : Window
    {
        private String playerName;
        private String type;
        private int playerCount;

        /// <summary>
        /// Constructor, based on type either
        /// used for nr of players or player name
        /// </summary>
        /// <param name="ptype">Count or Name</param>
        public NameWindow(String ptype)
        {
            InitializeComponent();
            if (ptype.Equals("Count"))
            {
                initCount();
            } else if (ptype.Equals("Name"))
            {
                initName();
            }
            
        }

        private void initName()
        {
            this.Title = "Player Name";
            FieldInputType.Text = "Player Name";
            this.type = "Name";
        }

        private void initCount()
        {
            this.Title = "Number of Players";
            FieldInputType.Text = "Enter number of players:";
            this.type = "Count";
        }
        /// <summary>
        /// Ok Button click, if Nr of players is used, check for int
        /// if player name is used, check for string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (type.Equals("Name"))
            {

                playerName = InputName.Text;
                if (String.IsNullOrEmpty(playerName))
                {
                    MessageBox.Show("You need to enter name");
                }
                else
                {
                    this.playerName = InputName.Text.ToString();
                    Trace.WriteLine(playerName);
                    DialogResult = true;
                    this.Close();
                }

            } else if (type.Equals("Count"))
            {
                int parsedCount = 0;
                if (int.TryParse(InputName.Text, out parsedCount)){
                    this.playerCount = parsedCount;
                    DialogResult = true;
                    this.Close();

                } else {
                    MessageBox.Show("Invalid number!");
                    
                }
            }
        }

        

        public String PlayerName
        {
            get { return this.playerName; }
        }

        public int NrOfPlayers
        {
            get { return this.playerCount; }
        }
    }
}
