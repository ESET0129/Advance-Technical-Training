using System;
using System.Collections.Generic;

namespace AMI_project.Models;

public partial class Meter
{
    public string MeterSerialNo { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public string Iccid { get; set; } = null!;

    public string Imsi { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public string? Firmware { get; set; }

    public string Category { get; set; } = null!;

    public DateTime InstallTsUtc { get; set; }

    public string Status { get; set; } = null!;

    public long? ConsumerId { get; set; }

    public int OrgUnitId { get; set; }

    public int TariffId { get; set; }

    public virtual Consumer? Consumer { get; set; }

    public virtual ICollection<DailyReading> DailyReadings { get; set; } = new List<DailyReading>();

    public virtual ICollection<MonthlyBill> MonthlyBills { get; set; } = new List<MonthlyBill>();

    public virtual OrgUnit OrgUnit { get; set; } = null!;

    public virtual Tariff Tariff { get; set; } = null!;
}
