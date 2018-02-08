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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameCardLibrary;
using System.Windows.Threading;
using System.Diagnostics;
using System.Media;
using System.Collections.ObjectModel;

namespace Black_Jack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Subscriber class - subscribes to events from NameWindow.
    /// Displays the main Game-window
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();
        private BlackJack currentGame;
        private bool gameDone = false;
        private List<PlayerWindow> playerWindows;
        private int playerCount;
        private bool faceUP = false;
        private List<String> playerNames;

        private readonly ObservableCollection<Tuple<int, string>> _scoretable = new ObservableCollection<Tuple<int, string>>();
        public ObservableCollection<Tuple<int,string>> Highscores { get { return _scoretable; } }


        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Intializes setup of the BlackJack-table
        /// </summary>
        public void Init()
        {
            currentGame = new BlackJack();                              // Starts new game logic for blackjack
            dealerCardImages = new List<Image>();                       // List of images for the dealers cards on the table
            playerWindows = new List<PlayerWindow>();                   // List of Windows for the players

            hideDealer();

            dealerCardImages.Add(dealerFirst);                          // Assign image-link to the window in the GUI
            dealerCardImages.Add(dealerSecond);
            dealerCardImages.Add(dealerThird);
            dealerCardImages.Add(dealerFour);
            dealerCardImages.Add(dealerFive);

            tempCard.Visibility = Visibility.Hidden;

            btnDeal.IsEnabled = false;
            var list = currentGame.HighScoreList();
            
            foreach (Tuple<int,string> t in list)
            {
                _scoretable.Add(t);
            }
            listBoxScore.ItemsSource = Highscores;
            
        }

        public void updateHighscores()
        {
            Highscores.Clear();
            var list = currentGame.HighScoreList();            

            foreach (Tuple<int, string> t in list)
            {
                _scoretable.Add(t);
            }

            

            listBoxScore.ItemsSource = Highscores;
        }

        /// <summary>
        /// Hides the dealers cards
        /// </summary>
        private void hideDealer()
        {
            dealerFirst.Visibility = Visibility.Hidden;
            dealerSecond.Visibility = Visibility.Hidden;
            dealerThird.Visibility = Visibility.Hidden;
            dealerFour.Visibility = Visibility.Hidden;
            dealerFive.Visibility = Visibility.Hidden;

            lblDealerScore.Content = "Dealer Score : ";

        }

        /// <summary>
        /// Function called to reset the game after one round is done
        /// </summary>
        private void reset()
        {
            playerWindows = new List<PlayerWindow>();
            faceUP = false;
            btnDeal.IsEnabled = true;
        }
/*
        public void animateCard(String currType,int nr)
        {
            if(currType == "player")
            {
                showCard(tempCard, currentGame.getPlayerOne.Hand.GetHand[nr].FaceUp);
            } else
            {
                showCard(tempCard, currentGame.TheDealer.Hand.GetHand[nr].FaceUp);
            }

        }
        */

