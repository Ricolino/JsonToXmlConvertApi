# Atomic stuff

In ConvertController is used JsonModel to deserialize input like this:

```csharp
JsonModel? jm = JsonConvert.DeserializeObject<JsonModel>(requestBody);
```

Then whatever values are needed are transferred to XmlModel like this:

```csharp
XmlModel xm = new XmlModel { description = jm.description, name = jm.name };
```
or whatever values are needed and where are needed in every XmlModel.

The JsonModel (input in our case) and XmlModel (output in our case) models don't need to be identical.
There are included also the two original comment lines with todo Atomic. Of

```csharp
public class JsonModel
    {        
    	//todo Atomic - define XML model to serialize to
        public int? id;
        public string? name;
        public string? description;
        public string[]? values;
    }
    
public class XmlModel
    {
        //todo Atomic - define XML model to serialize to
        public string? name; // but does not need to be the same name or even type (can be a number here an string in JSON and parsed)
        public string? description;
	//or can even have extra
	public class Extra
	{
		public int encriptedId; // can be (for example, but not necesarily) JsonModel.id.Encrypt() or something
	}
	public Extra extra;
    }
```

What if more than one output is needed? Let's say one for accountants and one for the financial istitution?
Well...

```csharp
public class XmlAccountantModel
    {
        //todo Atomic - define XML model to serialize to
        public string? name;
        public string? description;
    }
    
    public class XmlFinancialInstitutionModel
    {
        //todo Atomic - define XML model to serialize to
	public string? name;
        public string? description;
	
	//and some extra coming who knows where from
        public DateTime dt {get;set;}
	public CustomClass cc {get;set;}
    }
```

So, the XML models can be changed with no impact on JSON deserialization.

But the JsonModel should be changed only if the structure of the received Json will ever change.

