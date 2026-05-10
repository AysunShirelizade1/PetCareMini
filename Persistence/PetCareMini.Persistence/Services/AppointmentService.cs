using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Appointment;
using PetCareMini.Domain.Entities;
using PetCareMini.Domain.Enums;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly AppDbContext _context;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        AppDbContext context)
    {
        _appointmentRepository = appointmentRepository;
        _context = context;
    }

    public async Task CreateAsync(int userId, AppointmentCreateDto dto)
    {
        if (dto.AppointmentDate <= DateTime.UtcNow)
            throw new ArgumentException("Appointment date must be in the future.");

        var pet = await _context.Pets.FindAsync(dto.PetId);

        if (pet is null)
            throw new KeyNotFoundException("Pet not found.");

        if (pet.OwnerId != userId)
            throw new UnauthorizedAccessException("This pet does not belong to user.");

        var veterinarian = await _context.Veterinarians.FindAsync(dto.VeterinarianId);

        if (veterinarian is null)
            throw new KeyNotFoundException("Veterinarian not found.");

        if (!veterinarian.IsAvailable)
            throw new InvalidOperationException("Veterinarian is not available.");

        var service = await _context.Services.FindAsync(dto.ServiceId);

        if (service is null)
            throw new KeyNotFoundException("Service not found.");

        bool existsConflict = await _appointmentRepository
            .ExistsConflictAsync(dto.VeterinarianId, dto.AppointmentDate);

        if (existsConflict)
            throw new InvalidOperationException("This time slot is already booked.");

        var appointment = new Appointment
        {
            UserId = userId,
            PetId = dto.PetId,
            VeterinarianId = dto.VeterinarianId,
            ServiceId = dto.ServiceId,
            AppointmentDate = dto.AppointmentDate,
            Notes = dto.Notes
        };

        await _appointmentRepository.CreateAsync(appointment);
        await _appointmentRepository.SaveChangesAsync();
    }

    public async Task<List<AppointmentGetDto>> GetUserAppointmentsAsync(int userId)
    {
        var appointments = await _appointmentRepository
            .GetUserAppointmentsAsync(userId);

        return appointments.Select(a => new AppointmentGetDto
        {
            Id = a.Id,
            PetName = a.Pet.Name,
            VeterinarianName = a.Veterinarian.FullName,
            ServiceName = a.Service.NameEn,
            AppointmentDate = a.AppointmentDate,
            Status = a.Status.ToString(),
            Notes = a.Notes
        }).ToList();
    }

    public async Task<List<AppointmentGetDto>> GetAllAsync()
    {
        var appointments = await _appointmentRepository.GetAllAsync();

        return appointments.Select(a => new AppointmentGetDto
        {
            Id = a.Id,
            PetName = a.Pet.Name,
            VeterinarianName = a.Veterinarian.FullName,
            ServiceName = a.Service.NameEn,
            AppointmentDate = a.AppointmentDate,
            Status = a.Status.ToString(),
            Notes = a.Notes
        }).ToList();
    }

    public async Task UpdateStatusAsync(int appointmentId, AppointmentStatusUpdateDto dto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

        if (appointment is null)
            throw new KeyNotFoundException("Appointment not found.");

        appointment.Status = (AppointmentStatus)dto.Status;

        await _appointmentRepository.SaveChangesAsync();
    }
}