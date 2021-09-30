using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        private List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        private List<TeamModel> selectedTeams = new List<TeamModel>();
        private List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        private ITournamentRequester callingForm;

        public CreateTournamentForm(ITournamentRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            WireUpLists();
        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = nameof(TeamModel.TeamName);

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = nameof(TeamModel.TeamName);

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = nameof(PrizeModel.PlaceName);
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            // Call the CreatePrizeForm
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.ShowDialog();
        }

        public void PrizeComplete(PrizeModel model)
        {
            // Get back from the form a PrizeModel
            // Take the PrizeModel and put it into our list of selected prizes
            selectedPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            // Get back from the form a TeamModel
            // Take the TeamModel and put it into our list of selected teams
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Call the CreateTeamForm
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.ShowDialog();
        }

        private void removeSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;

            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);

                WireUpLists();
            }
        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);

                WireUpLists();
            }
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            // Validate form
            string errorMessage = ValidateForm();

            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage);
                return;
            }

            // Create our tournament model
            TournamentModel tm = new TournamentModel();
            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = decimal.Parse(entryFeeValue.Text);
            tm.InitialPrizePool = decimal.Parse(prizePoolValue.Text);
            tm.GreaterScoreWins = greaterScoreWinsCheckBox.Checked;

            tm.Prizes = selectedPrizes;
            tm.EnteredTeams = selectedTeams;

            // Generate matchups
            TournamentLogic.CreateRounds(tm);

            // Create tournament entry
            // Create all of the prizes entries
            // Create all of the team entries
            GlobalConfig.Connection.CreateTournament(tm);

            tm.AlertUsersToNewRound();
            
            this.Close();

            callingForm.TournamentComplete(tm, true);

        }

        private string ValidateForm()
        {
            StringBuilder output = new StringBuilder();

            if (tournamentNameValue.Text.Length == 0)
            {
                output.AppendLine("Invalid tournament name.");
            }

            if (!decimal.TryParse(entryFeeValue.Text, out decimal fee) || fee < 0)
            {
                output.AppendLine("Invalid entry fee.");
            }

            if (!decimal.TryParse(prizePoolValue.Text, out decimal prizePool) || prizePool < 0)
            {
                output.AppendLine("Invalid initial prize pool.");
            }

            if (selectedTeams.Count < 2)
            {
                output.AppendLine("You need to have at least 2 teams in a tournament.");
            }

            if (fee == 0 && selectedPrizes.Count > 0)
            {
                output.AppendLine("You cannot have prizes without an entry fee.");
            }

            decimal totalPrizePool = prizePool + selectedTeams.Count * fee;
            // PrizeAmount total
            decimal totalPrizeMoney = selectedPrizes.Aggregate(0M, (total, current) => total + current.PrizeAmount);
            // total of applied PrizePercentage
            totalPrizeMoney += Decimal.Multiply(Convert.ToDecimal(selectedPrizes.Aggregate(0D, (total, current) => total + current.PrizePercentage)), totalPrizePool) / 100;

            if (totalPrizeMoney > totalPrizePool)
            {
                output.AppendLine("The sum of prizes is larger than the total prize pool.");
            }

            if (selectedPrizes.Any(x => x.PlaceNumber > selectedTeams.Count))
            {
                output.AppendLine("You have a prize with an invalid place number.");
            }

            if (selectedPrizes.Count > selectedTeams.Count)
            {
                output.AppendLine("You cannot have more prizes than teams.");
            }

            return output.ToString();
        }
    }
}
