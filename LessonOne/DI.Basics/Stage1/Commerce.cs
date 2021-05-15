namespace DI.Basics.Stage1
{
    public class Commerce
    {
        public void ProcessOrder(OrderInfo orderInfo)
        {
            var billingProcessor = new BillingProcessor();
            var customerProcessor = new CustomerProcessor();
            var notifier = new Notifier();

            billingProcessor.ProcessPayment(orderInfo.CustomerName, orderInfo.CreditCard, orderInfo.Price);
            customerProcessor.UpdateCustomerOrder(orderInfo.CustomerName, orderInfo.Product);
            notifier.SendReceipt(orderInfo);
        }
    }
}