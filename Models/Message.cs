using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace sharebnb_api.Models;

[Table("messages")]
public partial class Message
{
    [Key]
    [Column("message_id")]
    public int MessageId { get; set; }

    [Column("user_to")]
    public string UserTo { get; set; } = null!;

    [Column("user_from")]
    public string UserFrom { get; set; } = null!;

    [Column("body")]
    public string Body { get; set; } = null!;

    [Column("sent_at")]
    public DateTime SentAt { get; set; }

    [ForeignKey("UserFrom")]
    [InverseProperty("MessageUserFromNavigations")]
    public virtual User UserFromNavigation { get; set; } = null!;

    [ForeignKey("UserTo")]
    [InverseProperty("MessageUserToNavigations")]
    public virtual User UserToNavigation { get; set; } = null!;
}
