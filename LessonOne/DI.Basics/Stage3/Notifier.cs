using System;

namespace DI.Basics.Stage3
{
    public class Notifier : INotifier
    {
        public void SendReceipt(OrderInfo orderInfo)
        {
            // send email to customer with receipt
            Console.WriteLine(string.Format("Receipt sent to customer '{0}' via email.", orderInfo.CustomerName));
        }
    }

    public interface INotifier
    {
        void SendReceipt(OrderInfo orderInfo);
    }
}