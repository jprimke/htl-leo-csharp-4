using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace TournamentPlanner.Data
{
    public enum PlayerNumber
    {
        Player1 = 1,
        Player2 = 2
    };

    public class TournamentPlannerDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<Match> Matches { get; set; }

        public TournamentPlannerDbContext(DbContextOptions<TournamentPlannerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>(m =>
            {
                m.Property(e => e.Round).IsRequired();
                m.HasOne(n => n.Player1)
                 .WithMany()
                 .HasForeignKey(p => p.Player1Id)
                 .OnDelete(DeleteBehavior.NoAction);
                m.HasOne(n => n.Player2)
                 .WithMany()
                 .HasForeignKey(p => p.Player2Id)
                 .OnDelete(DeleteBehavior.NoAction);
                m.HasOne(n => n.Winner)
                 .WithMany()
                 .HasForeignKey(p => p.WinnerId)
                 .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Player>(p =>
            {
                p.Property(e => e.Name).IsRequired().HasMaxLength(200);
                p.Property(e => e.Phone).HasMaxLength(100);
            });
        }

        private async Task<TEntity> AddEntity<TEntity>(TEntity newEntity) where TEntity : class
        {
            Add(newEntity);
            await SaveChangesAsync();

            return newEntity;
        }

        /// <summary>
        /// Adds a new player to the player table
        /// </summary>
        /// <param name="newPlayer">Player to add</param>
        /// <returns>Player after it has been added to the DB</returns>
        public async Task<Player> AddPlayer(Player newPlayer)
        {
            return await AddEntity(newPlayer);
        }

        /// <summary>
        /// Adds a match between two players
        /// </summary>
        /// <param name="player1Id">ID of player 1</param>
        /// <param name="player2Id">ID of player 2</param>
        /// <param name="round">Number of the round</param>
        /// <returns>Generated match after it has been added to the DB</returns>
        public async Task<Match> AddMatch(int player1Id, int player2Id, int round)
        {
            return await AddEntity(new Match {Player1Id = player1Id, Player2Id = player2Id, Round = round});
        }

        /// <summary>
        /// Set winner of an existing game
        /// </summary>
        /// <param name="matchId">ID of the match to update</param>
        /// <param name="player">Player who has won the match</param>
        /// <returns>Match after it has been updated in the DB</returns>
        public async Task<Match> SetWinner(int matchId, PlayerNumber player)
        {
            var match = await Matches.FindAsync(matchId);
            if (match is not null)
            {
                match.WinnerId = player == PlayerNumber.Player1 ? match.Player1Id : match.Player2Id;
                await SaveChangesAsync();
            }

            return match;
        }

        /// <summary>
        /// Get a list of all matches that do not have a winner yet
        /// </summary>
        /// <returns>List of all found matches</returns>
        public async Task<IList<Match>> GetIncompleteMatches()
        {
            return await Matches.Where(m => m.WinnerId == null).ToListAsync();
        }

        /// <summary>
        /// Delete everything (matches, players)
        /// </summary>
        public async Task DeleteEverything()
        {
            Players.RemoveRange(await Players.ToListAsync());
            Matches.RemoveRange(await Matches.ToListAsync());
            await SaveChangesAsync();
        }

        /// <summary>
        /// Get a list of all players whose name contains <paramref name="playerFilter"/>
        /// </summary>
        /// <param name="playerFilter">Player filter. If null, all players must be returned</param>
        /// <returns>List of all found players</returns>
        public async Task<IList<Player>> GetFilteredPlayers(string playerFilter = null)
        {
            return await Players.Where(p => playerFilter == null || p.Name.Contains(playerFilter)).ToListAsync();
        }

        /// <summary>
        /// Generate match records for the next round
        /// </summary>
        /// <exception cref="InvalidOperationException">Error while generating match records</exception>
        public async Task GenerateMatchesForNextRound()
        {
            if (await Matches.AnyAsync(m => m.WinnerId == null) || await Players.CountAsync() != 32)
            {
                throw new InvalidOperationException("Error while generating match records");
            }

            var countOfCompletedMatches = await Matches.CountAsync();
            var round = countOfCompletedMatches switch
            {
                0 => 1,
                16 => 2,
                24 => 3,
                28 => 4,
                30 => 5,
                _ => throw new InvalidOperationException("Error while generating match records")
            };

            var winners = await Players.ToArrayAsync();

            if (round > 1)
                winners = await Matches.Where(m => m.Round == round - 1).Select(m => m.Winner).ToArrayAsync();

            for (var i = 0; i < winners.Length; i += 2)
            {
                await AddMatch(winners[i].Id, winners[i + 1].Id, round);
            }

            await SaveChangesAsync();
        }
    }
}