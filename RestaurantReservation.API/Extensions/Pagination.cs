namespace RestaurantReservation.API.Extensions
{
    public static class Pagination
    {
        private const int MaxPageSize = 20;
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> values, 
                                                int pageNumber, int pageSize)
        {
            pageSize = Math.Min(pageSize, MaxPageSize);

            return values.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
