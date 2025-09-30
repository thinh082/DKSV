using System;
using System.Collections.Generic;

namespace DKSV.Models.Entities;

public partial class GiangVien
{
    public int IdNguoiDung { get; set; }

    public string MaGv { get; set; } = null!;

    public string? HocVi { get; set; }

    public string? Khoa { get; set; }

    public virtual NguoiDung IdNguoiDungNavigation { get; set; } = null!;
}
