﻿using System;
using System.Collections.Generic;

namespace VestTour.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? PaymentId { get; set; }

    public int? StoreId { get; set; }

    public int? VoucherId { get; set; }

    public int? ShipperPartnerId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    public string? Note { get; set; }

    public bool Paid { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Payment? Payment { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ShipperPartner? ShipperPartner { get; set; }

    public virtual Store? Store { get; set; }

    public virtual Voucher? Voucher { get; set; }
}