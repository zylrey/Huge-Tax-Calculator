using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HUGE_tax_calculator
{
    public partial class Form1 : Form
    {
        // Tax rate in decimal (10% = 0.10)
        private const decimal taxRate = 0.10m;
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Set default number of players (can be changed by the user)
            cbPlayers.SelectedIndex = 0;

            // Clear previous results and winner display
            ClearResults();
        }

        private void ClearResults()
        {
            // Clear player bet TextBox, winner display, and pot label
            txtBet.Text = "";
            lblWin.Text = "";
            lblPot.Text = "";
            lblTax.Text = "";
        }

        private void CalculateWinner()
        {
            // Parse player bet from TextBox
            if (decimal.TryParse(txtBet.Text, out decimal bet))
            {
                int numPlayers = int.Parse(cbPlayers.SelectedItem.ToString());

                // Calculate the total pot (sum of all bets)
                decimal pot = bet * numPlayers;
                lblPot.Text = $"{Math.Round(pot, 0)} WLS";

                // Calculate the total tax amount collected from all players
                decimal totalTax = (numPlayers - 1) * (bet * taxRate);
                lblTax.Text = $"{Math.Round(totalTax, 0)} WLS";

                // Calculate the winner's result after applying tax to other players
                decimal winnerResult = pot - totalTax;

                lblWin.Text = $"{Math.Round(winnerResult, 0)} WLS";
            }
            else
            {
                MessageBox.Show("Invalid bet! Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CalculateWinner();
        }

        private void cbPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearResults();
        }

        private void BtnClear_Click_1(object sender, EventArgs e)
        {
            ClearResults();
        }

        private void TxtBet_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Perform the calculation when Enter is pressed
                CalculateWinner();

                // Prevent further handling of the Enter key press event
                e.Handled = true;
            }
        }
    }
}
