using MeetingRoom.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoom.Services
{
    public interface ISaveChangesRepository
    {
        Task<bool> SaveChangesAsync();
        

    }
}
