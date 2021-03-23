using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApplication.Models
{
    public static class Calculation
    {
        public static SqlConnection connection = new SqlConnection(Connect.path);
        public static bool CalculationsForApplications()
        {
            int CountCreditHistory = 0;
            int CountOverdue = 0;

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string commandSql = $"SELECT * FROM Applications WHERE PassportNumber = '{Customer.PassportNO}'";
            using (SqlCommand command = new SqlCommand(commandSql, connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue("CreditStatus").ToString() == nameof(CreditStates.Open))
                        {
                            Console.WriteLine("У вас есть не закрытый кредит!");
                            Console.ReadKey();
                            return false;
                        }
                        else
                            CountCreditHistory++;
                        CountOverdue +=  int.Parse(reader.GetValue("Overdue").ToString());
                    }
                }
            }
            //if (connection.State == ConnectionState.Closed)
            //    connection.Open();
            //string comandSql = $"SELECT * FROM Applications WHERE PassportNumber = '{Customer.PassportNO}'";
            //using (SqlCommand command1 = new SqlCommand(comandSql, connection))
            //{
            //    using (SqlDataReader reader = command1.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            CountOverdue += int.Parse(reader.GetValue("Overdue").ToString());
            //        }
            //    }
            //}
            int Score = 1;
            if (CountCreditHistory == 0)
                Score--;
            else if (CountCreditHistory > 0 || CountCreditHistory < 3)
                Score++;
            else if (CountCreditHistory >= 3)
                Score += 2;
            if (CountOverdue == 4)
                Score--;
            else if (CountOverdue >= 5 && CountOverdue <= 7)
                Score -= 2;
            else if (CountOverdue > 7)
                Score -= 3;
            if (Application.CreditAim == "Бытовая техника")
                Score += 2;
            else if (Application.CreditAim == "Ремонт")
                Score++;
            else if (Application.CreditAim == "Прочее")
                Score--;
            if (Customer.Gender == "Женской")
                Score += 2;
            else
                Score++;
            if (Customer.MaritialStatus == "Холост")
                Score++;
            else if (Customer.MaritialStatus == "Семеянин")
                Score += 2;
            else if (Customer.MaritialStatus == "Вразводе")
                Score++;
            else if (Customer.MaritialStatus == "Вдовец/вдова")
                Score += 2;
            int Age = DateTime.Now.Year - Customer.BirthDate.Year;
            if (Age <= 25)
                Score += 0;
            else if (Age > 25 && Age <= 35)
                Score++;
            else if (Age >= 36 && Age <= 62)
                Score += 2;
            else if (Age >= 63)
                Score++;
            if (Customer.Nationality == "Таджикистан")
                Score++;
            decimal LoanAmnTotalIncome = ((Application.AmountCredit * 100) / Application.TotalIncome);
            if (LoanAmnTotalIncome <= 80)
                Score += 4;
            else if (LoanAmnTotalIncome > 80 && LoanAmnTotalIncome <= 150)
                Score += 3;
            else if (LoanAmnTotalIncome > 150 && LoanAmnTotalIncome <= 250)
                Score += 2;
            else if (LoanAmnTotalIncome > 250)
                Score += 1;
            if (Score <= 11)
            {
                Color.GetRedColor();
                Console.WriteLine("\nKоличество баллов:" + Score);
                Color.GetResetColor();
                return false;
            }
            else
            {
                Color.GetGreenColor();
                Console.WriteLine("\nKоличество баллов:" + Score);
                Color.GetResetColor();
                return true;
            }
        }
    }
}
