using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

class Patient
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int RegNum { get; set; }
}
class Prescription
{
    public uint RegNum { get; set; }
    public DateTime Date { get; set; }
    public string MedicationName { get; set; }
    public uint Dosage { get; set; }
    public uint FrequencyPerDay { get; set; }

    public uint getAmountPerDay()
    {
        return Dosage * FrequencyPerDay;
    }
}
//class Program
//{
//    static void Main(string[] args)
//    {
//        Console.OutputEncoding = Encoding.UTF8;

//        Patient[] patients = new Patient[]
//        {
//        new Patient { FirstName = "Іван", LastName = "Франко", RegistrationNumber = 1 },
//        new Patient { FirstName = "Тарас", LastName = "Шевченко", RegistrationNumber = 2 },
//        new Patient { FirstName = "Леся", LastName = "Українка", RegistrationNumber = 3 }
//        };

//        Prescription[] prescriptions = new Prescription[]
//        {
//        new Prescription { RegistrationNumber = 1, Date = new DateTime(2023, 3, 21), MedicationName = "Аспiрин", Dosage = 500, FrequencyPerDay = 2 },
//        new Prescription { RegistrationNumber = 1, Date = new DateTime(2023, 3, 21), MedicationName = "Кодепрон", Dosage = 200, FrequencyPerDay = 1 },
//        new Prescription { RegistrationNumber = 2, Date = new DateTime(2023, 3, 23), MedicationName = "Амоксиклав", Dosage = 875, FrequencyPerDay = 2 },
//        new Prescription { RegistrationNumber = 2, Date = new DateTime(2023, 3, 21), MedicationName = "Парацетамол", Dosage = 500, FrequencyPerDay = 3 },
//        new Prescription { RegistrationNumber = 3, Date = new DateTime(2023, 3, 22), MedicationName = "Аспiрин", Dosage = 1000, FrequencyPerDay = 2 },
//        new Prescription { RegistrationNumber = 3, Date = new DateTime(2023, 3, 21), MedicationName = "Кодепрон", Dosage = 400, FrequencyPerDay = 1 }
//        };


//        // (а) для кожного пацієнта повний перелік отриманих ліків з вказанням сумарної кількості кожного препарату в мг
//        Dictionary<string, Dictionary<string, uint>> medicationsByPatient = new Dictionary<string, Dictionary<string, uint>>();

//        foreach (Patient patient in patients)
//        {
//            medicationsByPatient.Add($"{patient.FirstName} {patient.LastName}", new Dictionary<string, uint>());

//            foreach (Prescription prescription in prescriptions)
//            {
//                if (prescription.RegistrationNumber == patient.RegistrationNumber)
//                {
//                    if (medicationsByPatient[$"{patient.FirstName} {patient.LastName}"].ContainsKey(prescription.MedicationName))
//                    {
//                        medicationsByPatient[$"{patient.FirstName} {patient.LastName}"][prescription.MedicationName] += prescription.getAmountPerDay();
//                    }
//                    else
//                    {
//                        medicationsByPatient[$"{patient.FirstName} {patient.LastName}"].Add(prescription.MedicationName, prescription.getAmountPerDay());
//                    }
//                }
//            }
//        }

//        foreach (KeyValuePair<string, Dictionary<string, uint>> patient in medicationsByPatient)
//        {
//            Console.WriteLine($"Пацієнт: {patient.Key}");

//            foreach (KeyValuePair<string, uint> medication in patient.Value)
//            {
//                Console.WriteLine($"Препарат: {medication.Key}, загальна кількість: {medication.Value}мг");
//            }
//            Console.WriteLine();
//        }

//        Dictionary<DateTime, Dictionary<string, uint>> medicationsByDay = new Dictionary<DateTime, Dictionary<string, uint>>();


//        foreach (Prescription pres in prescriptions)
//        {
//            medicationsByDay.Add(pres.Date, new Dictionary<string, uint>());
//            if (medicationsByDay[pres.Date].ContainsKey(pres.MedicationName))
//            {
//                medicationsByDay[pres.Date][pres.MedicationName] += pres.getAmountPerDay();
//            }
//            else
//            {
//                medicationsByDay[pres.Date].Add(pres.MedicationName, pres.getAmountPerDay());
//            }

//        }
//        foreach (KeyValuePair<DateTime, Dictionary<string, uint>> day in medicationsByDay)
//        {
//            Console.WriteLine($"День: {day.Key}");

