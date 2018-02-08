/// <summary>
/// Black Jack Game
/// Assignment 1 - C# III Course
/// David Hegardt
/// 2017-10-09
/// </summary>

using System;
using System.Collections.Generic;
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
using GameCardLibrary;

namespace Black_Jack
{
    /// <summary>
    /// Publisher class - the GUI for each player.
    /// Publishes events sent to the Subscriber class
    /// </summary>
    public partial class PlayerWindow : Window
    {
        private Hand playerHand;
        private String playerName;
        private int playerScore;
        private int playerID;
        private String Status;
        private int currMoney;
        private bool end;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="playerHand">Hand of cards for this player</param>
        /// <param name="playerName">Name of player</param>
        /// <param name="playerScore">Initial player score</param>
        /// <param name="playerID">Id of this player</param>
        public PlayerWindow(Hand playerHand, String playerName, int playerScore, int playerID)
        {
            InitializeComponent();
            this.playerHand = playerHand;
            this.playerName = playerName;
            this.playerScore = playerScore;
            this.playerID = playerID;
            initGUI();
        }

        public void initGUI()
        {
            populateHand(playerHand.GetHand);
            lblPlayerOne.Content = playerName;
            lblPlayerOneScore.Content = "Score : " + playerScore;
        }

        public event EventHandler<HitEvent> hitEvent;                               // Register EventHandlers for events to be sent
        public event EventHandler<StayEvent> stayEvent;
        public event EventHandler<EndEvent> endEvent;
        public event EventHandler<BetEvent> betEvent;

        /// <summary>
        /// Shows cards in the GUI
        /// </summary>
        /// <param name="card">Card gui component to be displayed</param>
        /// <param name="imgLink">Link of image to assign</param>
        private void showCard(Image card, String imgLink)
        {
            card.Visibility = Visibility.Visible;
            var uriSource = new Uri(@"/Images/" + imgLink, UriKind.Relative);
            card.Source = new BitmapImage(uriSource);
        }

        public Hand Playerhand
        {
            get { return this.playerHand; }
            set
            {
                this.playerHand = value;
            }
        }

        public int Money
        {
            get { return this.currMoney; }
            set
            {
                this.currMoney = value;
            }
        }

        public int ID
        {
            get { return this.playerID; }
            set
            {
                this.playerID = value;
            }
        }

        public int Score
        {
            get { return this.playerScore; }
            set
            {
                this.playerScore = value;
            }
        }

        public void Update()
        {
            initGUI();
        }

        /// <summary>
        /// Show the correct card in the GUI based on hand
        /// </summary>
        /// <param name="currentHand">List of cards to be displayed</param>
        private void populateHand(List<Card> currentHand)
        {
            if (currentHand.Count > 0)
            {
                showCard(firstCard, currentHand[0].FaceUp);
            }

            if (currentHand.Count > 1)
            {
                showCard(secondCard, currentHand[1].FaceUp);
            }

            if (currentHand.Count > 2)
            {
                showCard(thirdCard, currentHand[2].FaceUp);
            }

            if (currentHand.Count > 3)
            {
                showCard(fourCard, currentHand[3].FaceUp);
            }

            if (currentHand.Count > 4)
            {
                showCard(fiveCard, currentHand[4].FaceUp);
            }
        }

        /// <summary>
        /// If player is no longer in the game
        /// </summary>
        public bool End
        {
            get { return this.end; }
        }

        /// <summary>
        /// Updates GUI based on current status of game.
        /// If player has won or lost, controls should be locked
        /// </summary>
        /// <param name="currStatus">Win / Lose / Continue</param>
        public void checkStatus(String currStatus)
        {
            bool end = false;

            Status = currStatus;

            switch (currStatus)
            {
                case "LOSE":
                    lblStatus.Content = "YOU LOSE!"; lblStatus.Visibility = Visibility.Visible; end = true;
                    break;
                case "WIN":
                    lblStatus.Content = "WINNING! YOU WON"; lblStatus.Visibility = Visibility.Visible; end = true;
                    break;
                case "CONTINUE":
                    end = false;
                    break;
            }

            endGame(end);
        }

        /// <summary>
        /// If the player has won or lost, send a Endevent
        /// </summary>
        /// <param name="end">Should game for this player end or not</param>
        private void endGame(bool end)
        {
            if (end)
            {
                toggleButtons(false);
                EndEvent newEnd = new EndEvent(playerID, Status, Score);
                OnEndEvent(newEnd);
            } else
            {
                toggleButtons(true);
            }
        }

        public void toggleButtons(bool onoff)
        {
            btnHit.IsEnabled = onoff;
            btnStay.IsEnabled = onoff;
        }

        public void updateMoney(int newMoney)
        {
            lblPlayerMoney.Content = newMoney + "$";
        }

        public void updateWinHand(int countValue)
        {
            lblWinScore.Content = "" + countValue.ToString();
        }

        /// <summary>
        /// When user presses hit button, create a Hit-event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHit_Click(object sender, RoutedEventArgs e)
        {
            HitEvent newHit = new HitEvent(playerHand, playerID);

            OnHitEvent(newHit);

        }

        public void OnEndEvent(EndEvent e)
        {
            if (endEvent != null)
            {
                endEvent(this, e);
                end = true;
            }

        }

        public void OnBetEvent(BetEvent e)
        {
            if(betEvent != null)
            {
                betEvent(this, e);
            }
        }

        public void OnHitEvent(HitEvent e)
        {
            if (hitEvent != null)
            {
                hitEvent(this, e);
            }
        }

        public String Player
        {
            get { return this.playerName; }
        }

        /// <summary>
        /// When user presses the stay button, send a Stay event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStay_Click(object sender, RoutedEventArgs e)
        {
            StayEvent newStay = new StayEvent(playerHand, playerID);

            OnStayEvent(newStay);
        }

        public void OnStayEvent(StayEvent e)
        {
            if(stayEvent != null)
            {
                stayEvent(this, e);
            }
        }

        private static int OneBet = 1;
        private static int FiveBet = 5;
        private static int TenBet = 10;
        private static int Bet25 = 25;
        private static int Bet50 = 50;
        private static int Bet150 = 150;

        private void betOne_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BetEvent newBet = new BetEvent(OneBet, Money, playerID);

            OnBetEvent(newBet);
        }

        public void updateBet(String newTotal)
        {
            lblBet.Content = "Bet : " + newTotal;
        }

        private void betFive_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BetEvent newBet = new BetEvent(FiveBet, Money, playerID);

            OnBetEvent(newBet);
        }

        private void bet150_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BetEvent newBet = new BetEvent(Bet150, Money, playerID);

            OnBetEvent(newBet);
        }

        private void betFifty_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BetEvent newBet = new BetEvent(Bet50, Money, playerID);

            OnBetEvent(newBet);
        }

        private void bet25_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BetEvent newBet = new BetEvent(Bet25, Money, playerID);

            OnBetEvent(newBet);
        }

        private void betTen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BetEvent newBet = new BetEvent(TenBet, Money, playerID);

            OnBetEvent(newBet);
        }
    }
}
