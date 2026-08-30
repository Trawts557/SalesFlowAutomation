
using SalesFlowAutomation.Domain.Enums;
using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Domain.Entities
{
    public class Payment
    {
        public int Id { get; private set; }
        public int SaleId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime? PaidAt { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;
        public PaymentMethod PaymentMethod { get; private set; }

        public Payment(decimal amount, PaymentMethod paymentMethod)
        {

            if (amount <= 0)
                throw new DomainException("Amount must be greater than zero");

            Amount = amount;
            PaymentMethod = paymentMethod;
        }

        public void MarkAsPaid()
        {
            if (PaymentStatus == PaymentStatus.Paid)
                return;

            PaymentStatus = PaymentStatus.Paid;
            PaidAt = DateTime.UtcNow;
        }
    }
}
