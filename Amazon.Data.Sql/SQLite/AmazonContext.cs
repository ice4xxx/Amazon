using System;
using Amazon.Data.Sql.Entities;
using System.Data.Entity;
using DI.Enums;

namespace Amazon.Data.Sql.SQLite
{
    /// <summary>
    /// Context to data.db
    /// </summary>
    class AmazonContext : DbContext
    {
        /// <summary>
        /// Users table.
        /// </summary>
        public DbSet<UserEntity> Users { get; set; }

        /// <summary>
        /// Products table.
        /// </summary>
        public DbSet<ProductEntity> Products { get; set; }

        /// <summary>
        /// Orders table.
        /// </summary>
        public DbSet<OrderEntity> Orders { get; set; }

        public AmazonContext() : base("DataConnection")
        {
        }
    }
}
