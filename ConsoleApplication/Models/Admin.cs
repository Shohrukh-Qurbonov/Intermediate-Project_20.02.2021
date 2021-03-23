using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication.Models
{
    public static class Admin
    {
        static SqlConnection connection = new SqlConnection(Connect.path);
        public static int Id { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }

        public static bool CheckAdmin()
        {
            Console.Clear(); Color.GetMagentaColor();
            Console.WriteLine("* - обязательная поля!");
            Color.GetResetColor();
            Login = ConsoleWriteWithResult("Логин (*): ");
            Password = ConsoleWriteWithResult("Пароль (*): ");
            string commandSql = "SELECT * FROM Admins";
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            SqlCommand command = new SqlCommand(commandSql, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (Login == reader.GetValue(1).ToString() && Password == reader.GetValue(2).ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static void AddAdmin()
        {
            Console.Clear(); Color.GetMagentaColor();
            Console.WriteLine("* - обязательная поля!");
            Color.GetResetColor();
            Console.Clear(); Color.GetMagentaColor();
            Console.WriteLine("Добавление нового Админа!");
            Color.GetResetColor();
            Login = ConsoleWriteWithResult("Введите новый логин (*): ");
            Password = ConsoleWriteWithResult("Введите новый пароль (*): ");
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string commandSql = "SELECT * FROM Admins";
            bool result = false;
            using (SqlCommand command = new SqlCommand(commandSql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Login == reader.GetValue(1).ToString())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            if (!result)
            {
                string commandSqlIns = $"INSERT INTO Admins ([Login],[Password]) VALUES ('{Login}','{Password}')";
                SqlCommand command = new SqlCommand(commandSqlIns, connection);
                command.ExecuteNonQuery();
                Color.GetGreenColor();
                Console.WriteLine("Новый Админ успешно добавлен!");
                Color.GetResetColor();
                Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
                Console.ReadKey();
            }
            else
            {
                Color.GetRedColor();
                Console.WriteLine("Админ с таким Логином уже существует!");
                Color.GetResetColor();
                Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
                Console.ReadKey();
            }
        }
        public static void DeleteAdmin()
        {
            Console.WriteLine("Удалить существующий Админ!");
            Console.Clear(); Color.GetMagentaColor();
            Console.WriteLine("* - обязательная поля!");
            Color.GetResetColor();
            Id = int.Parse(ConsoleWriteWithResult("Введите Id (*): "));
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string commandSql = $"DELETE Admins WHERE Id = {Id}";
            using (SqlCommand command = new SqlCommand(commandSql, connection))
            {
                if (command.ExecuteNonQuery() > 0)
                {
                    Color.GetGreenColor();
                    Console.WriteLine("Успешно удалено!");
                    Color.GetResetColor();
                    Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
                    Console.ReadKey();
                }
                else
                {
                    Color.GetRedColor();
                    Console.WriteLine("Такой Id не существуеть в базе данных!");
                    Color.GetResetColor();
                    Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
                    Console.ReadKey();
                }
            }
        }
        public static string ConsoleWriteWithResult(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }
    }
}
