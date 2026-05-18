using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Domain.Entities;
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
    {
        return await _context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Appointment>> GetUserAppointmentsAsync(int userId)
    {
        return await _context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Pet)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<bool> ExistsConflictAsync(int veterinarianId, DateTime appointmentDate)
    {
        return await _context.Appointments.AnyAsync(a =>
            a.VeterinarianId == veterinarianId &&
            a.AppointmentDate == appointmentDate &&
            a.Status != Domain.Enums.AppointmentStatus.Canceled);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}