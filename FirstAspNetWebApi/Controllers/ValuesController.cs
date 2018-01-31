using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstAspNetWebApi.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        static List<string> degerler = new List<string>()
        {
            "value0","value1","value2","value3"
        };

        // GET api/values
        public IEnumerable<string> GetABC()//Metot Adı önemli değil önemli olan Ön Ek
        {
            return degerler;
        }

        // GET api/values/5
        public string Get(int id)//Aynı imzalı (Parametre) iki adet Get Öğesi olduğunda 500 hatası verir
        {
            return degerler[id];
        }

        // POST api/values  Content-type: application/json
        public void Post([FromBody]string value)
        {
            degerler.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            degerler[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            degerler.RemoveAt(id);
        }
    }
}
