
// Se implementará:
// 1) Chain of Responsibility para simplificar la notificación de los usuarios a través
//    de todos los sistemas de notificación utilizados.
// 2) Observer con una clase Usuario cuyas instancias puedan observar o no
//    el sistema de notificaciones para recibir mails/SMS

class Program
{
    static void Main()
    {
        NotificationSystemChain notifChain = new() {
            First = new EmailNotificationSystem() {
                Next = new SMSNotificationSystem()
            }
        };

        User u1 = new User { Name = "Marcelo", Email = "marce@gmail.com", PhoneNumber = "099123456" };
        User u2 = new User { Name = "José", Email = "pepe@gmail.com", PhoneNumber = "091987654" };

        notifChain.Subscribe(u1);
        notifChain.Subscribe(u2);
        notifChain.SendNotification("Se ganó un viaje a Miami!");

        notifChain.Unsubscribe(u1);
        notifChain.SendNotification("Era mentiraaa!");
    }
}


public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public void Notify(string message, string channel)
    {
        Console.WriteLine($"{Name} recibió el mensaje \"{message}\" por el canal \"{channel}\".");
    }
}


public class NotificationSystemChain
{
    private List<User> _users = [];

    public NotificationSystem First;

    public void SendNotification(string message)
    {
        foreach (var user in _users)
        {
            First.Handle(user, message);
        }
    }
    public void Subscribe(User user)
    {
        _users.Add(user);
    }

    public void Unsubscribe(User user)
    {
        _users.Remove(user);
    }
}

public abstract class NotificationSystem
{
    public NotificationSystem? Next;
    public void Handle(User user, string message)
    {
        InternalHandle(user, message);
        if (Next != null)
        {
            Next.Handle(user, message);
        }
    }

    protected abstract void InternalHandle(User user, string message);
}


public class EmailNotificationSystem : NotificationSystem
{
    protected override void InternalHandle(User user, string message)
    {
        Console.WriteLine($"Enviando email a {user.Email}...");
        user.Notify(message, "email");
    }
}


public class SMSNotificationSystem : NotificationSystem
{
    protected override void InternalHandle(User user, string message)
    {
        Console.WriteLine($"Enviando SMS a {user.PhoneNumber}...");
        user.Notify(message, "sms");
    }
}