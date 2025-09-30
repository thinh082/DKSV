using System;
using System.Collections.Generic;

namespace DKSV.Models.Entities;

public partial class MonHoc
{
    public int Id { get; set; }

    public string MaMon { get; set; } = null!;

    public string TenMon { get; set; } = null!;

    public byte SoTinChi { get; set; }

    public virtual ICollection<LopHoc> LopHocs { get; set; } = new List<LopHoc>();
}
