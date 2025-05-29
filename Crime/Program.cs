using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

// Main program without namespace
public class Program
{
    static void Main()
    {
        string path = @"C:\Users\Damian\Downloads\8h9b-rp9u_version_1106.csv";

        try
        {
            var records = CsvCrimeReader.LoadCrimeData(path, 10000);

            Console.WriteLine($"Loaded {records.Count} records.");
            Console.WriteLine("First 5 records:");
            Console.WriteLine("--------------------------------------------------");
            foreach (var record in records.Take(100))
            {
                Console.WriteLine($"{record.PerpRace}"); //{record.ArrestDate} | {record.OfnsDesc} | {record.LonLat} | 
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file was not found at {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

public static class CsvCrimeReader
{
    public static List<CrimeRecord> LoadCrimeData(string filePath, int limit = 10000)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            BadDataFound = null,
            MissingFieldFound = null,
            HeaderValidated = null,
            PrepareHeaderForMatch = args => args.Header.ToLower(),
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<CrimeRecordMap>();
            return csv.GetRecords<CrimeRecord>().Take(limit).ToList();
        }
    }
}
