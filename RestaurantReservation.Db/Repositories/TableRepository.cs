using RestaurantReservation.Db.Data;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Repositories
{
    public class TableRepository
    {
        private readonly RestaurantReservationDbContext _context;

        public TableRepository(RestaurantReservationDbContext context)
        {
            _context = context;
        }

        public async Task CreateTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTableAsync(Table updatedTable)
        {
            var table = await _context.Tables.FindAsync(updatedTable.TableId)
                              ??
                              throw new KeyNotFoundException("Table not found");

            table.Capacity = updatedTable.Capacity;

            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId)
                              ??
                              throw new KeyNotFoundException("Table not found");

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }
    }
}