//            foreach (KeyValuePair<string, uint> medication in day.Value)
//            {
//                Console.WriteLine($"Препарат: {medication.Key}, загальна кількість: {medication.Value}мг");
//            }
//            Console.WriteLine();
//        }
//        Dictionary<string, uint> amountsByMed = new Dictionary<string, uint>();

//        foreach (Prescription pres in prescriptions)
//        {
//            amountsByMed.Add(pres.MedicationName, 0);
//            amountsByMed[pres.MedicationName] += pres.getAmountPerDay();

//        }
//        foreach (KeyValuePair<string, uint> med in amountsByMed)
//        {
//            Console.WriteLine($"Препарат: {med.Key}, загальна кількість: {med.Value}мг");

//            Console.WriteLine();
//        }
//    }
//}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Patient[] patients = new Patient[]
        {
        new Patient { FirstName = "Іван", LastName = "Франко", RegNum = 1 },
        new Patient { FirstName = "Тарас", LastName = "Шевченко", RegNum = 2 },
        new Patient { FirstName = "Леся", LastName = "Українка", RegNum = 3 }
        };
        Prescription[] prescriptions = new Prescription[]
        {
        new Prescription { RegNum = 1, Date = new DateTime(2023, 3, 21), MedicationName = "Аспiрин", Dosage = 500, FrequencyPerDay = 2 },
        new Prescription { RegNum = 1, Date = new DateTime(2023, 3, 21), MedicationName = "Кодепрон", Dosage = 200, FrequencyPerDay = 1 },
        new Prescription { RegNum = 2, Date = new DateTime(2023, 3, 23), MedicationName = "Амоксиклав", Dosage = 875, FrequencyPerDay = 2 },
        new Prescription { RegNum = 2, Date = new DateTime(2023, 3, 21), MedicationName = "Парацетамол", Dosage = 500, FrequencyPerDay = 3 },
        new Prescription { RegNum = 3, Date = new DateTime(2023, 3, 22), MedicationName = "Аспiрин", Dosage = 1000, FrequencyPerDay = 2 },
        new Prescription { RegNum = 3, Date = new DateTime(2023, 3, 21), MedicationName = "Кодепрон", Dosage = 400, FrequencyPerDay = 1 }
        };

        var medicationsByPatient = new Dictionary<string, Dictionary<string, uint>>();

        foreach (var pat in patients)
        {
            medicationsByPatient.Add($"{pat.FirstName} {pat.LastName}", new Dictionary<string, uint>());

            foreach (var pres in prescriptions)
            {
                if (pres.RegNum == pat.RegNum)
                {
                    if (medicationsByPatient[$"{pat.FirstName} {pat.LastName}"].ContainsKey(pres.MedicationName)) medicationsByPatient[$"{pat.FirstName} {pat.LastName}"][pres.MedicationName] += pres.getAmountPerDay();
                    else medicationsByPatient[$"{pat.FirstName} {pat.LastName}"].Add(pres.MedicationName, pres.getAmountPerDay());
                }
            }
        }

        foreach (var pat in medicationsByPatient)
        {
            Console.WriteLine($"Пацієнт: {pat.Key}");
            foreach (var medication in pat.Value) Console.WriteLine($"Препарат: {medication.Key}, загальна кількість: {medication.Value}мг");
        }
        var medicationsByDay = new Dictionary<DateTime, Dictionary<string, uint>>();


        foreach (var pres in prescriptions)
        {
            medicationsByDay.Add(pres.Date, new Dictionary<string, uint>());
            if (medicationsByDay[pres.Date].ContainsKey(pres.MedicationName)) medicationsByDay[pres.Date][pres.MedicationName] += pres.getAmountPerDay();
            else medicationsByDay[pres.Date].Add(pres.MedicationName, pres.getAmountPerDay());
        }

        foreach (var day in medicationsByDay)
        {
            Console.WriteLine($"День: {day.Key}");
            foreach (var medication in day.Value) Console.WriteLine($"Препарат: {medication.Key}, загальна кількість: {medication.Value}мг\n");
        }


        var amountsByMed = new Dictionary<string, uint>();
        foreach (var pres in prescriptions)
        {
            amountsByMed.Add(pres.MedicationName, 0);
            amountsByMed[pres.MedicationName] += pres.getAmountPerDay();
        }
        foreach (var med in amountsByMed) Console.WriteLine($"Препарат: {med.Key}, загальна кількість: {med.Value}мг");
    }
}