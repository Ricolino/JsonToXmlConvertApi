namespace JsonToXmlConvertApi.Models
{
    public class JsonModel
    {
        //todo Atomic - define JSON model to be able to deserialize
        public int? id;
        public string? name;
        public string? description;
        public string[]? values;
    }
}

//sample JSON for testing
/*
{"id": 0, "name": null, "description": null, "values": null}
*/