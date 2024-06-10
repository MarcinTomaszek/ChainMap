using ChainMapLib;

namespace ChainMapApp;

class Program
{
    static void Main(string[] args)
    {
        var dict1 = new Dictionary<string, string>
        {
            { "a", "1" },
            { "b", "2" },
            { "c", "3" },
            { "e", "5" }
        };

        var dict2 = new Dictionary<string, string>
        {
            { "b", "22" },
            { "c", "33" },
            { "d", "44" }
        };

        var dict3 = new Dictionary<string, string>
        {
            { "c", "333" },
            { "d", "444" },
            { "e", "555" }
        };

        //Creating Chain Map
        var chainMap = new ChainMap<string, string>(dict1, dict2, dict3);
        
        //Displaying all entries
        Console.WriteLine("Displaying all entries");
        foreach (var d in chainMap)
        {
            Console.WriteLine((d.Key, d.Value));
        }
        Console.WriteLine();
        
        //Count=
        Console.WriteLine("Counting all entries");
        Console.WriteLine($"Count: {chainMap.Count}");
        Console.WriteLine();
        
        //Adding entry to main dictionary
        Console.WriteLine("Adding entry to main dictionary");
        chainMap.Add("newEntry","000");
        Console.WriteLine($"Added entry {chainMap["newEntry"]} at key \"newEntry\"");
        Console.WriteLine();
        
        //Changing entry to main dictionary
        Console.WriteLine("Changing entry to main dictionary");
        chainMap["a"] = "000";
        Console.WriteLine($"Changed entry chainMap[a] = {chainMap["a"]}");
        Console.WriteLine();
        
        //Checking if key exists
        Console.WriteLine("Checking if \"a\" key exists");
        Console.WriteLine(chainMap.ContainsKey("a"));
        Console.WriteLine();
        
        //Deleting key 
        Console.WriteLine("Deleting \"a\" key");
        Console.WriteLine($"Is key deleted: {chainMap.Remove("a")}");
        Console.WriteLine();
        Console.WriteLine("Checking if \"a\" key still exists");
        Console.WriteLine(chainMap.ContainsKey("a"));
        Console.WriteLine();
        
        //Printing all Keys and Values
        Console.WriteLine("Printing all Keys and Values");
        Console.WriteLine(string.Join(",",chainMap.Keys));
        Console.WriteLine(string.Join(",",chainMap.Values));
        Console.WriteLine();
        
        //TryGetValue
        Console.WriteLine("Using TryGetValue (false if key don't exist, value of key if exists)");
        bool a = chainMap.TryGetValue("a", out var A);
        bool b = chainMap.TryGetValue("b", out var B);
        Console.WriteLine($"Checking if b key is in chainmap: {a}");
        Console.WriteLine($"Value of a key: {A}");
        Console.WriteLine($"Checking if b key is in chainmap: {b}");
        Console.WriteLine($"Value of b key: {B}");
        Console.WriteLine();
        
        //Adding dictionary
        var dict4 = new Dictionary<string, string>
        {
            { "g", "11111" },
            { "h", "22222" },
            { "i", "33333" }
        };
        Console.WriteLine("Adding dictionary");
        Console.WriteLine($"Count of dictionaries before: {chainMap.CountDictionaries}");
        chainMap.AddDictionary(dict4,-1);
        Console.WriteLine($"Count of dictionaries after: {chainMap.CountDictionaries}");
        Console.WriteLine();
        
        
        //Access to main dictionary
        Console.WriteLine("Access to main dictionary");
        var dic = chainMap.GetMainDictionary();
        Console.WriteLine("Main dictionary entries:");
        foreach (KeyValuePair<string, string> row in dic)
        {
            Console.WriteLine($"Key: {row.Key}, Value: {row.Value}");
        }
        Console.WriteLine();
        
        //Access to other dictionaries
        Console.WriteLine("Access to main and other dictionaries");
        var dics = chainMap.GetDictionaries;
        foreach (var r in dics)
        {
            foreach (var row in r)
            {
                Console.WriteLine($"Key: {row.Key}, Value: {row.Value}");
            }
        }
        Console.WriteLine();
        
        //Merging dictionaries
        Console.WriteLine("Merging dictionaries");
        var mergedDict = chainMap.Merge();
        Console.WriteLine("Merged dictionary entries");
        foreach (var row in mergedDict)
        {
            Console.WriteLine($"Key: {row.Key}, Value: {row.Value}");
        }
    }
}