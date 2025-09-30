using DKSV.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DKSV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DangKyController : ControllerBase
    {
        private readonly myContext _context;

        public DangKyController(myContext context)
        {
            _context = context;
        }

        // API: Sinh viên đăng ký lớp
        [HttpPost("dangKyLop")]
        public IActionResult DangKyLop([FromBody] DangKyRequestDto request)
        {
            try
            {
                // kiểm tra lớp tồn tại
                var lop = _context.LopHocs
                    .Include(l => l.IdMonHocNavigation)
                    .FirstOrDefault(l => l.Id == request.IdLop);

                if (lop == null)
                    return NotFound("Lớp học không tồn tại.");

                // kiểm tra sinh viên tồn tại
                var sinhVien = _context.SinhViens
                    .Include(s => s.IdNguoiDungNavigation)
                    .FirstOrDefault(s => s.IdNguoiDung == request.IdSinhVien);

                if (sinhVien == null)
                    return NotFound("Sinh viên không tồn tại.");

                // kiểm tra trùng đăng ký
                var daDangKy = _context.DangKies
                    .FirstOrDefault(d => d.IdLop == request.IdLop && d.IdSinhVien == request.IdSinhVien);

                if (daDangKy != null)
                    return BadRequest("Sinh viên đã đăng ký lớp này.");

                // tạo đăng ký mới
                var dangKy = new DangKy
                {
                    IdLop = request.IdLop,
                    IdSinhVien = request.IdSinhVien,
                    TrangThai = "enrolled",
                    NgayDangKy = DateTime.Now
                };

                _context.DangKies.Add(dangKy);
                _context.SaveChanges();

                // trả về thông tin đăng ký
                var response = new DangKyResponseDto
                {
                    Id = dangKy.Id,
                    IdLop = lop.Id,
                    MaLop = lop.MaLop,
                    TenMonHoc = lop.IdMonHocNavigation.TenMon,
                    IdSinhVien = sinhVien.IdNguoiDung,
                    HoTenSinhVien = sinhVien.IdNguoiDungNavigation.HoTen,
                    TrangThai = dangKy.TrangThai,
                    NgayDangKy = dangKy.NgayDangKy
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
    public class DangKyRequestDto
    {
        public int IdLop { get; set; }
        public int IdSinhVien { get; set; }   // IdNguoiDung của sinh viên
    }
    public class DangKyResponseDto
    {
        public int Id { get; set; }
        public int IdLop { get; set; }
        public string MaLop { get; set; } = null!;
        public string TenMonHoc { get; set; } = null!;
        public int IdSinhVien { get; set; }
        public string HoTenSinhVien { get; set; } = null!;
        public string TrangThai { get; set; } = null!;
        public DateTime? NgayDangKy { get; set; }
    }

}
