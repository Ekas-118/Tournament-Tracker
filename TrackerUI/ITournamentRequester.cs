using TrackerLibrary.Models;

namespace TrackerUI
{
    public interface ITournamentRequester
    {
        /// <param name="newTournament">True if the method was called after creating a tournament</param>
        void TournamentComplete(TournamentModel model, bool newTournament);
    }
}
