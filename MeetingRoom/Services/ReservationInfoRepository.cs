using MeetingRoom.DbContexts;
using MeetingRoom.Entities;
using MeetingRoom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoom.Services
{
    public class ReservationInfoRepository : IReservationsInfoRepository
    {
        public readonly RoomReservationInfoContext _context;
        public ReservationInfoRepository(RoomReservationInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));    
        }

        public async Task<IEnumerable<Reservation>> GetReservationsAsync()
        {
            return await _context.Reservations.OrderBy(r => r.Date).ToListAsync();
        }

        public async Task<bool> ReservationExistsAsync(int reservationId)
        {
            return await _context.Reservations.AnyAsync(r => r.Id == reservationId);
        }

        
        public async Task<Reservation?> GetReservationAsync(int reservationId)
        {

            var query = _context.Reservations.AsQueryable();


            return await query
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }


        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
        }

        
        public void DeleteReservation(int reservationId)
        {
            var reservationToRemove = _context.Reservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservationToRemove != null)
            {
                _context.Reservations.Remove(reservationToRemove);
            }
        }




        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.OrderBy(r => r.Id).ToListAsync();
        }

        public async Task<Room?> GetRoomAsync(int roomId, bool includeReservations = false)
        {
            var query = _context.Rooms.AsQueryable();

            if (includeReservations)
            {
                query = query.Include(r => r.Reservations);
            }

            return await query.FirstOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<bool> RoomExistsAsync(int roomId)
        {
            return await _context.Rooms.AnyAsync(r => r.Id == roomId);
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
        }

        public void DeleteRoom(Room room)
        {
            _context.Rooms.Remove(room);
        }





        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.OrderBy(r => r.Id).ToListAsync();
        }


        public async Task<User?> GetUserAsync(int userId, bool includeReservations = false, bool filterReservationByData = true,int pageNumber = 0)
        {

            if (includeReservations)
            {
                if(filterReservationByData)
                {
                    return await _context.Users.Include(r => r.Reservations
                    .Where(r => new DateTime(r.Date, r.StartTime) > DateTime.Now)
                    .Skip(10 * pageNumber).Take(10))
                    .Where(r => r.Id == userId).FirstOrDefaultAsync();
                }
                return await _context.Users.Include(r => r.Reservations.Skip(10* pageNumber).Take(10))
                    .Where(r => r.Id == userId).FirstOrDefaultAsync();
            }

            return await _context.Users
                .Where(r => r.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(r => r.Id == userId);
        }

        public void CreateUserAsync (User user)
        {
             _context.Users.Add(user);
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
