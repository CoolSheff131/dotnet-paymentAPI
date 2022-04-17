using Microsoft.EntityFrameworkCore;

namespace dotnet_paymentAPI.Models
{
	public class PaymentDetailContext:DbContext
	{
		public PaymentDetailContext(DbContextOptions<PaymentDetailContext> options):base(options)
		{

		}

		public DbSet<PaymentDetail> PaymentDetails { get; set; }
	}
}
