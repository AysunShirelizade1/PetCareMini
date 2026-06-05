using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;

    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
    }

    public async Task<Appointment?> GetByIdAsync(int id)
        => await _context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<List<Appointment>> GetUserAppointmentsAsync(int userId)
        => await _context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .Include(a => a.User)
            .Where(a => a.UserId == userId &&
                        a.Status != AppointmentStatus.Canceled)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<List<Appointment>> GetAllAsync()
        => await _context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .Include(a => a.User)
            .Where(a => a.Status != AppointmentStatus.Canceled)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<List<Appointment>> GetByVeterinarianUserIdAsync(int vetUserId)
        => await _context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .Include(a => a.User)
            .Where(a => a.Veterinarian.UserId == vetUserId &&
                        a.Status != AppointmentStatus.Canceled)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<bool> ExistsConflictAsync(int veterinarianId, DateTime appointmentDate)
    {
        return await _context.Appointments.AnyAsync(a =>
            a.VeterinarianId == veterinarianId &&
            a.AppointmentDate.Date == appointmentDate.Date &&
            a.AppointmentDate.Hour == appointmentDate.Hour &&
            a.Status != AppointmentStatus.Canceled);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}