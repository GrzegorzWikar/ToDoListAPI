using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ToDoAPI.Data;
using ToDoAPI.Interface;
using System.Security.Claims;
using ToDoAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Service
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ToDoAPIContext db;

        public UserService(IOptions<AppSettings> appSettings, ToDoAPIContext _db) 
        {
            _appSettings = appSettings.Value;
            db = _db;
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
        {
            var user = await db.Users.SingleOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);
            
            if (user == null) return null;

            var token = await genereteJwtToken(user);

            return new AuthenticateResponse(user, token);
        }   

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.Where(x => x.isActive == true).ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> AddAndUpdateUser(User userObj)
        {
            bool isSuccess = false;
            if (userObj.Id > 0) 
            {
                var obj = await db.Users.FirstOrDefaultAsync(c =>  c.Id == userObj.Id);
                if (obj != null)
                {
                    obj.FirstName = userObj.FirstName;
                    obj.LastName = userObj.LastName;
                    db.Users.Update(obj);
                    isSuccess = await db.SaveChangesAsync() > 0;
                } 
            }
            else
            {
                await db.Users.AddAsync(userObj);
                isSuccess = await db.SaveChangesAsync() > 0;
            }

            return isSuccess ? userObj : null;
        }

        private async Task<string> genereteJwtToken(User user)
        {
            var tokenHendler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                return tokenHendler.CreateToken(tokenDescription);
            });

            return tokenHendler.WriteToken(token);
        }
    }
}
