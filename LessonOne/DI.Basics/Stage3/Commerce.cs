namespace DI.Basics.Stage3
{
    public class Commerce
    {
        private readonly IBillingProcessor _billingProcessor;
        private readonly ICustomerProcessor _customerProcessor;
        private readonly INotifier _notifier;

        public Commerce(IBillingProcessor billingProcessor, ICustomerProcessor customerProcessor, INotifier notifier)
        {
            _billingProcessor = billingProcessor;
            _customerProcessor = customerProcessor;
            _notifier = notifier;
        }

        public void ProcessOrder(OrderInfo orderInfo)
        {
            _billingProcessor.ProcessPayment(orderInfo.CustomerName, orderInfo.CreditCard, orderInfo.Price);
            _customerProcessor.UpdateCustomerOrder(orderInfo.CustomerName, orderInfo.Product);
            _notifier.SendReceipt(orderInfo);
        }
    }
}