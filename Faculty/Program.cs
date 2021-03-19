using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Faculty.Models;
using System.Threading.Tasks;

namespace Faculty
{
    class Program
    {
        private const string _constring = @"Data source = WIN-5RSC18PG333\SQLEXPRESS;initial catalog=Faculty;integrated security = true";
        static void Main(string[] args)
        {
            while (true)
            {
                int num;
                Console.Write("Select all --> 1\nInsert --> 2\nUpdate --> 3\nDelete --> 4\nChoice = ");
                num = int.Parse(Console.ReadLine());
                if (num == 1) { SelectAll(); }
                if (num == 2) { Insert(); }
                if (num == 3) { Console.Write("Enter Id = "); Update(int.Parse(Console.ReadLine())); }
                if (num == 4) { Console.Write("Enter Id = "); Delete(int.Parse(Console.ReadLine())); }
                Console.Clear();
            }

        }

        public static void SelectAll()
        {
            Console.Clear();
            try
            {
                using (IDbConnection con = new SqlConnection(_constring))
                {
                    var result = con.Query<Student>("Select * from Students").ToList();
                    foreach (var item in result)
                    {
                        Console.WriteLine($"First Name: " + item.FirstName +
                            "\nLast Name: " + item.LastName + "\nMiddle Name: "
                            + item.MiddleName + "\nDate of Birth: " + item.DateOfBirth);
                    }

                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static void Insert()
        {
            Console.Clear();
            var @newStudent = new Student();
            Console.Write("Enter your name = ");
            newStudent.FirstName = Console.ReadLine();
            Console.Write("Enter your Last name = ");
            newStudent.LastName = Console.ReadLine();
            Console.Write("Enter your Middle name = ");
            newStudent.MiddleName = Console.ReadLine();
            Console.Write("Enter your date of birth = ");
            newStudent.DateOfBirth = DateTime.Parse(Console.ReadLine());
            using (IDbConnection con = new SqlConnection(_constring))
            {
                
                var result = con.ExecuteScalar<Student>("Insert into Students(FirstName, LastName, MiddleName, DateOfBirth) " +
                                                       "values (@FirstName, @LastName, @MiddleName, @DateOfBirth)", newStudent);

                Console.WriteLine(result);
                Console.WriteLine("Successfully inserted!");
                Console.ReadKey();
            }
        }
        public static void Update(int Id)
        {
            Console.Clear();
            var @newStudent = new Student();
            Console.Write("Enter new name = ");
            newStudent.FirstName = Console.ReadLine();
            Console.Write("Enter new Last name = ");
            newStudent.LastName = Console.ReadLine();
            Console.Write("Enter new Middle name = ");
            newStudent.MiddleName = Console.ReadLine();
            using (IDbConnection con = new SqlConnection(_constring))
            {
                try
                {

                    var result = con.ExecuteScalar<Student>($"Update Students Set FirstName = @FirstName, LastName = @LastName,  MiddleName = @MiddleName where Id = '{Id}'", newStudent);
                    

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка в области --> {ex.Message}");
                }
                Console.WriteLine("Successfully Updated!");
                Console.ReadKey();
            }
        } 

        public static void Delete(int Id)
        {
            Console.Clear();
            using (IDbConnection con = new SqlConnection(_constring))
            {
                try
                {
                    var result = con.ExecuteScalar<Student>($"Delete from Students WHERE Id = {Id}");
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Ошибка в области --> {ex.Message}");
                }
               
            }
           
            Console.WriteLine("Successfully Deleted!");
            Console.ReadKey();
        }

    }

    
}
