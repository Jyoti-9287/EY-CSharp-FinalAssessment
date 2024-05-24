using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Authentication;
using System.ComponentModel.Design;

namespace ShoppingCenter
{
    
class Shop
    {
        //create product list
        public static List<Product> Products = new List<Product>()
        {
         new Product { Id = 1, Name = "Earphone", UnitPrice = 299 },
         new Product { Id = 2, Name = "Airpod", UnitPrice = 999 },
         new Product { Id = 3, Name = "Speaker", UnitPrice = 499 },
         new Product { Id = 4, Name = "Charger", UnitPrice = 599 }
        };
        

        public static List<Product> CartItems = new List<Product>();
        static void Main()
        {
            //Product product = new Product();
            
            MainStart:
            Console.WriteLine("Welcome to Shopping Center!");
            Console.WriteLine("Select a option -\n"+
                "1. Login\n"+
                "2. Register\n");
            string choice = Console.ReadLine();
            int choiceNumber;

            Console.WriteLine();

            if (int.TryParse(choice, out int _choiceNumber))
            {
                choiceNumber = _choiceNumber;
            }
            else
            {
                choiceNumber = 0;
            }

            switch (choiceNumber)
            {
                case 1:                 
                    User user = new User();
                    bool isLogin = user.Authentication();
                    if (isLogin)
                    {
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("Login Success.\n");
                        Console.ResetColor();

                        //add to cart process
                        Product.AddToCart();
                        //end of cart 
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Login failed. Credential are case sensitive. Try again");
                        Console.ResetColor();
                        goto case 1;
                    }
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nYou have been registered successfully. You can login with below credential :\nUsername: Admin\nPassword: 1234\n");
                    Console.ResetColor();
                    goto case 1;                   
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have entered invalid choice. Try again\n");
                    Console.ResetColor();
                    goto MainStart;
                    break;
            }


        }
    }

    //User Login class
    class User
    {
        public string username;
        public string password;

        public User() { }
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public bool Authentication()
        {
            Console.Write("\nLogin here - \nEnter Username:  ");
            string username = Console.ReadLine();
            Console.Write("Enter Password:  ");
            string password = Console.ReadLine();
            if (username == "Admin" && password == "1234") {  return true; }
            return false;

        }
    }

    class Product
    {
        public int Id;
        public string Name;
        public double UnitPrice;
        public int Quantity;
        public double TotalPrice;
        public Product() { }
        
        public Product(int id, string name, double unitPrice)
        {
            this.Id = id;
            this.Name = name;
            this.UnitPrice = unitPrice;
        }

        public Product(int id, string name, double unitPrice,int quantity, double totalPrice)
        {
            this.Id = id;
            this.Name = name;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
            this.TotalPrice = totalPrice;
        }

        public static void ShowProducts(List<Product> products)
        {
            Console.BackgroundColor=ConsoleColor.Gray;
            Console.ForegroundColor=ConsoleColor.Black;
            Console.WriteLine("Id  Name     Unit Price ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Id}  {product.Name}     $ {product.UnitPrice}");
            }
            Console.ResetColor();
        }
        public static void ShowCartProducts(List<Product> products)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Id  Name     Unit Price Quantity  Total Price");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Id}  {product.Name}   $ {product.UnitPrice}       {product.Quantity}       $ {product.TotalPrice}");
            }
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nTotal Price is : $ " +products.Sum(p=>p.TotalPrice));

            Console.ResetColor();
        }

        public static void AddToCart()
        {

            Product.ShowProducts(Shop.Products);
            SelectProduct:
            Console.Write("\nSelect Product Id to add item to cart:  ");
            string product = Console.ReadLine();
            int productID;
            if (int.TryParse(product, out int _productID))
            {
                productID = _productID;
                if (productID > 0 && productID <= 4)
                {
                QuantityDetail:
                    Console.Write("Enter Quantity: ");
                    string quantity = Console.ReadLine();
                    int quantityNumber;
                    if (int.TryParse(quantity, out int _quantityNumber))
                    {
                        quantityNumber = _quantityNumber;
                        Product SelectedProduct = Shop.Products.FirstOrDefault(p => p.Id == productID);
                        SelectedProduct.Quantity=quantityNumber;
                        SelectedProduct.TotalPrice=quantityNumber*SelectedProduct.UnitPrice;
                        Shop.CartItems.Add(SelectedProduct);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Product : {SelectedProduct.Name} Quantity : {quantityNumber} has been added to cart.\n");
                        Console.ResetColor();

                        //checkout start
                        Product.CheckoutProcess();
                        //end of checkout process
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have entered something wrong. Please try again:");
                        Console.ResetColor();
                        goto QuantityDetail;
                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please select correct Product ID");
                    Console.ResetColor();
                    goto SelectProduct;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please select correct Product ID");
                Console.ResetColor();
                goto SelectProduct;
            }
        }
        public static void CheckoutProcess()
        {
        CheckoutStart:
            Console.WriteLine("Please select an option:\n1.Continue shopping\n2.Checkout");
            string option = Console.ReadLine();
            int OptionID;
            if (int.TryParse(option, out int _optionID))
            {
                OptionID = _optionID;
                switch (OptionID)
                {
                    case 1:
                        Console.WriteLine("\nContinue to Shopping:");
                        //go to show product
                        //Product.ShowProducts(Shop.Products);
                        Product.AddToCart();
                        //goto CheckoutStart;
                        break;
                    case 2:
                        Console.WriteLine("\nCheckout:");
                        ShowCartProducts(Shop.CartItems);

                        Console.WriteLine("\nThank you for shopping with us!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have entered something wrong. Please try again:");
                        Console.ResetColor ();
                        goto CheckoutStart;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have entered something wrong. Please try again:");
                Console.ResetColor();
                goto CheckoutStart;
            }

        }
    }   

}