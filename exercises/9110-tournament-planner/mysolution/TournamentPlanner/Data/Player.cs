using System.ComponentModel.DataAnnotations;

namespace TournamentPlanner.Data
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Phone { get; set; }
    }
}
