// Implementaremos un adapter que permita que el código cliente utilice 
// el nuevo RestPaymentProcessor sin cambiar la interfaz que utiliza
// para comunicarse con SocketPaymentProcessor.

class Program
{
    static void Main()
    {
        SocketPaymentProcessor paymentProcessor = new RestPaymentProcessorAdapter();

        paymentProcessor.ExecutePayment("user1", 100.0);
    }
}


public class SocketPaymentProcessor
{
    public virtual void ExecutePayment(string username, double payment)
    {
        Console.WriteLine($"Executing payment for {username} through legacy socket-based system.");
    }
}

public class RestPaymentProcessor
{
    public void MakePayment(string username, double payment)
    {
        Console.WriteLine($"Executing payment for {username} through brand-new REST system.");
    }
}

public class RestPaymentProcessorAdapter : SocketPaymentProcessor
{
    private RestPaymentProcessor adaptee = new();

    public override void ExecutePayment(string username, double payment)
    {
        adaptee.MakePayment(username, payment);
    }
}