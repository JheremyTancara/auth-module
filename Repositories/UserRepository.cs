using Api.Data;
using Api.DTOs;
using Api.Models;
using Api.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class UserRepository : RepositoryBase<User, RegisterUserDTO>
    {
        private readonly DataContext _context;
        public DataTransformationService genericService;

        public UserRepository(DataContext context)
        {
            _context = context;
            genericService = new DataTransformationService();
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public override async Task<User> CreateAsync(RegisterUserDTO newUserDTO)
        {
            var user = new User
            {
                UserID = await _context.Users.CountAsync() + 1,
                Username = newUserDTO.Username,
                Email = newUserDTO.Email,
                Password = newUserDTO.Password,
                DateOfBirth = DataTransformationService.ConvertToDateTime(newUserDTO.DateOfBirth),
                SubscriptionLevel = DataTransformationService.ConvertToSubscriptionLevel(newUserDTO.SubscriptionLevel),
                ProfilePicture = DataTransformationService.ConvertToProfileSkin(newUserDTO.ProfilePicture),
                Role = UserRole.User
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public override async Task Update(int id, RegisterUserDTO userDTO)
        {
            var existingUser = await GetByIdAsync(id);

            if (existingUser is not null)
            {
                existingUser.Username = userDTO.Username;
                existingUser.Email = userDTO.Email;
                existingUser.Password = userDTO.Password;
                existingUser.DateOfBirth = DataTransformationService.ConvertToDateTime(userDTO.DateOfBirth);
                existingUser.SubscriptionLevel = DataTransformationService.ConvertToSubscriptionLevel(userDTO.SubscriptionLevel);
                existingUser.ProfilePicture = DataTransformationService.ConvertToProfileSkin(userDTO.ProfilePicture);
                existingUser.Role = UserRole.User;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && user.Password == password) 
            {
                return user;
            }

            return null; 
        }
    }
}
