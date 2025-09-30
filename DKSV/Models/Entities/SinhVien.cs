using System;
using System.Collections.Generic;

namespace DKSV.Models.Entities;

public partial class SinhVien
{
    public int IdNguoiDung { get; set; }

    public string MaSv { get; set; } = null!;

    public DateOnly? NgaySinh { get; set; }

    public string? Nganh { get; set; }

    public short? NamNhapHoc { get; set; }

    public virtual NguoiDung IdNguoiDungNavigation { get; set; } = null!;
}
