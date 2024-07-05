// Se elige utilizar el patrón Memento debido a que es un requisito
// poder restaurar estados previos de las instancias de UserProfile.
// Esto es una solución escalable para el momento en que esta clase
// se vuelva más compleja y no sea viable restaurar el estado
// manualmente. 

class Program
{
    private static List<UserProfile.Memento> history = new List<UserProfile.Memento>();

    static void Main()
    {
        UserProfile profile = new UserProfile("Alice", 25);
        history.Add(profile.CreateMemento());

        Console.WriteLine("Original Profile:");
        profile.PrintProfile();
        
        profile.Name = "Bob";
        profile.Age = 30;
        Console.WriteLine("\nUpdated Profile:");
        profile.PrintProfile();

        profile.SetMemento(history.Last());
        Console.WriteLine("\nRestored Profile:");
        profile.PrintProfile();
    }
}

public class UserProfile
{
    public class Memento
    {
        private readonly string _name;
        private readonly int _age;

        public Memento(UserProfile state)
        {
            _name = state.Name;
            _age = state.Age;
        }
        public UserProfile GetState()
        {
            return new UserProfile(_name, _age);
        }
    }

    public string Name { get; set; }
    public int Age { get; set; }

    public UserProfile(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public Memento CreateMemento()
    {
        return new Memento(this);
    }

    public void SetMemento(Memento memento)
    {
        UserProfile state = memento.GetState();
        Name = state.Name;
        Age = state.Age;
    }

    public void PrintProfile()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}");
    }
}