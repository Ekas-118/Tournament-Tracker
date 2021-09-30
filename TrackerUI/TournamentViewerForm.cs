using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;
        private BindingList<int> rounds = new BindingList<int>();
        private BindingList<MatchupModel> selectedMatchups = new BindingList<MatchupModel>();
        private ITournamentRequester callingForm;

        public TournamentViewerForm(TournamentModel tournamentModel, ITournamentRequester caller)
        {
            InitializeComponent();

            tournament = tournamentModel;

            tournament.OnTournamentComplete += Tournament_OnTournamentComplete;

            callingForm = caller;

            WireUpLists();

            LoadFormData();

            LoadRounds();

            DisplayPrizeButton();
        }

        private void DisplayPrizeButton()
        {
            if (!tournament.Active && tournament.Prizes.Count > 0)
            {
                prizesButton.Visible = true;
            }
        }

        private void Tournament_OnTournamentComplete(object sender, TeamModel t)
        {
            tournamentWinnerLabel.Visible = true;
            tournamentWinnerValue.Text = t.TeamName;
            tournamentWinnerValue.Visible = true;

            DisplayPrizeButton();

            MessageBox.Show($@"Team ""{t.TeamName}"" has won the tournament!");
        }

        private void LoadFormData()
        {
            tournamentNameLabel.Text = tournament.TournamentName;

            if (!tournament.Active)
            {
                tournamentWinnerLabel.Visible = true;
                tournamentWinnerValue.Text = tournament.Rounds.Last().Single().Winner.TeamName;
                tournamentWinnerValue.Visible = true;
            }
        }

        private void WireUpLists()
        {
            roundDropDown.DataSource = rounds;
            matchupListBox.DataSource = selectedMatchups;
            matchupListBox.DisplayMember = nameof(MatchupModel.DisplayName);
        }

        private void LoadRounds()
        {
            rounds.Clear();

            rounds.Add(1);
            int currRound = 1;

            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound > currRound)
                {
                    rounds.Add(++currRound);
                }
            }

            LoadMatchups(1);
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void LoadMatchups(int round)
        {
            foreach (List<MatchupModel> matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    selectedMatchups.Clear();
                    foreach (MatchupModel m in matchups)
                    {
                        if (m.Winner == null || !unplayedOnlyCheckBox.Checked)
                        {
                            selectedMatchups.Add(m);
                        }
                    }
                }
            }

            DisplayControls();

            if (selectedMatchups.Count > 0)
            {
                LoadMatchup(selectedMatchups.First());
            }
        }

        private void DisplayControls()
        {
            bool isVisible = selectedMatchups.Count > 0;

            teamOneName.Visible =
            teamOneScoreLabel.Visible =
            teamOneScoreValue.Visible =
            versusLabel.Visible =
            teamTwoName.Visible =
            teamTwoScoreLabel.Visible =
            teamTwoScoreValue.Visible = 
            scoreButton.Visible = isVisible;
        }

        private void LoadMatchup(MatchupModel m)
        {
            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneName.Text = m.Entries[0].TeamCompeting.TeamName;
                        teamOneScoreValue.Text = m.Entries[0].Score.ToString();

                        // Hide team two controls
                        teamTwoScoreLabel.Visible = false;
                        teamTwoScoreValue.Visible = false;
                        teamTwoName.Visible = false;
                        versusLabel.Visible = false;
                    }
                    else
                    {
                        teamOneName.Text = "Not Yet Set";
                        teamOneScoreValue.Text = "";
                    }
                }

                if (i == 1)
                {
                    // Show team two controls if it exists
                    teamTwoScoreLabel.Visible = true;
                    teamTwoScoreValue.Visible = true;
                    teamTwoName.Visible = true;
                    versusLabel.Visible = true;

                    if (m.Entries[1].TeamCompeting != null)
                    {
                        teamTwoName.Text = m.Entries[1].TeamCompeting.TeamName;
                        teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoName.Text = "Not Yet Set";
                        teamTwoScoreValue.Text = "";
                    }
                }
            }

            if (m.Entries.Any(x => x.TeamCompeting == null) || m.Entries.Count < 2 || m.Winner != null)
            {
                scoreButton.Enabled = false;
                teamOneScoreValue.Enabled = false;
                teamTwoScoreValue.Enabled = false;
            }
            else
            {
                scoreButton.Enabled = true;
                teamOneScoreValue.Enabled = true;
                teamTwoScoreValue.Enabled = true;
            }
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((MatchupModel)matchupListBox.SelectedItem == null) return;
            LoadMatchup((MatchupModel)matchupListBox.SelectedItem);
        }

        private void unplayedOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private string ValidateData()
        {
            string output = "";

            double teamOneScore = 0;
            double teamTwoScore = 0;

            bool scoreOneValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
            bool scoreTwoValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);

            if (!scoreOneValid)
            {
                output = "The Score One value is not a valid number.";
            }
            else if (!scoreTwoValid)
            {
                output = "The Score Two value is not a valid number.";
            }
            else if (teamOneScore == 0 && teamTwoScore == 0)
            {
                output = "You did not enter a score for either team.";
            }
            else if (teamOneScore == teamTwoScore)
            {
                output = "We do not allow ties in this application.";
            }

            return output;
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            string errorMessage = ValidateData();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show($"Input Error : {errorMessage}");
                return;
            }

            MatchupModel m = (MatchupModel)matchupListBox.SelectedItem;
            double teamOneScore = 0;
            double teamTwoScore = 0;

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);

                        if (scoreValid)
                        {
                            m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for team 1.");
                            return;
                        }
                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);

                        if (scoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid score for team 2.");
                            return;
                        }
                    }
                }
            }

            try
            {
                TournamentLogic.UpdateTournamentResults(tournament);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The application had the following error: {ex.Message}");
                return;
            }

            LoadMatchups((int)roundDropDown.SelectedItem);
        }

        private void TournamentViewerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            callingForm.TournamentComplete(tournament, false);
        }

        private void prizesButton_Click(object sender, EventArgs e)
        {
            PrizeDisplayForm frm = new PrizeDisplayForm(tournament);
            frm.ShowDialog();
        }
    }
}
