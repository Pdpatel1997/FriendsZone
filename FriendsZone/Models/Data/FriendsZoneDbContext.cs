using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FriendsZone.Models.Data
{
    public class FriendsZoneDbContext:DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
    }
}