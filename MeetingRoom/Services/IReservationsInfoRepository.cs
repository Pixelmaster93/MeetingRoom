using MeetingRoom.Entities;

namespace MeetingRoom.Services
{
    public interface IReservationsInfoRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Reservation?> GetReservationAsync(int reservationId);
        Task<bool> ReservationExistsAsync(int reservationId);
        void AddReservation(Reservation reservation);
        void DeleteReservation(int reservation);


        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<Room?> GetRoomAsync(
            int roomId,
            bool includeReservations = false);
        Task<bool> RoomExistsAsync(int roomId);
        void AddRoom(Room room);
        void DeleteRoom(Room room);


        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(
            int userId,
            bool includeReservations = false);
        Task<bool> UserExistsAsync(int userId);
        void CreateUserAsync (User user);
        void DeleteUser(User user);
       

        Task<bool> SaveChangesAsync();
    }
}
