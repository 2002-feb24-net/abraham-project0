using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project0
{
    public class OrderAction
    {
        public void YourNextOrderAction(string selection)
        {
            if (selection == "p")
            {
                PlaceAnOrder();
            }

            if (selection == "d")
            {

            }

            if (selection == "c")
            {

            }

            if (selection == "l")
            {

            }
        }

        public void PlaceAnOrder()
        {
            using (var context = new Project0DbContext())
            {
                int OdrCstmId;
                int OdrStrId;
                int Ool;

                int OrdListId;
                int OrdListProdId;

                ProductOrder pOrder = new ProductOrder();
                OrderList orderList = new OrderList();
                var product = from p in context.Product select p;
                List<Product> pList = new List<Product>();
                string[] fullName = new string[2];
                bool flag = false;

                Console.WriteLine("Please enter the customers full name: ");
                fullName = Console.ReadLine().Split(" ");
                Console.WriteLine("");

                var customer = context.Customer
                                        .Where(s => s.CstmFirstName == fullName[0])
                                        .Where(s => s.CstmLastName == fullName[1]);

                OdrCstmId = customer.First().CstmId;

                var location = from l in context.StoreLocation select l;

                Console.WriteLine("Please select the ID of the shipping destination: ");
                Console.WriteLine("");
                foreach(var l in location)
                {
                    Console.WriteLine("Location ID: " + l.LocId + ", Street: " + l.LocStreet + ", City: " + l.LocCity + ", Country: " + l.LocCountry);
                }
                OdrStrId = Int32.Parse(Console.ReadLine());
                Console.WriteLine("");
                Ool = 0;
                
                while (!flag)
                {
                    Console.WriteLine("Please enter the ID of the product you wish to puchase: ");
                    Console.WriteLine("");
                    foreach (var p in product)
                    {
                        Console.WriteLine("Product ID: " + p.ProdId + ", Product name: " + p.ProdName + ", Product Description: " + p.ProdDescription + ", Product Price: " + p.ProdPrice);
                    }
                    Console.WriteLine("");

                    var newProduct = Int32.Parse(Console.ReadLine());
                    product = context.Product.Where(x => x.ProdId == newProduct);
                    pList.Add(product.FirstOrDefault());

                    Console.WriteLine("Would you like to add another prodcut? (y/n)");
                    string cont = Console.ReadLine();
                    if (cont == "n")
                    {
                        var ol = context.OrderList.Where(a => a.LstOrderId == 1);
                        if (ol.Any())
                        {
                            int maxId = context.OrderList.Max(r => r.LstOrderId);
                            OrdListId = maxId + 1;
                        }
                        else
                        {
                            OrdListId = 1;
                        }                        

                        foreach (var p in pList)
                        {
                            var productList = new OrderList
                            {
                                LstOrderId = OrdListId,
                                LstProdId = p.ProdId
                            };
                        }
                        Ool = OrdListId;

                        var prodcutOrder = new ProductOrder
                        {
                            OrderCstmId = OdrCstmId,
                            OrderStrId = OdrStrId,
                            OrderOrdList = Ool,

                        };
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    Console.WriteLine("");
                }

                
            }
        }
    }
}
