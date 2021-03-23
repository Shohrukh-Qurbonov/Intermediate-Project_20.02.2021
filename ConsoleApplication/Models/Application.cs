using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApplication.Models
{
    public static class Application
    {
        public static SqlConnection connection = new SqlConnection(Connect.path);
        public static int Id { get; set; }
        public static string PassportNO { get; set; }
        public static decimal AmountCredit { get; set; }
        public static decimal AmountPay { get; set; }
        public static DateTime DateApplication { get; set; }
        public static DateTime? DatePay { get; set; }
        public static string CreditStatus { get; set; }
        public static decimal CreditRemain { get; set; }
        public static int TotalIncome { get; set; }
        public static int Overdue { get; set; } = 0;
        public static string CreditAim { get; set; }
        public static int CreditTerm { get; set; }

        public static bool CreateApplication()
        {
            AmountCredit = int.Parse(Admin.ConsoleWriteWithResult("Сумма кредита: "));
            TotalIncome = int.Parse(Admin.ConsoleWriteWithResult("Общий доход: "));
            switch (CreditAimMenu())
            {
                case "1": CreditAim = "Бытовая техника"; break;
                case "2": CreditAim = "Ремонт"; break;
                case "3": CreditAim = "Телефон"; break;
                case "4": CreditAim = "Прочее"; break;
                default: Console.WriteLine("Неверная комманда!"); break;
            }
            CreditTerm = int.Parse(Admin.ConsoleWriteWithResult("Срок кредита(месяц): "));
            Overdue = 0;
            AmountPay = Math.Round(AmountCredit + AmountCredit * 0.2m,2);
            if(!(CreditTerm > 3 && CreditTerm <= 60))
            {
                Color.GetRedColor();
                Console.WriteLine("Cрок кредит дольжно быть от 3 до 60 месяцев!");
                Color.GetResetColor();
            }
            DateApplication = DateTime.Now;
            if (Calculation.CalculationsForApplications())
            {
                CreditRemain = AmountPay;
                CreditStatus = nameof(CreditStates.Open);
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                InsertApplication(); Color.GetGreenColor();
                Console.WriteLine("Вы можете брать кредит!\nДля пополнение заявки нажмите 1");
                Color.GetResetColor();
                if (Console.ReadLine() == "1")
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    for (int i = 0; i < CreditTerm; i++)
                    {
                        InsertSchedule();
                        DateApplication = DateApplication.AddMonths(1);
                    }
                    Color.GetGreenColor();
                    Console.WriteLine($"Вы взяли кредит в размере {AmountCredit} сомони на срок {CreditTerm} месяцев.\n\tПосмотрите свой график погашения!");
                    Color.GetResetColor(); Console.ReadKey();
                }
            }
            else
            {
                Color.GetRedColor();
                Console.WriteLine("Вы не можите взять кредит!");
                Color.GetResetColor(); Console.ReadKey();
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                CreditStatus = nameof(CreditStates.Reject);
                InsertApplication();
            }
            return true;
        }
        public static void GetAllAplication()
        {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string commandSql = $"SELECT c.FirstName, c.LastName, a.AmountCredit, a.AmountPay, a.ApplicationDate,a.CreditAim, a.CreditRemain, a.CreditStatus, a.CreditTerm, a.TotalIncome,a.PassportNumber FROM Customers c join Applications a on c.PassportNumber = a.PassportNumber";
                using (SqlCommand command = new SqlCommand(commandSql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetValue("FirstName").ToString()}\t" +
                                              $"{reader.GetValue("LastName").ToString()}\t" +
                                              $"{reader.GetValue("AmountCredit").ToString()}\t" +
                                              $"{reader.GetValue("AmountPay").ToString()}\t\t" +
                                              $"{reader.GetValue("ApplicationDate").ToString()}\t\t" +
                                              $"{reader.GetValue("CreditAim").ToString()}\t" +
                                              $"{reader.GetValue("CreditRemain").ToString()}\t" +
                                              $"{reader.GetValue("CreditStatus").ToString()}\t" +
                                              $"{reader.GetValue("CreditTerm").ToString()}\t"+
                                              $"{reader.GetValue("TotalIncome").ToString()}\t" +
                                              $"{reader.GetValue("PassportNumber").ToString()}");
                    }
                    }
                }
            Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void GetApplicationByPassNO()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Console.Write("Введите PassportNumber: ");
            string passNO = Console.ReadLine();
            string commandSql = $"SELECT c.FirstName, c.LastName, a.AmountCredit, a.AmountPay, a.ApplicationDate, " +
                                $"a.CreditAim, a.CreditRemain, a.CreditStatus, a.CreditTerm, a.TotalIncome,c.PassportNumber " +
                                $"FROM " +
                                $"Customers c " +
                                $"JOIN Applications a " +
                                $"on " +
                                $"c.PassportNumber = a.PassportNumber " +
                                $"WHERE c.PassportNumber = '{passNO}'";
            using (SqlCommand command = new SqlCommand(commandSql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetValue("FirstName").ToString()}\t" +
                                          $"{reader.GetValue("LastName").ToString()}\t" +
                                          $"{reader.GetValue("AmountCredit").ToString()}\t" +
                                          $"{reader.GetValue("AmountPay").ToString()}\t\t" +
                                          $"{reader.GetValue("ApplicationDate").ToString()}\t\t" +
                                          $"{reader.GetValue("CreditAim").ToString()}\t" +
                                          $"{reader.GetValue("CreditRemain").ToString()}\t" +
                                          $"{reader.GetValue("CreditStatus").ToString()}\t" +
                                          $"{reader.GetValue("CreditTerm").ToString()}\t" +
                                          $"{reader.GetValue("TotalIncome").ToString()}\t" +
                                          $"{reader.GetValue("PassportNumber").ToString()}");
                    }
                }
            }
            Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void DelApplicationByPassNO()
        {
            bool IsDelCustomer = true;
            Console.WriteLine("Удалить существующий Application!");
            Console.Clear(); Color.GetMagentaColor();
            Console.WriteLine("* - обязательная поля!");
            Color.GetResetColor();
            while (IsDelCustomer)
            {
                Console.Write("Введите passport клиента (*): ");
                string PassNO = Console.ReadLine();
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string commandSql = $"DELETE Applications WHERE PassportNumber = '{PassNO}'";
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
        private static void InsertSchedule()
        {
            string comandSql = $"INSERT INTO RepaymentSchedules([PassportNumber]," +
                                                                                    $"[AmountPay]," +
                                                                                    $"[DatePay])VALUES(" +
                                                                                    $"'{Customer.PassportNO}'," +
                                                                                    $"'{Math.Round((AmountPay / CreditTerm), 2).ToString().Replace(',','.')}'," +
                                                                                    $"'{DateApplication.Year}-{DateApplication.Month}-{DateApplication.Day}')";
            SqlCommand command1 = new SqlCommand(comandSql, connection);
            command1.ExecuteNonQuery();
        }
        private static void InsertApplication()
        {
            string commandSql = $"INSERT INTO Applications([PassportNumber]," +
                                                                $"[AmountCredit]," +
                                                                $"[AmountPay]," +
                                                                $"[CreditStatus]," +
                                                                $"[CreditRemain]," +
                                                                $"[TotalIncome]," +
                                                                $"[Overdue]," +
                                                                $"[CreditAim]," +
                                                                $"[CreditTerm])VALUES(" +
                                                                $"'{Customer.PassportNO}'," +
                                                                $"'{AmountCredit.ToString().Replace(',', '.')}'," +
                                                                $"'{AmountPay.ToString().Replace(',','.')}'," +
                                                                $"'{CreditStatus}'," +
                                                                $"'{CreditRemain.ToString().Replace(',', '.')}'," +
                                                                $"'{TotalIncome.ToString().Replace(',', '.')}'," +
                                                                $"'{Overdue}'," +
                                                                $"'{CreditAim}'," +
                                                                $"'{CreditTerm}')";
            SqlCommand command = new SqlCommand(commandSql, connection);
            command.ExecuteNonQuery();
        }
        public static string CreditAimMenu()
        {
            Console.Clear();
            Console.WriteLine($"{new string('-', 20)}Выберите цель кредита{new string('-', 20)}");
            Console.Write($"\t1. Бытовая техника\n\t" +
                          $"2. Ремонт\n\t" +
                          $"3. Телефон\n\t" +
                          $"4. Прочее\n\t" +
                          $"Ваш выбор: ");
            return Console.ReadLine();
        } 
    }
}
