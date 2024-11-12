using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationStatusRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public ReservationStatusRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task CreateReservationStatusAsync(ReservationStatus reservationStatus)
        {
            await _context.ReservationsStatus.AddAsync(reservationStatus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationStatusAsync(ReservationStatus updatedReservationStatus)
        {
            var reservationStatus = await _context.ReservationsStatus
                                         .FindAsync(updatedReservationStatus.ReservationStatusId)
                                    ??
                                    throw
                                    new KeyNotFoundException("Reservation Status not found");

            reservationStatus.Status = updatedReservationStatus.Status;
            reservationStatus.StatusDate = updatedReservationStatus.StatusDate;

            _context.ReservationsStatus.Update(reservationStatus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationStatusAsync(int reservationId)
        {
            var reservationStatus = await _context.ReservationsStatus
                                          .FirstOrDefaultAsync
                                          (r => r.ReservationId == reservationId)
                                    ??
                                    throw
                                    new KeyNotFoundException("Reservation Status not found");

            _context.ReservationsStatus.Remove(reservationStatus);
            await _context.SaveChangesAsync();
        }
    }

}



