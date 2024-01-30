using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

namespace Task_2
{
    class Composition
    {
        private List<Swift_Transfer> _dic = new List<Swift_Transfer>();
        List<Swift_Transfer> dic
        {
            get { return _dic; }
            set { _dic = value; }
        }
        public int Count()
        {
            return dic.Count();
        }
        public void Print()
        {
            for (int i = 0; i < dic.Count(); i++)
                Console.WriteLine(dic[i].print());
        }
        public void ReadTxt(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    int counter = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        counter++;
                        try
                        {
                            Swift_Transfer sft = new Swift_Transfer();
                            string[] s = line.Split(' ');
                            var type = sft.GetType();
                            int i = 0;
                            for (int j = 0; j < s.Count(); j++)
                            {
                                var properties = TypeDescriptor.GetProperties(type);
                                var property = properties[i];
                                if (i >= 3 && i <= 4)
                                    property.SetValue(sft, Convert.ToDouble(s[j]));
                                else
                                    property.SetValue(sft, s[j]);
                                if (i >= s.Count() - 1)
                                    break;
                                i++;
                            }
                            dic.Add(sft);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(counter.ToString() + ": " + e.Message);
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void DeleteByID(string iden)
        {
            int counter = 0;
            for (int i = 0; i < dic.Count; i++)
            {
                if (dic[i].ID == iden)
                {
                    counter += 1;
                    dic.Remove(dic[i]);
                }
            }
            if (counter > 0)
                Console.WriteLine("Deletion successful!\n");
            else
                Console.WriteLine("There is no element in list with such ID " + iden);
        }
        public void SortByKey(string key, string desc = null)
        {
            int counter = 0;
            for (int i = 0; i < dic.Count; i++)
            {
                foreach (PropertyDescriptor j in TypeDescriptor.GetProperties(dic[i]))
                    if (j.Name == key)
                        counter++;
            }
            if (counter > 0)
            {

                if (desc != null)
                    dic = dic.OrderByDescending(r => r.GetType().GetProperty(key).GetValue(r, null)).ToList();

                else
                    dic = dic.OrderBy(r => r.GetType().GetProperty(key).GetValue(r, null)).ToList();
                Console.WriteLine("Sorting successful!\n");
            }
            else
                Console.WriteLine("Sort key is invalid!");
        }
        public void AddSwiftTransfer()
        {
            try
            {
                Swift_Transfer element = new Swift_Transfer();
                element.input();
                dic.Add(element);
                Console.WriteLine("Addition successful!\n");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void EditByID(string iden)
        {
            int counter = 0;
            for (int i = 0; i < dic.Count; i++)
            {
                if (dic[i].ID == iden)
                {
                    try
                    {
                        counter++;
                        Swift_Transfer element = new Swift_Transfer();
                        element.input();
                        dic.Insert(i, element);
                        dic.Remove(dic[i + 1]);
                        Console.WriteLine("Successfully!\n");
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("No element in list with ID " + iden);
            }
        }
        public void SearchByKey(string key)
        {
            foreach (Swift_Transfer obj in dic)
            {
                var type = obj.GetType();
                var properties = TypeDescriptor.GetProperties(type);
                for (int i = 0, n = properties.Count; i < n; i++)
                {
                    var property = properties[i];
                    string el = Convert.ToString(property.GetValue(obj));
                    if (Regex.IsMatch(el.ToLower(), key.ToLower()))
                    {
                        Console.WriteLine(obj.print());
                        break;
                    }
                }
            }
        }
        public void WriteToTxt(string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    foreach (Swift_Transfer obj in dic)
                    {
                        var type = obj.GetType();

                        var properties = TypeDescriptor.GetProperties(type);
                        for (int i = 0, n = properties.Count; i < n; i++)
                        {
                            var property = properties[i];
                            sw.Write(property.GetValue(obj));
                            sw.Write(" ");
                        }
                        sw.Write("\n");
                    }
                    Console.WriteLine("Operation successful!\n");
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
