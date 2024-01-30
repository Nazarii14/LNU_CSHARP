using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        static void FirstOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter the path of the file: ");
            string path = Console.ReadLine();
            sf.ReadTxt(path);
            Console.WriteLine("----------------------------------------");
        }
        static void SecondOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter key to sort for: ");
            string word = Console.ReadLine();
            Console.Write("In descending(desc) or ascending(asc) order: ");
            string choice = Console.ReadLine();
            if (choice == "desc")
                sf.SortByKey(word, choice);
            else
                sf.SortByKey(word);
            Console.WriteLine("----------------------------------------");
        }
        static void ThirdOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter key to search for: ");
            string word = Console.ReadLine();
            sf.SearchByKey(word);
            Console.WriteLine("----------------------------------------");
        }
        static void FourthOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Swift transfer: ");
            sf.AddSwiftTransfer();
            Console.WriteLine("----------------------------------------");
        }
        static void FifthOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter ID of the element to delete: ");
            try
            {
                string elem = Validation.ValidateNumber(Console.ReadLine());
                sf.DeleteByID(elem);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("----------------------------------------");
        }
        static void SixthOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            try
            {
                Console.Write("Enter ID of the element to edit: ");
                string word = Validation.ValidateNumber(Console.ReadLine());
                sf.EditByID(word);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("----------------------------------------");
        }
        static void SeventhOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter the path of the file: ");
            string path = Console.ReadLine();
            sf.WriteToTxt(path);
            Console.WriteLine("----------------------------------------");
        }
        static void EightOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Collection: ");
            sf.Print();
            Console.WriteLine("----------------------------------------");
        }
        static void NinethOption(Composition sf)
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Goodbye!");
            Console.WriteLine("----------------------------------------");
        }
        static void menu()
        {
            Console.WriteLine(@"    1. Read from file.
    2. Sort elements.
    3. Search elements.
    4. Add Swift_Transfer to collection.
    5. Delete Swift_Transfer from collection.
    6. Edit Swift_Transfer from collection.
    7. Write collection elements to file.
    8. Print collection.
    9. Exit");
        }
        static void MainMenu()
        {
            Composition sf = new Composition();
            while (true)
            {
                menu();
                Console.Write("\r\nOption: ");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 1)
                    FirstOption(sf);
                else if (option == 2)
                    SecondOption(sf);
                else if (option == 3)
                    ThirdOption(sf);
                else if (option == 4)
                    FourthOption(sf);
                else if (option == 5)
                    FifthOption(sf);
                else if (option == 6)
                    SixthOption(sf);
                else if (option == 7)
                    SeventhOption(sf);
                else if (option == 8)
                    EightOption(sf);
                else if (option == 9)
                {
                    NinethOption(sf);
                    break;
                }

                else
                {
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Invalid option! Try again!");
                    Console.WriteLine("----------------------------------------");
                    continue;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Choose what you want to do:");
            MainMenu();
        }
    }
}
