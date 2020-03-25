using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library
{
    public class Validation
    {
        public bool IsValidMenuSelection(string menu, string selection)
        {
            string[] mainMenu = { "c", "o", "x" };
            string[] customerMenu = { "a", "s", "r" };
            string[] orderMenu = { "p", "d", "c", "l", "r" };
            if (menu == "mainMenu")
            {
                foreach (string sl in mainMenu)
                {
                    if (sl == selection)
                    {
                        return true;
                    }
                }
            }
            if (menu == "customerMenu")
            {
                foreach (string sl in customerMenu)
                {
                    if (sl == selection)
                    {
                        return true;
                    }
                }
            }
            if (menu == "orderMenu")
            {
                foreach (string sl in orderMenu)
                {
                    if (sl == selection)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool IsValidString(string s)
        {
            if (s != "" || s != null)
            {
                return true;
            }
            return false;
        }

        public bool IsValidEmail(string e)
        {
            var addr = new System.Net.Mail.MailAddress(e);
            if (addr.Address == e)
            {
                return true;
            }
            return false;
        }
    }
}
