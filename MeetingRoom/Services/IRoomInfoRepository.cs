using MeetingRoom.Entities;

namespace MeetingRoom.Services
{
    public interface IRoomInfoRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<Room?> GetRoomAsync(
            int roomId, 
            bool includeReservations = false);
        Task<bool>RoomExistsAsync(int roomId);
    }
}
