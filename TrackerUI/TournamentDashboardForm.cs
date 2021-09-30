using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentDashboardForm : Form, ITournamentRequester
    {
        List<TournamentModel> tournaments = GlobalConfig.Connection.GetTournament_All();

        public TournamentDashboardForm()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            loadExistingTournamentDropDown.DataSource = null;
            tournaments = GlobalConfig.Connection.GetTournament_All();

            if (showInactiveCheckBox.Checked)
            {
                loadExistingTournamentDropDown.DataSource = tournaments;
            }
            else
            {
                loadExistingTournamentDropDown.DataSource = tournaments.Where(x => x.Active == true).ToList();
            }

            loadExistingTournamentDropDown.DisplayMember = nameof(TournamentModel.TournamentName);
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm frm = new CreateTournamentForm(this);
            frm.ShowDialog();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            TournamentModel tm = (TournamentModel)loadExistingTournamentDropDown.SelectedItem;
            if (tm == null)
            {
                return;
            }
            TournamentViewerForm frm = new TournamentViewerForm(tm, this);
            frm.ShowDialog();
        }

        private void showInactiveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            WireUpLists();
        }

        public void TournamentComplete(TournamentModel model, bool newTournament)
        {
            // Get back from the form a TournamentModel
            // Refresh the dropdown list
            WireUpLists();

            if (newTournament)
            {
                // Select the new tournament
                loadExistingTournamentDropDown.SelectedItem = loadExistingTournamentDropDown.Items.Cast<TournamentModel>().Single(x => x.Id == model.Id);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            TournamentModel tm = (TournamentModel)loadExistingTournamentDropDown.SelectedItem;
            if (tm == null)
            {
                return;
            }

            var confirmResult = MessageBox.Show($@"Are you sure you want to delete ""{tm.TournamentName}""?",
                                                "Confirm Deletion",
                                                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                GlobalConfig.Connection.DeleteTournament(tm);
                WireUpLists();
                MessageBox.Show($@"""{tm.TournamentName}"" has been deleted.");
            }
        }
    }
}
