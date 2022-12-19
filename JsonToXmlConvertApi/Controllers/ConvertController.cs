using JsonToXmlConvertApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace JsonToXmlConvertApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertController : Controller
    {
        private readonly ILogger<ConvertController> _logger;

        public ConvertController(ILogger<ConvertController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Convert JSON to XML")]
        public async Task<string> ConvertJsonToXml(string requestBody)
        {
            Debug.WriteLine(requestBody);
            //todo Atomic - implement logging
            JsonModel? jm = JsonConvert.DeserializeObject<JsonModel>(requestBody);

            //todo Atomic - deal with null exception, return proper HTTP error code and message
            if (jm == null) return "error";

            return await Task.Run(() =>
            {
                string result;
                XmlModel xm = new XmlModel { description = jm.description, name = jm.name };
                XmlSerializer xmlSerializer = new XmlSerializer(xm.GetType());
                using (var sw = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sw))
                    {
                        xmlSerializer.Serialize(writer, xm);
                        result = sw.ToString();
                    }
                }
                return result;
            });
        }
    }
}
