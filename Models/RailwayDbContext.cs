using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Railway_OnlineTicket.Models
{
    public class RailwayDbContext : DbContext
    {
        public DbSet<UserInfo> usreinfoes { get; set; }
        public DbSet<Fare_Query> Queryes { get; set; }
        public DbSet<Purchase_Ticket> PurchaseTickets { get; set; }

    }
}