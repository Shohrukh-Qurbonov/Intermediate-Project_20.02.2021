using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApplication.Models
{
    public static class Customer
    {
        static SqlConnection connection = new SqlConnection(Connect.path);
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string MiddleName { get; set; }
        public static DateTime BirthDate { get; set; }
        public static string Gender { get; set; }
        public static string Nationality { get; set; }
        public static string MaritialStatus { get; set; }
        public static string PassportNO { get; set; }
        public static int Login { get; set; }
        public static string Password { get; set; }
        
        public static bool CheckClient()
        {
            Console.Clear();
            bool IsCheckClient = true;
            Console.WriteLine($"{new string('-', 20)}Панель входа{new string('-', 20)}");
            Color.GetMagentaColor();
            Console.WriteLine("* - обязательная поля!");
            Color.GetResetColor();
            while (IsCheckClient)
            {
                Console.Write("Введите свой номера телефона (*): ");
                string login = Console.ReadLine();
                if (login.Length == 9 && int.TryParse(login, out int log))
                {
                    Login = int.Parse(login);
                    IsCheckClient = false;
                }
                else
                {
                    Console.Clear();
                    Color.GetRedColor();
                    Console.WriteLine("Неправильный логин!");
                    Color.GetResetColor();
                }
            }
            Password = Admin.ConsoleWriteWithResult("Введите свой пароль (*): ");
            if (connection.State == ConnectionState.Closed) 
                connection.Open();
            string commandSql = "SELECT * FROM Customers";
            SqlCommand command = new SqlCommand(commandSql, connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (Login == int.Parse(reader.GetValue("Login").ToString()) && 
                        Password == reader.GetValue("Password").ToString())
                    {
                        FirstName = reader.GetValue("FirstName").ToString();
                        LastName = reader.GetValue("LastName").ToString();
                        MiddleName = reader.GetValue("MiddleName").ToString();
                        BirthDate = DateTime.Parse(reader.GetValue("BirthDate").ToString());
                        Gender = reader.GetValue("Gender").ToString();
                        Nationality = reader.GetValue("Nationality").ToString();
                        MaritialStatus = reader.GetValue("MaritalStatus").ToString();
                        PassportNO = reader.GetValue("PassportNumber").ToString();
                        Login = int.Parse(reader.GetValue("Login").ToString());
                        Password = reader.GetValue("Password").ToString();
                        return true;
                    }
                }
            }
            return false;
        } 
        public static bool Registration()
        {
            bool isPhone = true;
            bool IsGender = true;
            bool IsStatus = true;
            bool IsNation = true;
            bool IsBirthDate = true;
            bool IsPassport = true;
            bool isPasswordTrue = true;
            string Birthdate;
            Console.Clear();
            Console.WriteLine($"{new string('-', 20)}Регистрационный панель{new string('-', 20)}");
            Color.GetMagentaColor();
            Console.WriteLine("* - обязательная поля!");
            Color.GetResetColor();
            FirstName = Admin.ConsoleWriteWithResult("Введите имя (*): ");
            LastName = Admin.ConsoleWriteWithResult("Введите фамилию: ");
            MiddleName = Admin.ConsoleWriteWithResult("Введите отчество: ");
            while (IsBirthDate)
            {
                Console.Write("Введите дату рождение (дд-мм-гггг)(*): ");
                Birthdate = Console.ReadLine();
                if (DateTime.TryParse(Birthdate, out DateTime birthDate))
                {
                    BirthDate = birthDate;
                    IsBirthDate = false;
                }
                else
                {
                    Color.GetRedColor();
                    Console.WriteLine("Неверная дата рождения!");
                    Color.GetResetColor();
                }
            }
            while (IsGender)
            {
                Console.Write("Ваш пол:\n\t1. Женской\n\t2. Мужской\n\tВаш выбор: ");
                switch (Console.ReadLine())
                {
                    case "1": { Gender = "Женской"; IsGender = false; } break;
                    case "2": { Gender = "Мужской"; IsGender = false; } break;
                    default:
                        {
                            Color.GetRedColor();
                            Console.WriteLine("Неправильная комманда!");
                            Color.GetResetColor();
                        }
                        break;
                }
            }
            while (IsNation)
            {
                Console.Write("Ваша страна:\n\t1. Таджикистан\n\t2. Другая страна\n\tВаш выбор: ");
                switch (Console.ReadLine())
                {
                    case "1": { Nationality = "Таджикистан"; IsNation = false; } break;
                    case "2": { Nationality = "Другая страна"; IsNation = false; } break;
                    default:
                        {
                            Color.GetRedColor();
                            Console.WriteLine("Неправильная комманда!");
                            Color.GetResetColor();
                        }
                        break;
                }
            }
            while (IsStatus)
            {
                Console.Write("Cемейное положение:\n\t1. Холост\n\t2. Семеянин\n\t3. Вразводе\n\t4. Вдовец/вдова\n\tВаш выбор: ");
                switch (Console.ReadLine())
                {
                    case "1": { MaritialStatus = "Холост"; IsStatus = false; } break;
                    case "2": { MaritialStatus = "Семеянин"; IsStatus = false; } break;
                    case "3": { MaritialStatus = "Вразводе"; IsStatus = false; } break;
                    case "4": { MaritialStatus = "Вдовец/вдова"; IsStatus = false; } break;
                    default:
                        {
                            Color.GetRedColor();
                            Console.WriteLine("Неправильная комманда!");
                            Color.GetResetColor();
                        }
                        break;
                }
            }
            while (IsPassport)
            {
                Console.Write("Введите номер пасспорта (*): ");
                string checkPassportNO = Console.ReadLine();
                if (checkPassportNO.Length == 8)
                {
                    PassportNO = checkPassportNO;
                    bool exist = CustomerExist();
                    if (exist)
                    {
                        Color.GetRedColor();
                        Console.WriteLine("Такой паспорт существует!");
                        Color.GetResetColor();
                        continue;
                    }
                    IsPassport = false;
                }
                else
                {
                    Color.GetRedColor();
                    Console.WriteLine("Неправильная серия паспорта!");
                    Color.GetResetColor();
                }
            }
            while (isPhone)
            {
                Console.Write("Введите номер телефона (920000000) (*): ");
                string login = Console.ReadLine();
                if (login.Length == 9 && int.TryParse(login, out int loginNumber))
                {
                    Login = int.Parse(login);
                    bool exist = CustomerExist();
                    if(exist)
                    {
                        Color.GetRedColor();
                        Console.WriteLine("Такой номер телефона уже существует!");
                        Color.GetResetColor();
                        continue;
                    }
                    isPhone = false;
                }
                else
                {
                    Color.GetRedColor();
                    Console.WriteLine("Неверьный логин!");
                    Color.GetResetColor();
                }
            }
            while (isPasswordTrue)
            {
                Password = Admin.ConsoleWriteWithResult("Введите пароль (*): ");
                Console.Write("Подтвердите пароль (*): ");
                string confirmPassword = Console.ReadLine();
                if (!(Password == confirmPassword))
                {
                    isPasswordTrue = true; Color.GetRedColor();
                    Console.WriteLine("Не соответствует!");
                    Color.GetResetColor();
                }
                else
                {
                    isPasswordTrue = false;
                }
            }
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            InsertCustomer();
            Color.GetGreenColor();
            Console.WriteLine("Регистрация успешно завершилась!");
            Color.GetResetColor();
            Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
            Console.ReadKey();
            return true;
        } 
        private static bool CustomerExist()
        {
            string commandSql = "SELECT * FROM Customers";
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                using (SqlCommand command = new SqlCommand(commandSql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (PassportNO == reader.GetValue("PassportNumber").ToString() || Login == int.Parse(reader.GetValue("Login").ToString()))
                            {
                                return true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        } 
        private static void InsertCustomer()
        {
            string commandSql = $"INSERT INTO Customers([FirstName],[LastName],[MiddleName],[BirthDate],[Gender]," +
                                         $"[Nationality],[MaritalStatus],[PassportNumber],[Login],[Password]) VALUES " +
                                         $"('{FirstName}','{LastName}','{MiddleName}','{BirthDate.Year}-{BirthDate.Month}-{BirthDate.Day}','{Gender}','{Nationality}','{MaritialStatus}','{PassportNO}','{Login}','{Password}')";
            SqlCommand command = new SqlCommand(commandSql, connection);
            command.ExecuteNonQuery();
        }  
        public static void GetCustomer()
        {
            switch (SelectAllOrByIdMenu())
            {
                case "1":
                    bool IsGetAllCustomer = true;
                    while (IsGetAllCustomer)
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string commandSql = "SELECT * FROM Customers";
                        using (SqlCommand command = new SqlCommand(commandSql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"{reader.GetValue("FirstName").ToString()}\t\t" +
                                                      $"{reader.GetValue("LastName").ToString()}\t\t" +
                                                      $"{reader.GetValue("MiddleName").ToString()}\t" +
                                                      $"{reader.GetValue("BirthDate").ToString()}\t" +
                                                      $"{reader.GetValue("Gender").ToString()}\t" +
                                                      $"{reader.GetValue("Nationality").ToString()}\t" +
                                                      $"{reader.GetValue("MaritalStatus").ToString()}\t" +                                                  
                                                      $"{reader.GetValue("Login").ToString()}\t" +                                                  
                                                      $"{reader.GetValue("PassportNumber").ToString()}");
                                                         IsGetAllCustomer = false;
                                }
                            }
                        }
                    }
                    Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "2":
                    bool IsGetAllCustomerId = true;
                    while (IsGetAllCustomerId)
                    {
                        Console.Write("Введите серии паспорта или логин: ");
                        string PassNO = Console.ReadLine();
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string comandSql = $"SELECT * FROM Customers";
                        using (SqlCommand command = new SqlCommand(comandSql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (PassNO == reader.GetValue("PassportNumber").ToString() || PassNO == reader.GetValue("Login").ToString())
                                    {
                                        Console.WriteLine($"{reader.GetValue("FirstName").ToString()}\t\t" +
                                                          $"{reader.GetValue("LastName").ToString()}\t\t" +
                                                          $"{reader.GetValue("MiddleName").ToString()}\t" +
                                                          $"{reader.GetValue("BirthDate").ToString()}\t" +
                                                          $"{reader.GetValue("Gender").ToString()}\t" +
                                                          $"{reader.GetValue("Nationality").ToString()}\t" +
                                                          $"{reader.GetValue("MaritalStatus").ToString()}\t" +
                                                          $"{reader.GetValue("Login").ToString()}\t" +
                                                          $"{reader.GetValue("PassportNumber").ToString()}");
                                                             IsGetAllCustomerId = false;
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Неправильная комманда!");
                    Console.ReadKey();
                    break;
            }
        } 
        public static void GetCustomerByPassNO()
        {
            Console.Write("Enter PassportNO: ");
            string PassNO = Console.ReadLine();
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string commandSQL = $"SELECT * FROM Customers";
            using (SqlCommand command = new SqlCommand(commandSQL, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if (PassNO == reader.GetValue("PassportNumber").ToString())
                        {
                            Console.WriteLine($"{reader.GetValue("FirstName").ToString()}\t" +
                                              $"{reader.GetValue("LastName").ToString()}\t" +
                                              $"{reader.GetValue("MiddleName").ToString()}\t" +
                                              $"{reader.GetValue("Gender").ToString()}\t" +
                                              $"{reader.GetValue("Nationality").ToString()}\t" +
                                              $"{reader.GetValue("MaritalStatus").ToString()}\t" +
                                              $"{reader.GetValue("BirthDate").ToString()}\t" +
                                              $"{reader.GetValue("PassportNumber").ToString()}");
                        }
                    }
                }
            }
            Console.ReadKey();
        } 
        public static void DeleteCustomer()
        {
            bool IsDelCustomer = true;
            Console.WriteLine("Удалить существующий Клиент!");
            Console.Clear(); Color.GetMagentaColor();
            Console.WriteLine("* - обязательная поля!");
            Color.GetResetColor();
            while (IsDelCustomer)
            {
                Console.Write("Введите логин клиента (*): ");
                int loginClient = Convert.ToInt32(Console.ReadLine());
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string commandSql = $"DELETE Customers WHERE Login = {loginClient}";
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
        public static string SelectAllOrByIdMenu()
        {
            Console.Clear();
            Console.WriteLine($"{new string('-', 20)}Команды для клиента{new string('-', 20)}");
            Console.Write($"\t1. Посмотреть список всех клиентов\n\t" +
                          $"2. Посмотреть по серии паспорта или логин\n\t" +
                          $"3. Назад\n\t" +
                          $"4. Выход\n\t" +
                          $"Ваш выбор: ");
            return Console.ReadLine();
        } 
    }
}
