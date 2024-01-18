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

            entity.ToTable("listings");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.OwnerUsername)
                .HasMaxLength(25)
                .HasColumnName("owner_username");
            entity.Property(e => e.PhotoUrl).HasColumnName("photo_url");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(40)
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .HasColumnName("type");

            entity.HasOne(d => d.OwnerUsernameNavigation).WithMany(p => p.ListingsNavigation)
                .HasForeignKey(d => d.OwnerUsername)
                .HasConstraintName("listings_owner_username_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.SentAt).HasColumnName("sent_at");
            entity.Property(e => e.UserFrom).HasColumnName("user_from");
            entity.Property(e => e.UserTo).HasColumnName("user_to");

            entity.HasOne(d => d.UserFromNavigation).WithMany(p => p.MessageUserFromNavigations)
                .HasForeignKey(d => d.UserFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_user_from_fkey");

            entity.HasOne(d => d.UserToNavigation).WithMany(p => p.MessageUserToNavigations)
                .HasForeignKey(d => d.UserTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("messages_user_to_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .HasColumnName("username");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Password).HasColumnName("password");

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
