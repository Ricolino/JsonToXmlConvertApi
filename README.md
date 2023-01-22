```csharp
using System;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class Program
{
    public static void Main()
    {
        ToBeSerialized tbs = new();
        
        tbs.TaxTotal=new TaxTotal();
        tbs.TaxTotal.TaxAmmount=new TaxAmmount();
        tbs.TaxTotal.TaxAmmount.currencyID = "RON";
        tbs.TaxTotal.TaxAmmount.ammount = 199.12;

        tbs.TaxTotal.SubTotal= new SubTotal();
        tbs.TaxTotal.SubTotal.TaxableAmmount=new TaxableAmmount();
        tbs.TaxTotal.SubTotal.TaxableAmmount.currencyID = "RON";
        tbs.TaxTotal.SubTotal.TaxableAmmount.ammount = 132.15;

        tbs.TaxTotal.SubTotal.TaxAmmount=new TaxAmmount();
        tbs.TaxTotal.SubTotal.TaxAmmount.currencyID = "RON";
        tbs.TaxTotal.SubTotal.TaxAmmount.ammount = 199.12;

        string xml;
        XmlSerializer serializer = new(typeof(ToBeSerialized));
        MemoryStream ms = new();
        using (StreamWriter writer = new(ms))
        {
            serializer.Serialize(writer, tbs);
            ms.Close();
            xml = Encoding.UTF8.GetString(ms.GetBuffer());
        }

        Console.WriteLine(xml);
        Console.ReadKey();

    }
}


public class ToBeSerialized
{

    [XmlElement(Namespace = "http://www.w3.org/2001/XMLSchema-instance")] //replace with cac namespace
    public TaxTotal TaxTotal { get; set; }    
}

public class TaxTotal
{
    [XmlElement(Namespace ="")]//put cbc namespace
    public TaxAmmount TaxAmmount { get; set; }

    [XmlElement(Namespace = "")]//put cac namespace
    public SubTotal SubTotal { get; set; }

}

public class SubTotal
{
    [XmlElement(Namespace ="")]//put cbc namespace
    public TaxableAmmount TaxableAmmount { get; set;}

    [XmlElement(Namespace = "")]//put cbc namespace
    public TaxAmmount TaxAmmount { get; set; }
}

public class TaxableAmmount
{
    [XmlAttribute]
    public string currencyID { get; set; }

    [XmlText]
    public double ammount { get; set; }    
}

public class TaxAmmount
{
    [XmlAttribute]
    public string currencyID { get; set; }

    [XmlText]
    public double ammount { get; set; }
}


```
