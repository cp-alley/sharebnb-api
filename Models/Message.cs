using System;
using System.Collections.Generic;

namespace sharebnb_api.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public string UserTo { get; set; } = null!;

    public string UserFrom { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime SentAt { get; set; }

    public virtual User UserFromNavigation { get; set; } = null!;

    public virtual User UserToNavigation { get; set; } = null!;
}
