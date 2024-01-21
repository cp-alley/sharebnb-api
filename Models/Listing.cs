using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sharebnb_api.Models;

[Table("listings")]
public partial class Listing
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(40)]
    public string Title { get; set; } = null!;

    [Column("type")]
    [StringLength(25)]
    public string Type { get; set; } = null!;

    [Column("photo_url")]
    public string? PhotoUrl { get; set; }

    [Column("price")]
    public int? Price { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("location")]
    public string Location { get; set; } = null!;

    [Column("owner_username")]
    [StringLength(25)]
    public string OwnerUsername { get; set; } = null!;

    [ForeignKey("OwnerUsername")]
    [InverseProperty("ListingsNavigation")]
    public virtual User OwnerUsernameNavigation { get; set; } = null!;

    [ForeignKey("ListingId")]
    [InverseProperty("Listings")]
    public virtual ICollection<User> Usernames { get; set; } = new List<User>();
}
