using System;
using System.Collections.Generic;

namespace DKSV.Models.Entities;

public partial class DangKy
{
    public int Id { get; set; }

    public int IdLop { get; set; }

    public int IdSinhVien { get; set; }

    public string TrangThai { get; set; } = null!;

    public decimal? Diem { get; set; }

    public DateTime? NgayDangKy { get; set; }

    public virtual LopHoc IdLopNavigation { get; set; } = null!;

    public virtual NguoiDung IdSinhVienNavigation { get; set; } = null!;
}
