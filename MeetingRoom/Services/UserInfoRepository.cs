using MeetingRoom.DbContexts;
using MeetingRoom.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoom.Services
{
    public class UserInfoRepository : IUserInfoRepository, ISaveChangesRepository
    {
        public readonly RoomReservationInfoContext _context;
        public UserInfoRepository(RoomReservationInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<User?> GetUserAsync(int userId, bool includeReservations = false)
        {
            
            if (includeReservations)
            {
                return await _context.Users.Include(r => r.Reservations)
                    .Where(r => r.Id == userId).FirstOrDefaultAsync();
            }
            
            return await _context.Users
                .Where(r => r.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(r => r.Id == userId);
        }
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
