using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sharebnb_api.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("username")]
    [StringLength(25)]
    public string Username { get; set; } = null!;

    [Column("password")]
    public string Password { get; set; } = null!;

    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    public string LastName { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [InverseProperty("OwnerUsernameNavigation")]
    public virtual ICollection<Listing> ListingsNavigation { get; set; } = new List<Listing>();

    [InverseProperty("UserFromNavigation")]
    public virtual ICollection<Message> MessageUserFromNavigations { get; set; } = new List<Message>();

    [InverseProperty("UserToNavigation")]
    public virtual ICollection<Message> MessageUserToNavigations { get; set; } = new List<Message>();

    [ForeignKey("Username")]
    [InverseProperty("Usernames")]
    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
