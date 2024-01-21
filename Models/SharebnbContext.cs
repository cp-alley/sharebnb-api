using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sharebnb_api.Models;

public partial class SharebnbContext : DbContext
{
    public SharebnbContext()
    {
    }

    public SharebnbContext(DbContextOptions<SharebnbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Listing> Listings { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:PostgreSQL");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Listing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("listings_pkey");

            entity.HasOne(d => d.OwnerUsernameNavigation).WithMany(p => p.ListingsNavigation).HasConstraintName("listings_owner_username_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("messages_pkey");

            entity.HasOne(d => d.UserFromNavigation).WithMany(p => p.MessageUserFromNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_user_from_fkey");

            entity.HasOne(d => d.UserToNavigation).WithMany(p => p.MessageUserToNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_user_to_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("users_pkey");

            entity.HasMany(d => d.Listings).WithMany(p => p.Usernames)
                .UsingEntity<Dictionary<string, object>>(
                    "Booking",
                    r => r.HasOne<Listing>().WithMany()
                        .HasForeignKey("ListingId")
                        .HasConstraintName("bookings_listing_id_fkey"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("Username")
                        .HasConstraintName("bookings_username_fkey"),
                    j =>
                    {
                        j.HasKey("Username", "ListingId").HasName("bookings_pkey");
                        j.ToTable("bookings");
                        j.IndexerProperty<string>("Username")
                            .HasMaxLength(25)
                            .HasColumnName("username");
                        j.IndexerProperty<int>("ListingId").HasColumnName("listing_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
