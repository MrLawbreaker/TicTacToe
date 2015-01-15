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

namespace TicTackToe
{
    public partial class MainWindow : Window
    {
        PlayField pField;

        Dictionary<String, Button> btn_dictionary;

        Player player1;
        Player player2;
        Player currentPlayer;

        public MainWindow()
        {
            InitializeComponent();
            init();
            initDic();
        }

        private void init()
        {
            pField = new PlayField();

            //Initialize players
            player1 = new CrossPlayer();
            player2 = new TickPlayer();
            currentPlayer = player1;

            //Set starting message
            txt_Message.Text = String.Format("Player {0} begins!", Enums.PlaySymbolToString(currentPlayer.symbol));
        }

        private void initDic(){
            #region Buttons to Dictionary
            //Adding all buttons to a dictionary for easier use later on
            btn_dictionary = new Dictionary<String, Button>();
            btn_dictionary.Add("btn_00", btn_00);
            btn_dictionary.Add("btn_10", btn_10);
            btn_dictionary.Add("btn_20", btn_20);

            btn_dictionary.Add("btn_01", btn_01);
            btn_dictionary.Add("btn_11", btn_11);
            btn_dictionary.Add("btn_21", btn_21);

            btn_dictionary.Add("btn_02", btn_02);
            btn_dictionary.Add("btn_12", btn_12);
            btn_dictionary.Add("btn_22", btn_22);
            #endregion
        }

        private void MakeTurn(byte x, byte y)
        {
            //Set the field to show the players input
            if (pField.SetField(x, y, currentPlayer.symbol))
            {
                Button btn = null;
                if (btn_dictionary.TryGetValue("btn_" + x + "" + y, out btn))
                {
                    btn.Content = Enums.PlaySymbolToString(currentPlayer.symbol);
                }
                //If the player makes the winning move, then end the game
                if (!pField.gameState.Equals(Enums.GameState.IN_PROGRESS))
                    ShowEndScreen();
                else
                {
                    SwitchPlayer();
                }
            }
            
        }

        private void ShowEndScreen()
        {
            //Disable all buttons
            Button btn = null;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (btn_dictionary.TryGetValue("btn_" + i + "" + j, out btn))
                    {
                        btn.IsEnabled = false;
                        if (pField.gameState.Equals(Enums.GameState.DRAW))
                        {
                            txt_Message.Text = "The game has ended in a draw!";
                        }
                        else
                        {
                            txt_Message.Text = String.Format("Congratulations player {0}, you have won!", Enums.PlaySymbolToString(currentPlayer.symbol));
                        }
                    }

                }
            }
        }

        private void Reset()
        {
            //Enable all Buttons
            Button btn = null;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (btn_dictionary.TryGetValue("btn_" + i + "" + j, out btn))
                    {
                        btn.IsEnabled = true;
                        btn.Content = "";
                    }

                }
            }
            currentPlayer = player1;
            txt_Message.Text = String.Format("Player {0} begins!", Enums.PlaySymbolToString(currentPlayer.symbol));
            pField.ResetField();

        }

        private void SwitchPlayer()
        {
            if (currentPlayer.Equals(player1))
                currentPlayer = player2;
            else
                currentPlayer = player1;

            txt_Message.Text = String.Format("Player {0}, it is your turn!", Enums.PlaySymbolToString(currentPlayer.symbol));
        }


        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        #region Btn_Click Event Handler
        private void btn_00_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(0, 0);
        }

        private void btn_10_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(1, 0);
        }
        
        private void btn_20_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(2, 0);
        }

        private void btn_01_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(0, 1);
        }

        private void btn_11_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(1, 1);
        }

        private void btn_21_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(2, 1);
        }

        private void btn_02_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(0, 2);
        }

        private void btn_12_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(1, 2);
        }

        private void btn_22_Click(object sender, RoutedEventArgs e)
        {
            MakeTurn(2, 2);
        }
        #endregion
    }
}
