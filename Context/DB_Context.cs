using ApiRoulette.Redis.Entities;
using EntityRoulette.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityRoulette.Context
{
    public class DB_Context : DbContext
    {
        public DB_Context(DbContextOptions<DB_Context> options):base(options) 
        {

        }
        public DbSet<RouletteEntity> Roulette { get; set; }
        public DbSet<BetEntity> Bet { get; set; }
    }
}
