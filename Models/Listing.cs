using System;
using System.Collections.Generic;

namespace sharebnb_api.Models;

public partial class Listing
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? PhotoUrl { get; set; }

    public int? Price { get; set; }

    public string? Description { get; set; }

    public string Location { get; set; } = null!;

    public string OwnerUsername { get; set; } = null!;

    public virtual User OwnerUsernameNavigation { get; set; } = null!;

    public virtual ICollection<User> Usernames { get; set; } = new List<User>();
}
