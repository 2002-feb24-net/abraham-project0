using Project0.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Project0
{
    public class CustomerAction
    {

        Validation valid = new Validation();
        public string ctmrFirstName;
        public string ctmrLastName;
        public string ctmrEmail;
        public void YourNextCustomerAction(string s)
        {
            using (var context = new Project0DbContext())
            {
                if (s == "a")
                {
                    bool v = false;
                    while (!v)
                    {
                        Console.WriteLine("Please enter the customer first name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("");
                        v = valid.IsValidString(name);
                        if (!v)
                        {
                            Console.WriteLine("You must enter the first name.");
                        }
                        else
                        {
                            ctmrFirstName = name;
                        }
                    }
                    v = false;
                    while (!v)
                    {
                        Console.WriteLine("Please enter the customer last name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("");
                        v = valid.IsValidString(name);
                        if (!v)
                        {
                            Console.WriteLine("You must enter the last name.");
                        }
                        else
                        {
                            ctmrLastName = name;
                        }
                    }
                    v = false;
                    while (!v)
                    {
                        Console.WriteLine("Please enter the customer email: ");
                        string email = Console.ReadLine();
                        Console.WriteLine("");
                        v = valid.IsValidEmail(email);
                        if (!v)
                        {
                            Console.WriteLine("You must enter a email with '@' and ends with .net .edu .com etc.");
                        }
                        else
                        {
                            ctmrEmail = email;
                        }
                    }
                    var Customer = new Customer
                    {
                        CstmFirstName = ctmrFirstName,
                        CstmLastName = ctmrLastName,
                        CstmEmail = ctmrEmail
                    };
                    context.Add(Customer);
                    context.SaveChanges();

                    Console.WriteLine(ctmrFirstName + " " + ctmrLastName + " was added to our Database.");
                }
                else
                {
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
                        }
                        else
                        {
                            var cstmr = context.Customer
                                        .Where(s => s.CstmFirstName == fullName[0])
                                        .Where(s => s.CstmLastName == fullName[1]);
                            if (cstmr.Any())
                            {
                                foreach (var c in cstmr)
                                {
                                    Console.WriteLine("Customer ID: " + c.CstmId);
                                    Console.WriteLine("Customer full name: " + c.CstmFirstName + " " + c.CstmLastName);
                                    Console.WriteLine("Customer email: " + c.CstmEmail);
                                    Console.WriteLine("Customer default store: " + c.CstmDefaultStoreLoc);
                                }
                                Console.WriteLine("");
                            }
                            else
                            {
                                Console.WriteLine(fullName[0] + " " + fullName[1] + " is not a customer.");
                            }
                        }
                    }
                }
            }
        }
    }
}
