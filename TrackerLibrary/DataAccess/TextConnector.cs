using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrackerLibrary.DataAccess.TextHelpers;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    class TextConnector : IDataConnection
    {
        public TextConnector()
        {
            Directory.CreateDirectory(GlobalConfig.FileDirectory);
        }

        /// <summary>
        /// Marks the specified tournament as complete
        /// </summary>
        public void CompleteTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = GetTournament_All();

            tournaments.Remove(tournaments.Single(x => x.Id == model.Id));
            
            model.Active = false;

            tournaments.Add(model);

            tournaments.SaveToTournamentFile();
        }

        /// <summary>
        /// Saves a new person to the text file
        /// </summary>
        /// <param name="model">The person information.</param>
        public void CreatePerson(PersonModel model)
        {
            // Load the text file and convert the text to List<PersonModel>
            List<PersonModel> people = GetPerson_All();

            // Find the max ID
            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.Max(x => x.Id) + 1;
            }

            model.Id = currentId;

            // Add the new record with the new ID (max + 1)
            people.Add(model);

            // Convert the people to List<string> and save to the text file
            people.SaveToPeopleFile();
        }

        /// <summary>
        /// Saves a new prize to the text file
        /// </summary>
        /// <param name="model">The prize information.</param>
        public void CreatePrize(PrizeModel model)
        {
            // Load the text file and convert the text to List<PrizeModel>
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            // Find the max ID
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.Max(x => x.Id) + 1;
            }

            model.Id = currentId;

            // Add the new record with the new ID (max + 1)
            prizes.Add(model);

            // Convert the prizes to List<string> and save to the text file
            prizes.SaveToPrizeFile();
        }

        /// <summary>
        /// Saves a new team to the text file
        /// </summary>
        /// <param name="model">The team information.</param>
        public void CreateTeam(TeamModel model)
        {
            // Load the text file and convert the text to List<TeamModel>
            List<TeamModel> teams = GetTeam_All();

            // Find the max ID
            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.Max(x => x.Id) + 1;
            }

            model.Id = currentId;

            // Add the new record with the new ID (max + 1)
            teams.Add(model);

            // Convert the teams to List<string> and save to the text file
            teams.SaveToTeamFile();
        }

        /// <summary>
        /// Saves a new tournament and its related information to the text file
        /// </summary>
        /// <param name="model">The tournament information.</param>
        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = GetTournament_All();

            // Find the max ID
            int currentId = 1;

            if (tournaments.Count > 0)
            {
                currentId = tournaments.Max(x => x.Id) + 1;
            }

            model.Id = currentId;

            model.SaveRoundsToFile();

            // Add the new record with the new ID (max + 1)
            tournaments.Add(model);

            // Convert the tournaments to List<string> and save to the text file
            tournaments.SaveToTournamentFile();

            TournamentLogic.UpdateTournamentResults(model);
        }

        public void DeletePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            prizes.Remove(prizes.Single(x => x.Id == model.Id));

            prizes.SaveToPrizeFile();
        }

        /// <summary>
        /// Removes the specified tournament and all its data from the text files
        /// </summary>
        public void DeleteTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = GetTournament_All();
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
            List<MatchupModel> matchups = GlobalConfig.MatchupFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();

            List<int> tournamentPrizeIds = new List<int>();
            model.Prizes.ForEach(prize => tournamentPrizeIds.Add(prize.Id));
            List<int> tournamentMatchupIds = new List<int>();
            model.Rounds.ForEach(round => round.ForEach(matchup => tournamentMatchupIds.Add(matchup.Id)));
            List<int> tournamentMatchupEntryIds = new List<int>();
            model.Rounds.ForEach(round => round.ForEach(matchup => matchup.Entries.ForEach(entry => tournamentMatchupEntryIds.Add(entry.Id))));

            tournaments.Remove(tournaments.Single(x => x.Id == model.Id));
            prizes.RemoveAll(prize => tournamentPrizeIds.Contains(prize.Id));
            matchups.RemoveAll(matchup => tournamentMatchupIds.Contains(matchup.Id));
            entries.RemoveAll(entry => tournamentMatchupEntryIds.Contains(entry.Id));

            tournaments.SaveToTournamentFile();
            prizes.SaveToPrizeFile();
            matchups.SaveToMatchupFile();
            entries.SaveToMatchupEntryFile();
        }

        public List<PersonModel> GetPerson_All()
        {
            return GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_All()
        {
            return GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeamModels();
        }

        public List<TournamentModel> GetTournament_All()
        {
            return GlobalConfig.TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();
        }

        public void UpdateMatchup(MatchupModel model)
        {
            model.UpdateMatchupToFile();
        }
    }
}
