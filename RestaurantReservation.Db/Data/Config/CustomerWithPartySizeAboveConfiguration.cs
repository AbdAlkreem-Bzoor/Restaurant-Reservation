using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantReservation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReservation.Db.Data.Config
{
    public class CustomerWithPartySizeAboveConfiguration
        : IEntityTypeConfiguration<CustomerWithPartySizeAbove>
    {
        public void Configure(EntityTypeBuilder<CustomerWithPartySizeAbove> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.PhoneNumber).HasColumnName("Phone Number");
        }
    }
}
