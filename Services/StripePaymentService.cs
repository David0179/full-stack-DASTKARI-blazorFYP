using Stripe.Checkout;

namespace Services
{

    public class StripePaymentService : IStripePaymentService
    {
        public string CreateCheckoutSession(decimal amount, string productName)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(amount * 100), // Stripe uses cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = productName
                        }
                    },
                    Quantity = 1
                }
            },
                Mode = "payment",
                SuccessUrl = "https://localhost:7276/success",
                CancelUrl = "https://localhost:7276/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return session.Url;
        }
    }

}
