using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class ReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public ReservationRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task CreateReservationAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(Reservation updatedReservation)
        {
            var reservation = await _context.Reservations.FindAsync(updatedReservation.ReservationId)
                                    ??
                                    throw new KeyNotFoundException("Reservation not found");

            reservation.ReservationDate = updatedReservation.ReservationDate;
            reservation.PartySize = updatedReservation.PartySize;

            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId)
                                    ??
                                    throw new KeyNotFoundException("Reservation not found");

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Reservation>> GetReservationsByCustomerAsync(int customerId)
        {
            return await _context.Customers.AsNoTracking()
                                    .Include(c => c.Reservations)
                                    .Where(c => c.CustomerId == customerId)
                                    .SelectMany(c => c.Reservations)
                                    .ToListAsync();
        }
        public async Task<List<ReservationCustomerRestaurantDetail>>
            ReservationsWithCustomerAndRestaurantAsync()
        {
            return await _context.ReservationsCustomerRestaurantDetails.AsNoTracking()
                                                                       .ToListAsync();
        }
        public async Task<List<int>> FindReservationsWithoutOrders()
        {
            return await _context.Reservations.Include(x => x.Orders)
                                              .Where(x => x.Orders.Count == 0)
                                              .Select(x => x.ReservationId)
                                              .ToListAsync();
        }
        public async Task<List<int>> FindReservationsByPredicateOnDate(DateOnly date,
            Func<DateOnly, DateOnly, bool> predicate)
        {
            return await _context.Reservations
                                 .Where(
                                        x =>
                                        predicate(x.ReservationDate, date)
                                       )
                                 .Select(x => x.ReservationId)
                                 .ToListAsync();
        }
    }

}



