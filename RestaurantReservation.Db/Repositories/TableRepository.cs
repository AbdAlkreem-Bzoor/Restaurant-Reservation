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
        public async Task<List<Table>> GetAvailableTablesAsync()
        {
            var allTables = await _context.Tables
                                          .AsNoTracking()
                                          .ToListAsync();

            var reservedTables = await _context.Reservations.Include(x => x.Table)
                                                            .Select(x => x.Table)
                                                            .Distinct()
                                                            .ToListAsync();

            return allTables.Except(reservedTables, new TableComparer())
                            .ToList();
        }
    }
    public class TableComparer : IEqualityComparer<Table>
    {
        public bool Equals(Table? x, Table? y)
        {
            if (x is null || y is null) return false;

            return x.TableId == y.TableId;
        }

        public int GetHashCode(Table obj)
        {
            return obj.TableId.GetHashCode();
        }
    }
}
