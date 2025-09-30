using System;
using System.Collections.Generic;

namespace DKSV.Models.Entities;

public partial class HocKy
{
    public int Id { get; set; }

    public string MaHocKy { get; set; } = null!;

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public virtual ICollection<LopHoc> LopHocs { get; set; } = new List<LopHoc>();
}
