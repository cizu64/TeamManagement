using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.WebAPI.Security
{
    public class JwtAuth
    {
        private readonly IGenericRepository<TeamLead> _teamLeadRepo;
        private readonly IGenericRepository<TeamMember> _teamMemberRepo;

        public JwtAuth(IGenericRepository<TeamLead> teamLeadRepo, IGenericRepository<TeamMember> teamMemberRepo)
        {
            _teamLeadRepo = teamLeadRepo;
            _teamMemberRepo = teamMemberRepo;
        }
        public async Task<string> AuthenticateTeamLead(string email, string password)
        {
            var teamLead = await _teamLeadRepo.Get(t => t.Email.ToLower() == email.ToLower() && t.Password == password);
            if (teamLead == null)  return string.Empty;
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = new byte[128];
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, teamLead.Id.ToString()),
                new Claim(ClaimTypes.Role, teamLead.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> AuthenticateTeamMember(string email, string password)
        {
            var teamMember = await _teamMemberRepo.Get(t => t.Email.ToLower() == email.ToLower() && t.Password == password);
            if (teamMember == null) return string.Empty;
            ArgumentNullException.ThrowIfNull(teamMember);
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = new byte[128];
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, teamMember.Id.ToString()),
                new Claim(ClaimTypes.Role, teamMember.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Tuple<ClaimsIdentity, bool>> ValidateToken(string token)
        {
            try
            {
                var validationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience=false,
                    ValidateIssuer=false,
                    IssuerSigningKey = new SymmetricSecurityKey(new byte[128])
                });
                return new(validationResult.ClaimsIdentity, validationResult.SecurityToken != null);
            }
            catch (Exception)
            {
                return new(null,false);
            }
        }

       
    }
}
