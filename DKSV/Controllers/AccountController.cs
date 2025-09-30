using DKSV.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DKSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly myContext _context;
        public AccountController(myContext myContext)
        {
            _context = myContext;
        }
        [HttpPost("register")]
        public IActionResult Register(AccountModel account)
        {
            try
            {
                var existingAccount = _context.NguoiDungs.FirstOrDefault(a => a.Email == account.Username);
                if (existingAccount != null)
                {
                    return BadRequest("Username already exists.");
                }
                existingAccount = new NguoiDung
                {
                    Email = account.Username,
                    MatKhau = account.Password,
                    HoTen = account.FullName,
                    TrangThai = true,
                    NgayTao = DateTime.Now,
                    IsSinhVien = true,
                    IsGiaoVien = false
                };
                _context.NguoiDungs.Add(existingAccount);
                _context.SaveChanges();
                return Ok("Registration successful.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("login")]
        public IActionResult Login(AccountModel account)
        {
            try
            {
                var existingAccount = _context.NguoiDungs.FirstOrDefault(a => a.Email == account.Username && a.MatKhau == account.Password);
                if (existingAccount == null)
                {
                    return Unauthorized("Invalid username or password.");
                }
                return Ok("Login successful.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
    public class AccountModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? FullName { get; set; }
    }
}
