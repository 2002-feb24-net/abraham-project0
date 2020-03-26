using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Project0.App;
using Project0.Library;

namespace Project0
{
    public class OrderAction
    {
        Validation valid = new Validation();
        public void YourNextOrderAction(string selection)
        {
            if (selection == "p")
            {
                PlaceAnOrder();
            }

            if (selection == "d")
            {
                DisplayAnOrder();
            }

            if (selection == "c")
            {
                CustomerOrderHistory();
            }

            if (selection == "l")
            {
                LocationOrderHistory();
            }
        }

        public void PlaceAnOrder()
        {
            using (var context = new Project0DbContext())
            {

            }
        }

        public void DisplayAnOrder()
        {
            using (var context = new Project0DbContext())
            {
                int customerID = 0;
                bool v = false;
                while (!v)
                {
                    string[] fullName = new string[2];
                    Console.WriteLine("Please enter the first and last name to search: ");
                    fullName = Console.ReadLine().Split(" ");
                    Console.WriteLine("");
                    v = valid.IsValidString(fullName[0] + " " + fullName[1]);
                    if (!v)
                    {
                        Console.WriteLine("You must enter a name.");
                        Console.WriteLine("");
                    }
                    else
                    {
                        var cstmr = context.Customer
                                    .Where(s => s.CstmFirstName == fullName[0])
                                    .Where(s => s.CstmLastName == fullName[1]);
                        if (cstmr.Any())
                        {
                            customerID = cstmr.First().CstmId;
                            v = true;
                        }
                        else
                        {
                            Console.WriteLine(fullName[0] + " " + fullName[1] + " is not a customer.");
                            Console.WriteLine("");
                        }
                    }
                }
                
                var ord = context.ProductOrder
                            .Where(s => s.OrderCstmId == customerID);

                if (ord.Any())
                {
                    foreach(var o in ord)
                    {
                        Console.WriteLine("Order ID: " + o.OrderId + ", Order Date: " + o.OrderOrdDate);
                    }
                    Console.WriteLine("Select the ID of the order you wish to view: ");
                    int oId = Int32.Parse(Console.ReadLine());

                    var oLId = context.OrderList
                                .Where(o => o.LstOrderId == oId);
                }
                else
                {
                    Console.WriteLine("Customer does not currently have any orders.");
                    Console.WriteLine("");
                }

            }     
        }

        public void CustomerOrderHistory()
        {
            using (var context = new Project0DbContext())
            {

            }
        }

        public void LocationOrderHistory()
        {
            using (var context = new Project0DbContext())
            {

            }
        }
    }
}
