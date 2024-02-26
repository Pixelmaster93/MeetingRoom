using MeetingRoom.Entities;

namespace MeetingRoom.Services
{
    public interface IUserInfoRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(
            int userId, 
            bool includeReservations = false);
        Task<bool> UserExistsAsync(int userId);
        void DeleteUser(User user);
    }
}
