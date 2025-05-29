// Services/AuthService.cs
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CrimeAnalysisSystem.Models;
using CrimeAnalysisSystem.Models.ViewModels;
using CrimeAnalysisSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies; // Added this using

namespace CrimeAnalysisSystem.Services
{
    public interface IAuthService
    {
        Task<AuthResult> Authenticate(string username, string password);
        Task<AuthResult> Register(RegisterViewModel model);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResult> Authenticate(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            
            if (user == null)
                return new AuthResult { Success = false, ErrorMessage = "Invalid login attempt." };
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            
            if (result != PasswordVerificationResult.Success)
                return new AuthResult { Success = false, ErrorMessage = "Invalid login attempt." };
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new AuthResult { Success = true, ClaimsIdentity = claimsIdentity };
        }

        public async Task<AuthResult> Register(RegisterViewModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                return new AuthResult { Success = false, ErrorMessage = "Username already exists." };
            
            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Role = model.IsAdmin ? "Admin" : "User"
            };
            
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return new AuthResult { Success = true };
        }
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public ClaimsIdentity? ClaimsIdentity { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
