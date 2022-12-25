# Atomic stuff

```csharp
using System;
using System.Xml.Serialization;
using System.IO;
using System.Text;
					
public class Program
{
	public static void Main()
	{
		ToBeSerialized tbs=new();
		tbs.SchemaLocation="ChilotiLocation";
		tbs.dt=DateTime.Now;
		tbs.cc=new();
		tbs.cc.s="Chiloti";
		tbs.cc.x=15;
		string xml;
		XmlSerializer serializer=new(typeof(ToBeSerialized));
		MemoryStream ms=new();
		using(StreamWriter writer=new(ms))
		{
		serializer.Serialize(writer, tbs);	
			ms.Close();
			xml=Encoding.UTF8.GetString(ms.GetBuffer());
		}
		
		Console.WriteLine(xml);
		
	}
}

public class ToBeSerialized
{
	[XmlAttributeAttribute("schemaLocation", AttributeName = "schemaLocation", 
    Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
    public string SchemaLocation {get;set;}
	public DateTime dt {get;set;}
	public CustomClass cc {get;set;}
}

public class MyClass
{
}

public class CustomClass:MyClass
{
	public int x {get; set;}
	[XmlAttribute]//it makes it an attr instead of element(default)
	public string s {get;set;}
}
```

