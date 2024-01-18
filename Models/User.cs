using System;
using System.Collections.Generic;

namespace sharebnb_api.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Listing> ListingsNavigation { get; set; } = new List<Listing>();

    public virtual ICollection<Message> MessageUserFromNavigations { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageUserToNavigations { get; set; } = new List<Message>();

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
