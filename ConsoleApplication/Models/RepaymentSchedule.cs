using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApplication.Models
{
    class RepaymentSchedule
    {
        static SqlConnection connection = new SqlConnection(Connect.path);
        public static void RepaymentSchAll()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string commandSQL = $"SELECT c.FirstName, c.LastName, c.PassportNumber, rs.DatePay, rs.DatePayed, rs.AmountPay, rs.AmountPayed " +
                                $"FROM " +
                                $"RepaymentSchedules rs " +
                                $"JOIN Customers c " +
                                $"ON " +
                                $"rs.PassportNumber = c.PassportNumber " +
                                $"ORDER " +
                                $"BY rs.PassportNumber, rs.DatePay";
            using (SqlCommand command = new SqlCommand(commandSQL, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetValue("FirstName").ToString()}\t\t" +
                                          $"{reader.GetValue("LastName").ToString()}\t\t" +
                                          $"{reader.GetValue("DatePay").ToString()}\t\t" +
                                          $"{reader.GetValue("DatePayed").ToString()}\t\t" +
                                          $"{reader.GetValue("AmountPay").ToString()}\t\t" +
                                          $"{reader.GetValue("AmountPayed").ToString()}");
                    }
                }
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void RepaymentSchByPassNO()
        {
            Console.Write("Введите PassportNumber: ");
            string PassNO = Console.ReadLine();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string commandSQL = $"SELECT c.FirstName, c.LastName, c.PassportNumber, rs.DatePay, rs.DatePayed, rs.AmountPay, rs.AmountPayed " +
                                $"FROM " +
                                $"RepaymentSchedules rs " +
                                $"JOIN Customers c " +
                                $"ON " +
                                $"rs.PassportNumber = c.PassportNumber " +
                                $"WHERE c.PassportNumber = '{PassNO}' " +
                                $"ORDER " +
                                $"BY rs.PassportNumber, rs.DatePay";
            using (SqlCommand command = new SqlCommand(commandSQL, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetValue("FirstName").ToString()}\t\t" +
                                          $"{reader.GetValue("LastName").ToString()}\t\t" +
                                          $"{reader.GetValue("DatePay").ToString()}\t\t" +
                                          $"{reader.GetValue("DatePayed").ToString()}\t\t" +
                                          $"{reader.GetValue("AmountPay").ToString()}\t\t" +
                                          $"{reader.GetValue("AmountPayed").ToString()}");
                    }
                }
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void RepaymentSchDelByPassNO()
        {
            bool IsDelCustomer = true;
            Console.WriteLine("Удалить существующые Графики погашение!");
            Console.Clear(); Color.GetMagentaColor();
            Console.WriteLine("* - обязательная поля!");
            Color.GetResetColor();
            while (IsDelCustomer)
            {
                Console.Write("Введите пасспорт клиента (*): ");
                string PassNO = Console.ReadLine();
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string commandSql = $"DELETE RepaymentSchedules WHERE PassportNumber = '{PassNO}'";
                using (SqlCommand command = new SqlCommand(commandSql, connection))
                {
                    if (command.ExecuteNonQuery() > 0)
                    {
                        Color.GetGreenColor();
                        Console.WriteLine("Успешно удалено!");
                        Color.GetResetColor();
                        Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
                        Console.ReadKey();
                        IsDelCustomer = false;
                    }
                    else
                    {
                        Color.GetRedColor();
                        Console.WriteLine("Такой логин не существуеть в базе данных!");
                        Color.GetResetColor();
                    }
                }

            }

        }

    }
}