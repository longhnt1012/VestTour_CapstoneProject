using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VestTour.Domain.Entities;

[Table("Feedback")]
public partial class Feedback
{
    [Key]
    [Column("FeedbackID")]
    public int FeedbackId { get; set; }

    [StringLength(255)]
    public string? Comment { get; set; }

    public int? Rating { get; set; }

    [StringLength(255)]
    public string? Response { get; set; }

    public DateOnly? DateSubmitted { get; set; }

    [Column("UserID")]
    public int? UserId { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Feedbacks")]
    public virtual Order? Order { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Feedbacks")]
    public virtual User? User { get; set; }
}
