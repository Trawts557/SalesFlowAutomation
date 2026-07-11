using SalesFlowAutomation.Domain.Enums;
using SalesFlowAutomation.Domain.Entities;
using SalesFlowAutomation.Domain.Exceptions;

namespace SalesFlowAutomation.Tests.Domain
{
    public class PaymentTests
    {
        [Fact]
        public void Payment_WithInvalidAmount_ShouldThrowDomainException()
        {
            decimal amount = 0;
            PaymentMethod paymentMethod = PaymentMethod.Card;

            Assert.Throws<DomainException>(() => new Payment(amount, paymentMethod));
        }

        [Fact]
        public void Payment_WithValidData_ShouldStartAsPending()
        {
            decimal amount = 3000m;
            PaymentMethod paymentMethod = PaymentMethod.Cash;

            Payment payment = new(amount, paymentMethod);

            Assert.Equal(PaymentStatus.Pending, payment.PaymentStatus);
        }

        [Fact]
        public void Payment_MarkAsPaid_ShouldChangeStatusToPaid()
        {
            decimal amount = 3000m;
            PaymentMethod paymentMethod = PaymentMethod.Cash;

            Payment payment = new(amount, paymentMethod);

            payment.MarkAsPaid();

            Assert.Equal(PaymentStatus.Paid, payment.PaymentStatus);

        }

        [Fact]
        public void Payment_MarkAsPaid_ShouldSetPaidAt()
        {
            decimal amount = 3000m;
            PaymentMethod paymentMethod = PaymentMethod.Cash;

            Payment payment = new(amount, paymentMethod);

            payment.MarkAsPaid();

            Assert.NotNull(payment.PaidAt);
        }

        [Fact]
        public void Payment_MarkAsPaid_WhenAlreadyPaid_ShouldDoNothing()
        {
            decimal amount = 3000m;
            PaymentMethod paymentMethod = PaymentMethod.Cash;

            Payment payment = new(amount, paymentMethod);

            payment.MarkAsPaid();

            DateTime? firstPaidAt = payment.PaidAt;

            payment.MarkAsPaid();

            Assert.Equal(PaymentStatus.Paid, payment.PaymentStatus);
            Assert.Equal(firstPaidAt, payment.PaidAt);
            
        }
    }
}