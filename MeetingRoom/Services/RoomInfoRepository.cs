using MeetingRoom.DbContexts;
using MeetingRoom.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoom.Services
{
    public class RoomInfoRepository : IRoomInfoRepository, ISaveChangesRepository
    {
        public readonly RoomReservationInfoContext _context;
        public RoomInfoRepository(RoomReservationInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<Room?> GetRoomAsync(int roomId, bool includeReservations = false)
        {
            
            if (includeReservations)
            {
                return await _context.Rooms.Include(r => r.Reservations)
                    .Where(r => r.Id == roomId).FirstOrDefaultAsync();
            }
            
            return await _context.Rooms
                .Where(r => r.Id == roomId).FirstOrDefaultAsync();
        }

        public async Task<bool> RoomExistsAsync(int roomId)
        {
            return await _context.Reservations.AnyAsync(r => r.Id == roomId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
