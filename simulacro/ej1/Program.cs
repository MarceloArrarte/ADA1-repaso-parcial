// Antipatrón detectado: Lava flow
// A sabiendas de que nuevos métodos de cálculos de sueldo se podrán agregar en un corto plazo,
// asumiendo que solo el método de cálculo de sueldo es variable, voy a implementar un
// Strategy, con una estrategia por cada forma de cálculo de sueldo que se asocia a Employee.
// Además, corrige la violación de SRP y patrón Expert que implica el cálculo de sueldo de
// cada empleado individualmente en el método EmployeeManager.CalculateTotalPayroll, ya que
// ese método debe calcular el total de la nómina.
// Además, cada una instancia de una estrategia concreta tiene comportamiento pero no estado, por lo que
// puede implementarse el patrón Singleton en cada una.
// 
// Si hay más diferencias entre distintos tipos de empleados (registro de horas, pedido de licencias, etc)
// podría valer la pena implementar un Factory, con una fábrica por subtipo de empleado.

class Program
{
    static void Main(string[] args)
    {
        var employeeManager = new EmployeeManager();

        employeeManager.AddEmployee(new Employee {
            Name = "Lala",
            HoursWorked = 40,
            HourlyRate = 25,
            PayrollCalculationStrategy = LinearHourlyPayrollCalculationStrategy.Instance
        });
        employeeManager.AddEmployee(new Employee {
            Name = "Pepe",
            HoursWorked = 50,
            HourlyRate = 20,
            PayrollCalculationStrategy = LinearHourlyPayrollCalculationStrategy.Instance
        });

        Console.WriteLine("Total Payroll: $" + employeeManager.CalculateTotalPayroll());
    }
}

public class Employee
{
    public string Name { get; set; }
    public int HoursWorked { get; set; }
    public double HourlyRate { get; set; }
    public PayrollCalculationStrategy PayrollCalculationStrategy { get; set; }

    public double CalculatePayroll()
    {
        return PayrollCalculationStrategy.CalculatePayroll(HoursWorked, HourlyRate);
    }
}

public abstract class PayrollCalculationStrategy
{
    public abstract double CalculatePayroll(int hoursWorked, double hourlyRate);
}

public class LinearHourlyPayrollCalculationStrategy : PayrollCalculationStrategy
{
    private LinearHourlyPayrollCalculationStrategy() {}

    private static LinearHourlyPayrollCalculationStrategy _instance;
    public static LinearHourlyPayrollCalculationStrategy Instance
    {
        get {
            _instance ??= new LinearHourlyPayrollCalculationStrategy();
            return _instance;
        }
    }

    public override double CalculatePayroll(int hoursWorked, double hourlyRate)
    {
        return hoursWorked * hourlyRate;
    }
}

public class EmployeeManager
{
    private List<Employee> employees = new List<Employee>();
    
    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
    }

    public double CalculateTotalPayroll()
    {
        double total = 0;
        foreach (var employee in employees)
        {
            total += employee.CalculatePayroll();
        }
        return total;
    }
}