/*
        public void delayStuff(String plyrType,int nr, Image placeHolder)
        {
            timer.Tick += (object s, EventArgs a) => Timer_finish(s, a, plyrType,nr,placeHolder);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        */
        /*
        private void Timer_finish(object sender, EventArgs e,String currType,int nr, Image where)
        {
            if (currType == "player")
            {
                showCard(where, currentGame.getPlayerOne.Hand.GetHand[nr].FaceUp);
            }
            else
            {
                showCard(where, currentGame.TheDealer.Hand.GetHand[nr].FaceUp);
            }
        }
        */
        private List<Image> dealerCardImages;


        private void showCard(Image card,String imgLink)
        {
            card.Visibility = Visibility.Visible;
            var uriSource = new Uri(@"/Images/" + imgLink, UriKind.Relative);
            card.Source = new BitmapImage(uriSource);
        }

        /// <summary>
        /// Function to display how many cards are left in the decks
        /// </summary>
        private void ShowCardsLeft()
        {
            lblDeckCount.Content = "Deck One left : " + currentGame.TheDealer.getDeckCount(0);
            lblDeck2Count.Content = "Deck Two left : " + currentGame.TheDealer.getDeckCount(1);


        }

        /// <summary>
        /// Initiates a new game
        /// </summary>
        /// <param name="Names">List of player names</param>
        public void newGame(List<String> Names)
        {            
            playerNames = Names;
            currentGame.startGame(playerNames.Count, Names);
            playerCount = Names.Count;
        }

        /// <summary>
        /// Initiates a new round when the Deal button is pressed
        /// </summary>
        public void sendDeal()
        {
            
            currentGame.StartDeal();                                    //Initiate game logic
            populateDealer(currentGame.TheDealer.Hand.GetHand);         // Update GUI of dealer cards
            String hand  = currentGame.TheDealer.Hand.ToString();

            // Loops all players and sets up new windows for each player
            for (int i = 0; i < playerCount; i++)
            {
                int score = currentGame.calcScore(currentGame.getPlayerByID(i));
                PlayerWindow newWindow = new PlayerWindow(currentGame.getPlayerByID(i).Hand, currentGame.getPlayerByID(i).Name, score, i);
                newWindow.Title = "Player " + currentGame.getPlayerByID(i);

                newWindow.Show();

                newWindow.hitEvent += OnHitEvent;                               //Register events
                newWindow.stayEvent += OnStayEvent;
                newWindow.endEvent += OnEndEvent;
                newWindow.betEvent += OnBetEvent;                           // NEW BET EVENT
                playerWindows.Add(newWindow);
                String status = currentGame.checkStatus(currentGame.getPlayerByID(i));          // Check if player won on first cards
                newWindow.checkStatus(status);
            }

            //-------------------------------------------------------------------------------------------------------------
            
            populateDealer(currentGame.TheDealer.Hand.GetHand);                     // Update dealer cards
            ShowCardsLeft();                                                // Show how many cards are left
            updateMoney();
            updateWinHand();
        }

        private void OnBetEvent(object sender, BetEvent e)
        {
            // LÄGG TILL BET FÖR SPELARE
            currentGame.Addbet(e.PlayerID, e.Bet);
            var currentWindow = playerWindows.First(obj => obj.ID == e.PlayerID);
            int bet = currentGame.getTotalBet(e.PlayerID);
            currentWindow.updateBet(bet.ToString());
        }

        /// <summary>
        /// Triggers when End event is recieved from player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnEndEvent(object sender, EndEvent e)
        {
            String status = currentGame.checkStatus(currentGame.getPlayerByID(e.PlayerID));
            Trace.WriteLine(status);

            // CALL FUNCTION FOR PAYOUT
            currentGame.Payout(status, e.PlayerID);
            updateMoney();

            currentGame.removePlayer(e.PlayerID);
            removeWindow(e.PlayerID);
            currentGame.addFinish();
            

            // SHOWS END SCORE AND STATUS FOR PLAYER

            bool nextCard = currentGame.checkProceed();                 // Check if game should continue
            if (nextCard)
            {
                dealerProceed();
            }

            if (currentGame.GameFinished())                                     // If all players are finished, show Game Over and reset game
            {
                lblFinished.Visibility = Visibility.Visible;            //GAME OVER
                currentGame.resetGame(playerNames);
                reset();
                
            }
        }

        /// <summary>
        /// Function to clear all child windows after each round
        /// </summary>
        /// <param name="winID"></param>
        private void removeWindow(int winID)
        {
            playerWindows.RemoveAll(p => p.ID == winID);
        }
                
        /// <summary>
        /// Triggers when Hit event is recieved from player-window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHitEvent(object sender, HitEvent e)
        {
            e.Playerhand = currentGame.hit(e.PlayerID); 
            var currentWindow = playerWindows.First(obj => obj.ID == e.PlayerID);               //Find window which sent hit event
            currentWindow.Playerhand = e.Playerhand;
            currentWindow.Score = currentGame.calcHandScore(e.Playerhand);                      // Calculate score of players hand
            currentWindow.Update();
            String status = currentGame.checkStatus(currentGame.getPlayerByID(e.PlayerID));     // Check if player has won or lost
            currentWindow.checkStatus(status);
            ShowCardsLeft();
        }

        /// <summary>
        /// Function to handle when dealer should give out more cards
        /// </summary>
        private void dealerProceed()
        {
            if (faceUP)                                     // If dealer has shown his cards, proceed to take new cards
            {

                if (currentGame.dealerCount())              // If dealers score is 17 >=
                {

                    WinCheck();                             // For each player, check who won or lost
                    return;
                }

                do
                {
                    currentGame.addCardDealer();                            // Continue to deal cards to dealer until he hits 17
                    populateDealer(currentGame.TheDealer.Hand.GetHand);
                } while (!currentGame.dealerCount());

            }
            else if (!faceUP)                               // If the dealer has not shown his first card, flip it
            {
                showCard(dealerCardImages[0], currentGame.TheDealer.Hand.GetHand[0].FaceUp);
                faceUP = true;
            }

            

            int dealerScore = currentGame.calcScore(currentGame.TheDealer);

            lblDealerScore.Content = "Dealer Score " + dealerScore;         // Show the dealers Score

            RoundCheck();                           // Check if all players are finished
           
        }
        /// <summary>
        /// Check if the round is finished or should proceed
        /// </summary>
        private void RoundCheck()
        {
            foreach (PlayerWindow win in playerWindows.ToList())
            {
                if (!win.End)
                {
                    String status = currentGame.checkStatus(currentGame.getPlayerByID(win.ID));
                    win.checkStatus(status);
                }
            }

            currentGame.clearStay();
        }

        public void updateMoney()
        {
            foreach(PlayerWindow win in playerWindows.ToList())
            {
                win.updateMoney(currentGame.getPlayerMoney(win.Player));
                win.Money = currentGame.getPlayerMoney(win.Player);
            }
        }

        public void updateWinHand()
        {
            foreach (PlayerWindow win in playerWindows.ToList())
            {
                win.updateWinHand(currentGame.getPlayerWinHand(win.Player));
                //win.Money = currentGame.getPlayerMoney(win.Player);
            }
        }

        /// <summary>
        /// After dealer has hit 17 or above, check who won and who lost
        /// </summary>
        private void WinCheck()
        {
            foreach (PlayerWindow win in playerWindows.ToList())
            {
                if (!win.End)
                {
                    Trace.WriteLine("Winning check for" + win.Player);
                    String status = currentGame.competeDealer(currentGame.getPlayerByID(win.ID));                    

                    // CALL FUNCTION FOR PAYOUT
                    currentGame.Payout(status, win.ID);
                    updateMoney();
                    win.checkStatus(status);
                }
            }

            currentGame.clearStay();
        }
        /// <summary>
        /// Triggers when Stay event is recieved from players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStayEvent(object sender,StayEvent e)
        {

            currentGame.stay(currentGame.getPlayerByID(e.PlayerID));
            bool nextCard = currentGame.checkProceed();
            if (nextCard)                                           // Proceed if under 21 or not lost
            {
                dealerProceed();

            } else
            {
                var currentWindow = playerWindows.First(obj => obj.ID == e.PlayerID);
                currentWindow.toggleButtons(false);
            }

        }


        /// <summary>
        /// Function to populate the Dealers hand in the GUI
        /// </summary>
        /// <param name="currentHand">Which cards should be shown</param>
        private void populateDealer(List<Card> currentHand)
        {
            if (currentHand.Count > 0)
            {
                if (!faceUP)
                {
                    showCard(dealerCardImages[0], currentHand[0].FaceDown);
                } else
                {
                    showCard(dealerCardImages[0], currentHand[0].FaceUp);
                }
            }

            if (currentHand.Count > 1)
            {
                showCard(dealerCardImages[1], currentHand[1].FaceUp);
            }

            if (currentHand.Count > 2)
            {
                showCard(dealerCardImages[2], currentHand[2].FaceUp);
            }

            if (currentHand.Count > 3)
            {
                showCard(dealerCardImages[3], currentHand[3].FaceUp);
            }

            if (currentHand.Count > 4)
            {
                showCard(dealerCardImages[4], currentHand[4].FaceUp);
            }
        }

        private bool flipped = false;

        /// <summary>
        /// When the Deal-button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeal_Click(object sender, RoutedEventArgs e)
        {
            closeWindows();                                             // Close any open windows, this is a new round
            hideDealer();                                               // Reset dealers hand
            sendDeal();                                                 // Start the deal-round
            lblFinished.Visibility = Visibility.Hidden;                 
            btnShuffle.IsEnabled = true;
        }

        /// <summary>
        /// Initiates start of new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            List<String> playerNames = setupWindow();
            btnDeal.IsEnabled = true;
            newGame(playerNames);
            btnNewGame.IsEnabled = false;
        }

        private void closeWindows()
        {
            foreach (Window win in App.Current.Windows)
            {
                if (!(win is MainWindow))
                {
                    win.Close();
                }
            }
        }

        /// <summary>
        /// Sets up a new player window is shown
        /// </summary>
        /// <returns>A list of all players names</returns>
        private List<String> setupWindow()
        {
            NameWindow countWindow = new NameWindow("Count");

            int playerCount;
            List<String> Names = new List<string>();
            

            if(countWindow.ShowDialog() == true)
            {
                playerCount = countWindow.NrOfPlayers;

                if (playerCount > 0)
                {
                    for (int i = 0; i < playerCount; i++)
                    {
                        NameWindow nameWindow = new NameWindow("Name");
                        if(nameWindow.ShowDialog() == true)
                        {
                            String currName = nameWindow.PlayerName;
                            Trace.WriteLine(currName);
                            Names.Add(currName);
                        }
                    }
                }
            }

            return Names;
        }
        /// <summary>
        /// Instantly shuffles the decks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            currentGame.TheDealer.shuffleDeck();
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var index = listBoxScore.SelectedIndex;
            if (index != -1)
            {
                var playername = _scoretable[index].Item2;
                currentGame.deletePlayer(playername);
                updateHighscores();
            }
        }
    }
}
