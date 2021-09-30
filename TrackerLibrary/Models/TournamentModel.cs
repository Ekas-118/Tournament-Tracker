using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        public event EventHandler<TeamModel> OnTournamentComplete;

        /// <summary>
        /// The unique identifier for the tournament.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name given to this tournament.
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// The amount of money each team needs to put up to enter.
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// The initial amount of money in the prize pool
        /// </summary>
        public decimal InitialPrizePool { get; set; }

        /// <summary>
        /// The set of teams that have been entered.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();

        /// <summary>
        /// The list of prizes for the various places.
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// The matchups per round
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

        /// <summary>
        /// True if the winner has not yet been determined
        /// </summary>
        public bool Active { get; set; } = true;

        /// <summary>
        /// True if the team with the greater score should win
        /// </summary>
        public bool GreaterScoreWins { get; set; }

        public void CompleteTournament()
        {
            OnTournamentComplete?.Invoke(this, Rounds.Last().Single().Winner);
        }
    }
}
