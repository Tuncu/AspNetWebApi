using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApiRouting.ActionBasedRouting.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        static List<string> values = new List<string>()
        {
            "value0","value1","value2"
        };

        // GET api/values
        [HttpGet] //olmaz ise HTTP/1.1 405 Method Not Allowed alırsın
        public IEnumerable<string> Values()
        {
            return values;
        }

        // GET api/values/5
        [HttpGet]
        public string SingleValue(int id)
        {
            return values[id];
        }

        // POST api/values
        [HttpPost]
        public void AddValue([FromBody]string value)
        {
            values.Add(value);
        }

        // PUT api/values/5
        [HttpPut]
        public void UpdateValue(int id, [FromBody]string value)
        {
            values[id] = value;
        }

        // DELETE api/values/5
        [HttpDelete]
        public void RemoveValue(int id)
        {
            values.RemoveAt(id);
        }
    }
}
