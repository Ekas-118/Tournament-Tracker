using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class PrizeDisplayForm : Form
    {
        private TournamentModel tournament;

        public PrizeDisplayForm(TournamentModel model)
        {
            InitializeComponent();

            tournament = model;

            DisplayPrizes();
        }

        private void DisplayPrizes()
        {
            StringBuilder displayText = new StringBuilder();

            List<PrizeModel> prizes = tournament.Prizes.OrderBy(x => x.PlaceNumber).ToList();
            decimal prizePool = tournament.InitialPrizePool + tournament.EntryFee * tournament.EnteredTeams.Count;

            MatchupModel final = tournament.Rounds.Last().Single();
            TeamModel winner = final.Winner;
            TeamModel runnerUp = final.Entries.Single(x => x.TeamCompeting.Id != winner.Id).TeamCompeting;

            foreach (PrizeModel prize in prizes)
            {
                StringBuilder line = new StringBuilder();
                line.Append($"#{prize.PlaceNumber} ({prize.PlaceName}) - ");
                switch (prize.PlaceNumber)
                {
                    case 1:
                        line.Append($"{winner.TeamName} - ");
                        break;
                    case 2:
                        line.Append($"{runnerUp.TeamName} - ");
                        break;
                    default:
                        break;
                }
                line.Append($"${prize.PrizeAmount + decimal.Multiply(Convert.ToDecimal(prize.PrizePercentage), prizePool) / 100}");
                displayText.AppendLine(line.ToString());
            }

            prizeListValues.Text = displayText.ToString();
        }
    }
}
