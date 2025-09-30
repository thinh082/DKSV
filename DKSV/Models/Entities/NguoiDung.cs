using System;
using System.Collections.Generic;

namespace DKSV.Models.Entities;

public partial class NguoiDung
{
    public int Id { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public bool TrangThai { get; set; }

    public DateTime? NgayTao { get; set; }

    public bool? IsSinhVien { get; set; }

    public bool? IsGiaoVien { get; set; }

    public virtual ICollection<DangKy> DangKies { get; set; } = new List<DangKy>();

    public virtual GiangVien? GiangVien { get; set; }

    public virtual ICollection<LopHoc> LopHocs { get; set; } = new List<LopHoc>();

    public virtual SinhVien? SinhVien { get; set; }
}
