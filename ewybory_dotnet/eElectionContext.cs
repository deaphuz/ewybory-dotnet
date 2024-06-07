using ewybory_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace ewybory_dotnet
{
    public class eElectionContext : DbContext
    {
        public eElectionContext(DbContextOptions options) : base (options)
        { 
        }

        protected eElectionContext()
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Election> Elections {  get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupElection> GroupElections { get; set; }
        public virtual DbSet<Organizer> Organizers { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<Voter> Voters { get; set; }
        public virtual DbSet<VoterGroup> VoterGroups { get; set; }
    }
}
