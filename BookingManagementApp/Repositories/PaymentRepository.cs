using System.Threading.Tasks;
using BookingManagementApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingManagementApp.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Payment> GetPaymentByBookingIdAsync(int bookingId)
        {
            return await _dbSet
                .Include(p => p.Booking)
                .FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }

        public async Task<bool> UpdatePaymentStatusAsync(int paymentId, string status)
        {
            var payment = await _dbSet.FindAsync(paymentId);
            if (payment == null)
                return false;

            payment.PaymentStatus = status;
            await SaveChangesAsync();
            return true;
        }
    }
}