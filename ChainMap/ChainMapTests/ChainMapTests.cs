using System.Collections.ObjectModel;
using ChainMapLib;

namespace ChainMapTests;


public class Tests
{
    [Test]
    public void SetIndexTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Act
        cm["b"] = "200";
        cm["e"] = "250";
        
        //Assert
        Assert.AreEqual( ("200", "250"),(cm["b"],cm["e"]));
    }

    [Test]
    public void GetIndexTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Assert
        Assert.AreEqual( ("1", "2", "5", "8"),(cm["a"],cm["b"],cm["d"],cm["e"]));
    }
    
    
    [Test]
    public void EnumeratorTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        var eRes = new List<(string, string)>{("a", "1"), ("b", "2"), ("c", "3"),("b", "3"),("c", "4"),("d", "5"),("c", "2"),("b", "7"),( "e", "8")};
        
        //Act
        var res = new List<(string, string)> { };

        foreach (var d in cm)
        {
            res.Add((d.Key, d.Value));
        }
        
        //Assert
        Assert.AreEqual( eRes,res);
    }
    
    [Test]
    public void AddTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Act
        cm.Add("d", "0000");
        var res = cm["d"];
        //Assert
        Assert.AreEqual( "0000",res);
    }
    
    [Test]
    public void TryAddTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        
        //Assert
        Assert.IsTrue(cm.TryAdd("g", "1111"));
        Assert.IsFalse(cm.TryAdd("a", "1111"));
    }
    
    [Test]
    public void ClearTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Act
        cm.Clear();
        
        //Assert
        Assert.IsFalse(cm.ContainsKey("a"));
        Assert.IsTrue(cm.ContainsKey("b"));

    }
    
    [Test]
    public void CountTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Assert
        Assert.IsTrue(cm.Count == 9);
    }
    
    [Test]
    public void ContainsKeyTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Act
        
        //Assert
        Assert.IsTrue(cm.ContainsKey("a"));
        Assert.IsTrue(cm.ContainsKey("d"));
        Assert.IsTrue(cm.ContainsKey("e"));
        Assert.IsFalse(cm.ContainsKey("abcd"));
    }
    
    [Test]
    public void ContainsValueTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);

        //Assert
        Assert.IsTrue(cm.ContainsValue("1"));
        Assert.IsTrue(cm.ContainsValue("4"));
        Assert.IsTrue(cm.ContainsValue("7"));
        Assert.IsFalse(cm.ContainsValue("1111"));
    }
    
    [Test]
    public void RemoveTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Assert
        Assert.IsTrue(cm.Remove("a"));
        Assert.IsTrue(cm.ContainsKey("a") == false);
        Assert.IsFalse(cm.Remove("abcd"));
        
    }
    
    [Test]
    public void TryGetValueTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
    
        //Act
        var res = cm.TryGetValue("b", out var a);
        var res1 = cm.TryGetValue("abcd", out var b);
        
        //Assert
        Assert.IsTrue(res);
        Assert.AreEqual(  "2",a  );
        Assert.IsFalse(res1);
        
    }
    
    [Test]
    public void KeysTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        
        //Assert
        Assert.AreEqual( new List<string>(){"a", "b", "c", "d", "e"},cm.Keys);
    }
    
    [Test]
    public void ValuesTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Assert
        Assert.AreEqual(new List<string>(){"1", "2", "3", "4", "5", "7", "8"}, cm.Values );
    }
    
    [Test]
    public void CountDictionariesTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        
        //Assert
        Assert.AreEqual(3, cm.CountDictionaries);
    }
    
    [Test]
    public void AddDictionaryTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Act
        cm.AddDictionary(new Dictionary<string, string>() { { "a", "122" }, { "j", "1234" } }, 5);
        
        //Assert
        Assert.AreEqual(4,cm.CountDictionaries);
    }
    
    
    
    [Test]
    public void RemoveDictionaryTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Act
        cm.RemoveDictionary(1);
        cm.RemoveDictionary(1000);

        //Assert
        Assert.AreEqual( 2,cm.CountDictionaries);
    }
    
    
    [Test]
    public void ClearDictionariesTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Act
        cm.ClearDictionaries();
        
        //Assert
        Assert.IsTrue(cm.Count == 0);
    }
    
    [Test]
    public void GetDictionaryTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        
        //Assert
        Assert.AreEqual(new ReadOnlyDictionary<string, string>(d2),cm.GetDictionary(0) );
    }
    
    [Test]
    public void GetMainDictionaryTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Assert
        Assert.AreEqual(d1, cm.GetMainDictionary());
    }
    
    [Test]
    public void MergeTest()
    {
        //Arrange
        var d1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var d2 = new Dictionary<string, string>{{ "b", "3" },{ "c", "4" },{ "d", "5" }};
        var d3 = new Dictionary<string, string>{ { "c", "2" },{ "b", "7" },{ "e", "8" }};
        var cm = new ChainMap<string, string>(d1, d2, d3);
        
        //Act
        var eRes = new Dictionary<string, string>()
        {
            {"a", "1"},
            {"b", "2"},
            {"c", "3"},
            {"d", "5"},
            {"e", "8"}
        };
        
        //Assert
        Assert.AreEqual(eRes, cm.Merge());
    }
}