using System;
using System.Collections.Generic;

namespace DKSV.Models.Entities;

public partial class LopHoc
{
    public int Id { get; set; }

    public int IdMonHoc { get; set; }

    public int IdHocKy { get; set; }

    public string MaLop { get; set; } = null!;

    public int IdGiangVien { get; set; }

    public int SiSoToiDa { get; set; }

    public string Thu { get; set; } = null!;

    public TimeOnly GioBatDau { get; set; }

    public TimeOnly GioKetThuc { get; set; }

    public string? Phong { get; set; }

    public virtual ICollection<DangKy> DangKies { get; set; } = new List<DangKy>();

    public virtual NguoiDung IdGiangVienNavigation { get; set; } = null!;

    public virtual HocKy IdHocKyNavigation { get; set; } = null!;

    public virtual MonHoc IdMonHocNavigation { get; set; } = null!;
}
