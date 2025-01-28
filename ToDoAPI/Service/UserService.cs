using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ToDoAPI.Data;
using ToDoAPI.Interface;
using System.Security.Claims;
using ToDoAPI.Model;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.DTO;
using ToDoAPI.Helpers;

namespace ToDoAPI.Service
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly ToDoAPIContext db;
        private Converter _converter = Converter.GetInstance();

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

        public async Task<IEnumerable<DTOUser>> GetAll()
        {
            ICollection<User>? users = await db.Users.ToListAsync();

            ICollection<DTOUser> DTOUsers = new List<DTOUser>();

            foreach(User User in users)
            {
                DTOUser DTOUser = _converter.UserToDTOUser(User);

                DTOUsers.Add(DTOUser);
            }

            return DTOUsers;
        }

        public async Task<DTOUser?> GetById(int id)
        {
            User? user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null) return null;

            DTOUser DTOUser = _converter.UserToDTOUser(user);

            return DTOUser;
        }

        public async Task<DTOUser?> AddAndUpdateUser(User user)
        {
            bool isSuccess = false;

            if (user.Id > 0) 
            {
                if (user != null)
                {
                    db.Users.Update(user);
                    isSuccess = await db.SaveChangesAsync() > 0;
                } 
            }
            else
            {

                await db.Users.AddAsync(user);
                isSuccess = await db.SaveChangesAsync() > 0;
            }

            if (user == null) return null;

            return isSuccess ? _converter.UserToDTOUser(user) : null;
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
