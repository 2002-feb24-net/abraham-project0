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
                int customerID = 0;
                bool v = false;
                while (!v)
                {
                    string[] fullName = new string[2];
                    Console.WriteLine("Please enter the first and last name to start an Order: ");
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

                int storeId = 0;
                var store = from s in context.StoreLocation
                            select s;
                v = false;
                while (!v)
                {
                    Console.WriteLine("Location ID---Street-------------------City----------------Country");
                    foreach (var l in store)
                    {
                        string i = "--[ " + l.LocId.ToString() + " ]----------------------------------------------------";
                        string s = l.LocStreet + "-------------------------------------------------------------";
                        string c = l.LocCity + "------------------------------------------------------";
                        string u = l.LocCountry;
                        Console.WriteLine(i.Substring(0, 14) + s.Substring(0, 25) + c.Substring(0, 20) + u);
                    }
                    Console.WriteLine("");


                    Console.WriteLine("Please select a store to have order delivered: ");
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out int num))
                    {
                        Console.WriteLine("Please enter numeric values only.");
                        v = false;
                    }
                    else
                    {
                        storeId = num;
                        v = true;
                    }
                }
                Console.WriteLine("");
                v = false;
                while(!v)
                {
                    Console.WriteLine("Would you like this to be your default store? (y/n)");
                    string defaultLoc = Console.ReadLine();
                    Console.WriteLine("");
                    v = valid.IsValidYesNo(defaultLoc);
                    if(!v)
                    {
                        Console.WriteLine("You must select 'y' or 'n'. Case sensitive.");
                        Console.WriteLine("");
                    }
                    else
                    {
                        if (defaultLoc == "y")
                        {
                            var cstm = from c in context.Customer
                                       where c.CstmId == customerID
                                       select c;

                            foreach(var c in cstm)
                            {
                                c.CstmDefaultStoreLoc = storeId;
                            }
                            context.SaveChanges();
                        }
                    }
                }

                int maxId = 0;
                maxId = context.ProductOrder.Max(p => p.OrderId);
                int newOrderId = maxId + 1;
                ProductOrder newProdOrder = new ProductOrder
                {
                    OrderCstmId = customerID,
                    OrderStrId = storeId
                };
                context.ProductOrder.Add(newProdOrder);
                context.SaveChanges();

                var prod = from p in context.Product
                           join l in context.StoreInventory on p.ProdId equals l.InvProdId
                           where l.InvStoreLoc == storeId
                           select new { Product = p, Inventory = l };

                Console.WriteLine("Product ID-----Name------------Description-------------------Price----Inventory");
                foreach(var t in prod)
                {
                    string i = "--[ " + t.Product.ProdId.ToString() + " ]--------------------------------------------------------------------------------------";
                    string n = t.Product.ProdName + "--------------------------------------------------------------------------------------";
                    string d = t.Product.ProdDescription + "--------------------------------------------------------------------------------------";
                    string p = t.Product.ProdPrice.ToString("#.##") + "--------------------------------------------------------------------------------------";
                    string s = t.Inventory.InvProdInventory.ToString();
                    Console.WriteLine(i.Substring(0, 15) + n.Substring(0, 16) + d.Substring(0,30) + p.Substring(0, 9) + s);
                }
                Console.WriteLine("");

                v = false;
                List<int> itemList = new List<int>();
                while (!v)
                {
                    Console.WriteLine("Select an Item to add to your order: ");
                    int item = Int32.Parse(Console.ReadLine());
                    itemList.Add(item);
                    Console.WriteLine("");
                    Console.WriteLine("Item [ " + item + " ] added to your cart.");
                    Console.WriteLine("");
                    Console.WriteLine("Would you like to add another item? (y/n)");
                    string yn = Console.ReadLine();
                    Console.WriteLine("");
                    v = valid.IsValidYesNo(yn);
                    if (!v)
                    {
                        Console.WriteLine("Please enter [ y ] or [ n ]. Case Sensitive.");
                    }
                    else
                    {
                        if (yn == "y")
                        {
                            v = false;
                        }
                        else
                        {
                            v = true;
                        }
                    }
                }
                
                foreach(var i in itemList)
                {
                    OrderList ol = new OrderList { LstOrderId = newOrderId, LstProdId = i };
                    context.OrderList.Add(ol);
                    context.SaveChanges();

                    var inv = from sI in context.StoreInventory
                              where sI.InvStoreLoc == storeId & sI.InvProdId == i
                              select sI;

                    foreach(var d in inv)
                    {
                        --d.InvProdInventory;
                    }
                }
                context.SaveChanges();
                Console.WriteLine("Your Order has successfully been placed! Thank you!");
            }
            Console.WriteLine("");

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
                    Console.WriteLine("Order ID------Order Date");
                    foreach(var o in ord)
                    {
                        string i = "--[ " + o.OrderId.ToString() + " ]------------------";
                        string d = o.OrderOrdDate.ToString();
                        Console.WriteLine(i.Substring(0, 14) + d);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Select the ID of the order you wish to view: ");
                        
                    int oId = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("");

                    var selectedOrderWLoc = from pOrder in context.ProductOrder
                                            join sLoc in context.StoreLocation on pOrder.OrderStrId equals sLoc.LocId
                                            where pOrder.OrderId == oId
                                            select new { Product = pOrder, StoreLocation = sLoc };

                    var po = selectedOrderWLoc.Select(p => p.Product);
                    var sl = selectedOrderWLoc.Select(l => l.StoreLocation);

                    var productList = context.OrderList.Where(x => x.LstOrderId == oId).Select(s => s.LstProd);
                    var id = po.Select(p => p.OrderId).First();
                    var cst = po.Select(p => p.OrderCstm).Select(c => c.CstmFirstName + " " + c.CstmLastName).First();
                    var loc = sl.Select(s => s.LocCity).First();
                    var dt = po.Select(p => p.OrderOrdDate).First();

                    Console.WriteLine("Order ID:-------" + id
                                    + "\nCustomer:-------" + cst
                                    + "\nStore Location:-" + loc
                                    + "\nOrder Date:-----" + dt
                                    +"\n");

                    Console.WriteLine("ID---Name-----------Description----------Price");
                    foreach(var l in productList)
                    {
                        string i = l.ProdId + "----------------------------------------------------";
                        string n = l.ProdName + "----------------------------------------------------";
                        string d = l.ProdDescription + "----------------------------------------------------";
                        string p = "---" + l.ProdPrice.ToString("#.##");
                        Console.WriteLine(i.Substring(0, 5) + n.Substring(0, 15) + d.Substring(0, 20) + p.Substring(p.Length - 7));
                    }
                    Console.WriteLine("");
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

                var price = from pO in context.ProductOrder
                            join oL in context.OrderList on pO.OrderId equals oL.LstOrderId
                            join p in context.Product on oL.LstProdId equals p.ProdId
                            where pO.OrderCstmId == customerID
                            select new { ProductOrder = pO, Poduct = p };

                if (ord.Any())
                {
                    
                    Console.WriteLine("Order ID------Total----Order Date");
                    foreach (var o in price)
                    {
                           
                        string i = "--[ " + o.ProductOrder.OrderId.ToString() + " ]------------------";
                        string s = o.Poduct.ProdPrice.ToString("#.##") + "-----------------------"; 
                        string d = o.ProductOrder.OrderOrdDate.ToString();
                        Console.WriteLine(i.Substring(0, 14) + s.Substring(0, 9) + d);
                    }
                    Console.WriteLine("");
                }
            }
        }

        public void LocationOrderHistory()
        {
            using (var context = new Project0DbContext())
            {
                int storeId = 0;
                    var store = from s in context.StoreLocation
                                select s;

                Console.WriteLine("Location ID---Street-------------------City----------------Country");
                foreach(var l in store)
                {
                    string i = "--[ " + l.LocId.ToString() + " ]----------------------------------------------------";
                    string s = l.LocStreet + "-------------------------------------------------------------";
                    string c = l.LocCity + "------------------------------------------------------";
                    string u = l.LocCountry;
                    Console.WriteLine(i.Substring(0, 14) + s.Substring(0, 25) + c.Substring(0, 20) + u);
                }
                Console.WriteLine("");

                Console.WriteLine("Please select a store to view orders: ");
                storeId = Int32.Parse(Console.ReadLine());
                Console.WriteLine("");

                var ord = context.ProductOrder
                            .Where(s => s.OrderStrId == storeId);

                var location = from pO in context.ProductOrder
                            join sL in context.StoreLocation on pO.OrderStrId equals sL.LocId
                            select new { ProductOrder = pO, StoreLocation = sL };

                if (ord.Any())
                {
                    Console.WriteLine("Order ID------Order Date");
                    foreach (var l in location)
                    {
                        string i = "--[ " + l.ProductOrder.OrderId.ToString() + " ]------------------";
                        string d = l.ProductOrder.OrderOrdDate.ToString();
                        Console.WriteLine(i.Substring(0, 14)+ d);
                    }
                    Console.WriteLine("");
                }
            }
        }
    }
}
