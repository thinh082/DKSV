using DKSV.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DKSV.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly myContext _context;
        public StudentController(myContext context)
        {
            _context = context;
        }

        [HttpGet("getAllStudents")]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _context.SinhViens
                    .Include(s => s.IdNguoiDungNavigation) // lấy cả thông tin từ NguoiDung
                    .Select(s => new StudentDto
                    {
                        Id = s.IdNguoiDung,
                        HoTen = s.IdNguoiDungNavigation.HoTen,
                        Email = s.IdNguoiDungNavigation.Email,
                        MaSV = s.MaSv,
                        NgaySinh = s.NgaySinh,
                        Nganh = s.Nganh,
                        NamNhapHoc = s.NamNhapHoc
                    })
                    .ToList();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("getStudentById/{id}")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var student = _context.SinhViens
                    .Include(s => s.IdNguoiDungNavigation)
                    .Where(s => s.IdNguoiDung == id)
                    .Select(s => new StudentDto
                    {
                        Id = s.IdNguoiDung,
                        HoTen = s.IdNguoiDungNavigation.HoTen,
                        Email = s.IdNguoiDungNavigation.Email,
                        MaSV = s.MaSv,
                        NgaySinh = s.NgaySinh,
                        Nganh = s.Nganh,
                        NamNhapHoc = s.NamNhapHoc
                    })
                    .FirstOrDefault();

                if (student == null) return NotFound("Student not found.");

                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("addStudent")]
        public IActionResult AddStudent(StudentDto studentDto)
        {
            try
            {
                var nguoiDung = new NguoiDung
                {
                    HoTen = studentDto.HoTen,
                    Email = studentDto.Email,
                    MatKhau = "123456", // default hoặc generate
                    TrangThai = true,
                    NgayTao = DateTime.Now,
                    IsSinhVien = true,
                    IsGiaoVien = false
                };

                _context.NguoiDungs.Add(nguoiDung);
                _context.SaveChanges();

                var sinhVien = new SinhVien
                {
                    IdNguoiDung = nguoiDung.Id,
                    MaSv = studentDto.MaSV,
                    NgaySinh = studentDto.NgaySinh,
                    Nganh = studentDto.Nganh,
                    NamNhapHoc = studentDto.NamNhapHoc
                };

                _context.SinhViens.Add(sinhVien);
                _context.SaveChanges();

                return Ok("Student added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("updateStudent/{id}")]
        public IActionResult UpdateStudent(int id, StudentDto updatedStudent)
        {
            try
            {
                var sinhVien = _context.SinhViens
                    .Include(s => s.IdNguoiDungNavigation)
                    .FirstOrDefault(s => s.IdNguoiDung == id);

                if (sinhVien == null) return NotFound("Student not found.");

                // cập nhật bên NguoiDung
                sinhVien.IdNguoiDungNavigation.HoTen = updatedStudent.HoTen;
                sinhVien.IdNguoiDungNavigation.Email = updatedStudent.Email;

                // cập nhật bên SinhVien
                sinhVien.MaSv = updatedStudent.MaSV;
                sinhVien.NgaySinh = updatedStudent.NgaySinh;
                sinhVien.Nganh = updatedStudent.Nganh;
                sinhVien.NamNhapHoc = updatedStudent.NamNhapHoc;

                _context.SaveChanges();
                return Ok("Student updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete("deleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var sinhVien = _context.SinhViens.FirstOrDefault(s => s.IdNguoiDung == id);
                if (sinhVien == null) return NotFound("Student not found.");

                _context.SinhViens.Remove(sinhVien);
                _context.SaveChanges();

                return Ok("Student deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

    public class StudentDto
    {
        public int Id { get; set; }           // IdNguoiDung
        public string HoTen { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string MaSV { get; set; } = null!;
        public DateOnly? NgaySinh { get; set; }
        public string? Nganh { get; set; }
        public short? NamNhapHoc { get; set; }
    }

}
