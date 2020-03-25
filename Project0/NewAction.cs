using System;
using Project0;
using System.Collections.Generic;
using System.Text;
using Project0.Library;

namespace Project0
{
    public class NewAction
    {

        public void StartingTheApp()
        {
            bool flag = false;
            Validation valid = new Validation();
            while (!flag)
            {
                Console.WriteLine("Hello, how many we assist you today?\n" +
                                "Would you like to:\n" +
                                "(c) Work with a customer.\n" +
                                "(o) Work with an Order.\n" +
                                "(x) Exit program");

                string selection = Console.ReadLine();
                Console.WriteLine("");
                flag = valid.IsValidMenuSelection("mainMenu", selection);
                if (!flag)
                {
                    Console.WriteLine("You made an invald secletion, please select 'c', 'o'.");
                }
                else if (selection == "x")
                {
                    Environment.Exit(0);
                }
                else
                {
                    YourNextAction(selection);
                    flag = false;
                }
            }
        }
        public void YourNextAction(string selection)
        {
            if (selection == "c")
            {
                CustomerAction();
            }
            else
            {
                OrderAction();
            }
        }

        public void CustomerAction()
        {
            bool flag = false;
            Validation valid = new Validation();

            while (!flag)
            {
                Console.WriteLine("You selected Customer, what would you like to do?\n" +
                                    "(a) Add a new Customer.\n" +
                                    "(s) Search a new Customer.\n" +
                                    "(r) Return to main menu");

                string selection = Console.ReadLine();
                Console.WriteLine("");
                flag = valid.IsValidMenuSelection("customerMenu", selection);
                if (!flag)
                {
                    Console.WriteLine("You made an invald secletion, please select 'a', 's'.");
                }
                else if (selection == "r")
                {
                    StartingTheApp();
                }
                else
                {
                    CustomerAction nc = new CustomerAction();
                    nc.YourNextCustomerAction(selection);
                }
            }
        }

        public void OrderAction()
        {
            bool flag = false;
            Validation valid = new Validation();
            while (!flag)
            {
                Console.WriteLine("You selected Order, what would you like to do?\n" +
                                "(p) Place a new Order.\n" +
                                "(d) Display an Order.\n" +
                                "(c) Display Customer Order history.\n" +
                                "(l) Display store Order history.\n" +
                                "(r) Return to main menu.");

                string selection = Console.ReadLine();
                Console.WriteLine("");
                flag = valid.IsValidMenuSelection("orderMenu", selection);
                if (!flag)
                {
                    Console.WriteLine("You made an invald secletion, please select 'p', 'd', 'c', 'l'.");
                }
                else if (selection == "r")
                {
                    StartingTheApp();
                }
                else
                {
                    OrderAction oa = new OrderAction();
                    oa.YourNextOrderAction(selection);
                }
            }
        }
    }
}
