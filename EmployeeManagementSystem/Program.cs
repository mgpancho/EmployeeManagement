using System;
using System.Collections.Generic;

namespace EmployeeManagementSystem
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    //declare enum for department 
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
           // Console.WriteLine("ID\t\tEmployee Name\t\t\tSalary\tDepartment");

            foreach (var employee in employees)
            {
                //Console.WriteLine($"{employee.EmployeeID}\t{employee.FirstName} {employee.LastName}\t\t{employee.Salary}\t{employee.Department}");
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
                Console.WriteLine("\nEmployee Management System");
                foreach (string option in selectOptions)
                {
                    Console.WriteLine(option);
                }

                Console.Write("Choose the operation number(1-4): ");
                string choice = Console.ReadLine();

                bool invalid = false;
                switch (choice)
                {
                    case "1":
                        do
                        {
                            try
                            {
                                // Add Employee Information
                                Console.WriteLine("\nPlease Provide the Employee Details");
                                Console.Write("Employee ID: ");
                                int employeeID;
                                while (!int.TryParse(Console.ReadLine(), out employeeID))
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid Employee ID.");
                                    Console.Write("Employee ID: ");
                                }

                                Console.Write("Firstname: ");
                                string firstName = Console.ReadLine().Trim();
                                while (string.IsNullOrEmpty(firstName))
                                {
                                    Console.WriteLine("First name cannot be empty. Please enter a valid Firstname ");
                                    Console.Write("Firstname: ");
                                    firstName = Console.ReadLine().Trim();
                                }

                                Console.Write("Lastname: ");
                                string lastName = Console.ReadLine().Trim();
                                while (string.IsNullOrEmpty(lastName))
                                {
                                    Console.WriteLine("Last name cannot be empty. Please enter a valid Lastname: ");
                                    Console.Write("Lastname: ");
                                    lastName = Console.ReadLine().Trim();
                                }

                                Console.Write("Salary: ");
                                double salary;
                                while (!double.TryParse(Console.ReadLine(), out salary) || salary < 0)
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid Salary (a non-negative number): ");
                                    Console.Write("Salary: ");
                                }

                                Console.Write("Choose Department ([0] IT, [1] HR, [2] Finance): ");
                                int departmentInt;
                                while (!int.TryParse(Console.ReadLine(), out departmentInt) || !Enum.IsDefined(typeof(Department), departmentInt))
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid Department ([0] IT, [1] HR, [2] Finance): ");
                                    Console.Write("Department: ");
                                }
                                Department department = (Department)departmentInt;

                                manager.AddEmployee(new Employee(employeeID, firstName, lastName, salary, department));
                                invalid = true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                invalid = false;
                            }
                        } while (!invalid);



                        break;

                    case "2":
                        // Remove Employee by ID
                        Console.Write("Enter Employee ID to remove: ");
                        int idToRemove = int.Parse(Console.ReadLine());
                        manager.RemoveEmployee(idToRemove);
                        break;

                    case "3":
                        // Show Employee Details
                        Console.WriteLine("\nEmployee Details");
                        manager.DisplayEmployees();
                        Console.WriteLine($"Total Salary: {manager.CalculateTotalSalary()}\n");
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
