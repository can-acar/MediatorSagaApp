namespace Core;

public class Description
{
}

    /*
    * There are 3 different projects under a solution structure.
    * First project "MainApp", 2nd project "OrderService", 3rd "PaymentService".
    * Running "MainApp" will open an OrderProcessing and PaymentProcessing channel.
    * When the "OrderService" project listening to the OrderProcessing channel is made, a payment order ("CreateOrderRequest") is made.
    * The function "CreateOrderHandler" listening to the "OrderProcessing" channel will be triggered and will return with a defined type to
    * The person who made the order from the same channel after completing the process steps.
    * The function that listens to the "OrderProcessing" channel in "MainApp" will print the values returned to the screen.
    * Then, if the "Order" status is true, the "CreatePaymentRequest" notification will be made from the "PaymentProcessing" channel and
    * the "CreatePaymentHandler" listening function in the "PaymentService" will be triggered.
    * After this function completes the "Payment" steps, a return will be made in the defined format.
    * The function that listens for "PaymentProcessing" in "MainApp" will print the values returned to the screen.
    */
  