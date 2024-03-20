using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public enum Department
    {
        IT,
        HR,
        Finance
    }

    public class Employee : Person
    {
        public int EmployeeID { get; private set; }
        public double Salary { get; set; }
        public Department Department { get; set; }

        private static int totalEmployees = 0;

        public Employee(int employeeID, string firstName, string lastName, double salary, Department department)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            Department = department;
            totalEmployees++;
        }

        ~Employee()
        {
            Console.WriteLine($"Employee {EmployeeID} is being destroyed.");
        }
    }

    public interface IManager
    {
        void AssignEmployeeToDepartment(Employee employee, Department department);
    }

    public class EmployeeManager : IManager
    {
        private List<Employee> employees = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void RemoveEmployee(int employeeID)
        {
            employees.RemoveAll(emp => emp.EmployeeID == employeeID);
        }

        public void DisplayEmployees()
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.EmployeeID}, Name: {employee.FirstName} {employee.LastName}, Salary: {employee.Salary}");
            }
        }

        public double CalculateTotalSalary()
        {
            double totalSalary = 0;
            foreach (var employee in employees)
            {
                totalSalary += employee.Salary;
            }
            return totalSalary;
        }

        public void AssignEmployeeToDepartment(Employee employee, Department department)
        {
            employee.Department = department;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();

            string[] selectOptions = { "[1] Add Employee Information", "[2] Remove Employee by ID", "[3] Show Employee Details", "[4] Exit App" };

            while (true)
            {
                Console.WriteLine("Employee Management System");
                foreach (string option in selectOptions)
                {
                    Console.WriteLine(option);
                }

                Console.Write("Choose the operation number(1-4): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Add Employee Information
                        Console.WriteLine("Enter Employee Details:");
                        Console.Write("Employee ID: ");
                        int employeeID = int.Parse(Console.ReadLine());
                        Console.Write("First Name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last Name: ");
                        string lastName = Console.ReadLine();
                        Console.Write("Salary: ");
                        double salary = double.Parse(Console.ReadLine());
                        Console.Write("Department (0 for IT, 1 for HR, 2 for Finance): ");
                        Department department = (Department)int.Parse(Console.ReadLine());
                        manager.AddEmployee(new Employee(employeeID, firstName, lastName, salary, department));
                        break;

                    case "2":
                        // Remove Employee by ID
                        Console.Write("Enter Employee ID to remove: ");
                        int idToRemove = int.Parse(Console.ReadLine());
                        manager.RemoveEmployee(idToRemove);
                        break;

                    case "3":
                        // Show Employee Details
                        Console.WriteLine("Employee Details:");
                        manager.DisplayEmployees();
                        Console.WriteLine($"Total Salary: {manager.CalculateTotalSalary()}");
                        break;

                    case "4":
                        // Exit App
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid operation.");
                        break;
                }
            }
        }
    }
}
