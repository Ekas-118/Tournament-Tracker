using System.Collections.Generic;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents on team
    /// </summary>
    public class TeamModel
    {
        /// <summary>
        /// The unique identifier for the team.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the team
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// The list of team members
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
    }
}
