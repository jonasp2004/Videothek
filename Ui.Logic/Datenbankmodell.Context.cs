﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ui.Logic
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class videothek_development : DbContext
    {
        public videothek_development()
            : base("name=videothek_development")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AppUpdates> AppUpdates { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleRented> ArticleRented { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<PasswordArchive> PasswordArchive { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
    }
}