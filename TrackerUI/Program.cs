using System;
using System.Configuration;
using System.Net.Configuration;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary;

namespace TrackerUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configure database connection
            GlobalConfig.InitializeConnections(DatabaseType.Sql);

            // Configure email sender
            EmailLogic.ConfigureSender();

            // Check for configuration errors
            // If there are errors, don't start the app
            string errorMessage = ValidateConfig();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage);
                return;
            }

            Application.Run(new TournamentDashboardForm());
        }

        private static string ValidateConfig()
        {
            StringBuilder output = new StringBuilder();

            if (GlobalConfig.FromEmail() == "emailAddress" | 
                ((SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp")).Network.UserName == "emailAddress")
            {
                output.AppendLine("The email in the app config has not been set up.");
            }

            return output.ToString();
        }
    }
}
