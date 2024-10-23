using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HafizaOyunu
{
    public partial class Form1 : Form
    {
        List<string> icons = new List<string>()
        {
            "!",",","b","k","v","w","z","N","M","s",    "x","c","V","B","n","a","d","f","g","j",
            "!",",","b","k","v","w","z","N","M","s",    "x","c","V","B","n","a","d","f","g","j",
        };
        Random rnd = new Random();
        int randomindex;

        Timer t = new Timer();
        Timer t2 = new Timer();
        Timer firstSelectionTimer = new Timer();

        Button first, second;

        // Player-related variables
        int playerTurn = 1;
        int player1Score = 0;
        int player2Score = 0;

        public Form1()
        {
            InitializeComponent();
            t.Tick += T_Tick;
            t.Start();
            t.Interval = 10000;
            show();
            t2.Tick += T2_Tick;

            firstSelectionTimer.Interval = 5000;
            firstSelectionTimer.Tick += FirstSelectionTimer_Tick;
        }

        private void FirstSelectionTimer_Tick(object sender, EventArgs e)
        {
            first.ForeColor = first.BackColor;
            first = null;
            firstSelectionTimer.Stop();
            SwitchTurns();
        }

        private void T2_Tick(object sender, EventArgs e)
        {
            t2.Stop();
            first.ForeColor = first.BackColor;
            second.ForeColor = second.BackColor;
            first = null;
            second = null;

            // Switch turns
            SwitchTurns();
        }

        private void T_Tick(object sender, EventArgs e)
        {
            t.Stop();
            foreach (Button item in Controls)
            {
                item.ForeColor = item.BackColor;
            }
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void show()
        {
            Button btn;
            foreach (Button item in Controls)
            {
                btn = item as Button;
                randomindex = rnd.Next(icons.Count);
                btn.Text = icons[randomindex];
                btn.ForeColor = Color.Black;
                icons.RemoveAt(randomindex);
            }
        }

        int matchCount = 0;

        private void buton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (first == null)
            {
                first = btn;
                first.ForeColor = Color.Black;

                firstSelectionTimer.Start();
                return;
            }

            firstSelectionTimer.Stop();

            second = btn;
            second.ForeColor = Color.Black;

            if (first.Text == second.Text)
            {
                first.ForeColor = Color.Black;
                second.ForeColor = Color.Black;
                first = null;
                second = null;
                matchCount++;

                // Add points to the current player
                if (playerTurn == 1)
                {
                    player1Score++;
                    if (player1Score == 11)
                    {
                        DisplayGameOver();
                    }

                }
                else
                {
                    player2Score++;
                    if (player2Score == 11)
                    {
                        DisplayGameOver();
                    

                        }
                    }

                // Check if all matches are found
                if (matchCount == 20)
                {
                    DisplayGameOver();
                }
            }
            else
            {
                t2.Start();
                t2.Interval = 1000;
            }
        }

        private void SwitchTurns()
        {
            if (playerTurn == 1)
            {
                playerTurn = 2;
                Text = "oyuncu 2";
                
            }
            else
            {
                playerTurn = 1;
                Text = "oyuncu 1";
               
            }
        }

        private void DisplayGameOver()
        {
            // Stop the game and display the final scores
            string message = $"sonuçlar!\noyuncu 1 Score: {player1Score}\noyuncu 2 Score: {player2Score}";
            MessageBox.Show(message);
        }

       
    }
}
