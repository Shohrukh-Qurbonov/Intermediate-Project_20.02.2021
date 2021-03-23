using ConsoleApplication.Models;
using System;
using AutoCreditSystem;

namespace ConsoleApplication
{
    class Program
    {
        static bool isWorking = true;
        static void Main(string[] args)
        {
            while (isWorking)
            {
                switch (ShowMainMenu())
                {
                    case "1":
                        if (Admin.CheckAdmin())
                        {
                            switch (ShowAdminMenu())
                            {
                                case "1":
                                    switch (AdminPart())
                                    {
                                        case "1": Admin.AddAdmin(); break;
                                        case "2": 
                                            if (Admin.CheckAdmin())
                                                Admin.DeleteAdmin();
                                            else
                                            {
                                                Color.GetRedColor();
                                                Console.WriteLine("Такой Id не существуеть в базе данных!");
                                                Color.GetResetColor();
                                            }
                                            break;
                                        case "3": break;
                                        default: ErrorMessage(); break;
                                    } break;
                                case "2":
                                    switch (ClientPart())
                                    {
                                        case "1": Customer.GetCustomer(); break;
                                        case "2": Customer.DeleteCustomer(); break;
                                        case "3": Application.GetAllAplication(); break;
                                        case "4": Application.GetApplicationByPassNO(); break;
                                        case "5": Application.DelApplicationByPassNO(); break;
                                        case "6": RepaymentSchedule.RepaymentSchAll(); break;
                                        case "7": RepaymentSchedule.RepaymentSchByPassNO(); break;
                                        case "8": RepaymentSchedule.RepaymentSchDelByPassNO(); break;
                                        case "9": break;
                                        default: ErrorMessage(); break;
                                    } break;
                                case "3": break;
                                case "4": return;
                                default: ErrorMessage(); break;
                            }
                        }
                        else NotMatchedLogOrPass(); 
                        break;
                    case "2": 
                        switch (ShowMainMenuCustomer())
                        {
                            case "1":
                                if (Customer.CheckClient())
                                {
                                    switch (ShowMenuCustomer())
                                    {
                                        case "1": Application.CreateApplication(); break;
                                        case "2": Application.GetApplicationByPassNO(); break;
                                        case "3": Customer.GetCustomerByPassNO(); break;
                                        case "4": RepaymentSchedule.RepaymentSchByPassNO(); break;
                                        case "5": break;
                                    }
                                }
                                else NotMatchedLogOrPass();
                                break;
                            case "2": Customer.Registration(); break;
                            case "3": break;
                            case "4": return;
                            default: ErrorMessage(); break;
                        }
                        break;
                    case "3": isWorking = false; break;
                    default: ErrorMessage(); break;
                }
            }
        }
        static string ShowMainMenu()
        {
            Console.Clear(); Color.GetGreenColor();
            Console.WriteLine($"{new string('_', 46)}ДОБРО ПОЖАЛОВАТЬ В АВТОКРЕДИТНУЮ СИСТЕМУ <<АЛИФ БАНКА>>{new string('_', 46)}\n"); Color.GetResetColor();
            Console.WriteLine($"{new string('-', 20)}Главная меню{new string('-', 20)}");
            Console.Write($"\t1. Админ\n\t" +
                          $"2. Клиент\n\t" +
                          $"3. Выход\n\t" +
                          $"Ваш выбор: ");
            return Console.ReadLine();
        }
        static string ShowAdminMenu()
        {
            Console.Clear(); Color.GetBlueColor();
            Console.WriteLine($"{new string('-', 20)}Меню администратора{new string('-', 20)}");
            Color.GetResetColor();
            Console.WriteLine($"Добро пожаловать Admin (^_^)");
            Console.Write($"\t1. Административная часть\n\t" +
                          $"2. Клиентская часть\n\t" +
                          $"3. Вернуться в главное меню\n\t" +
                          $"4. Выход :(\n\t" +
                          $"Выберите команду: ");
            return Console.ReadLine();
        }
        static string AdminPart()
        {
            Console.Clear(); Color.GetBlueColor();
            Console.WriteLine($"{new string('-', 20)}Административная часть{new string('-', 20)}");
            Color.GetResetColor();
            Console.Write($"\t1. Добавить нового администратора\n\t" +
                          $"2. Удалить существующего администратора\n\t" +
                          $"3. Вернуться в главное меню\n\t" +
                          $"Ваш выбор: ");
            return Console.ReadLine();
        }
        static string ClientPart()
        {
            Console.Clear(); Color.GetBlueColor();
            Console.WriteLine($"{new string('-', 20)}Клиентская часть{new string('-', 20)}");
            Color.GetResetColor();
            Console.Write($"\t1. Посмотреть список клиентов\n\t" +
                          $"2. Удалить клиента по номеру паспорта \n\t" +
                          $"3. Посмотреть список заявок\n\t" +
                          $"4. Посмотреть заявку по номеру паспорта\n\t" +
                          $"5. Удалить заявку по номеру паспорта\n\t" +
                          $"6. Посмотреть список всех графиков погашения\n\t" +
                          $"7. Посмотреть график погашения по номеру паспорта\n\t" +
                          $"8. Удалить график погашения по номеру паспорта\n\t" +
                          $"9. Вернуться в главное меню\n\t" +
                          $"Ваш выбор: ");
            return Console.ReadLine();
        }
        static string ShowMainMenuCustomer()
        {
            Console.Clear(); Color.GetBlueColor();
            Console.WriteLine($"{new string('-', 20)}Главное меню клиента{new string('-', 20)}"); Color.GetResetColor();
            Console.Write($"\t1. Вход\n\t" +
                          $"2. Регистрация\n\t" +
                          $"3. Вернуться в главное меню\n\t" +
                          $"4. Выход\n\t" +
                          $"Ваш выбор: ");
            return Console.ReadLine();
        }
        static string ShowMenuCustomer()
        {
            Console.Clear(); Color.GetBlueColor();
            Console.WriteLine($"{new string('-', 20)}Меню команд клиента{new string('-', 20)}"); Color.GetResetColor();
            Console.Write($"\t1. Создать новую заявку\n\t" +
                          $"2. Посмотреть заявку по номеру паспорта\n\t" +
                          $"3. Посмотреть данные клиента по номеру паспорта\n\t" +
                          $"4. Посмотреть график погашения по номеру паспорта\n\t" +
                          $"5. Вернуться в главное меню\n\t" +
                          $"Ваш выбор: ");
            return Console.ReadLine();
        }
        static void ErrorMessage()
        {
            Color.GetRedColor();
            Console.WriteLine("Неправильная комманда!");
            Color.GetResetColor();
            Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
            Console.ReadKey();
        }
        static void NotMatchedLogOrPass()
        {
            Color.GetRedColor(); Console.WriteLine("Логин или пароль не совпадаеть!");
            Color.GetResetColor(); Console.WriteLine("Нажмите любую клавишу, чтобы перейти в главное меню ...");
            Console.ReadKey();
        }
    }
}
