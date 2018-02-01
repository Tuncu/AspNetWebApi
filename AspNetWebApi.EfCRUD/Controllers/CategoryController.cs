using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.EfCRUD.Controllers
{
    public class CategoryController : ApiController
    {
        NORTHWNDEntities db = new NORTHWNDEntities();

        public IEnumerable<Category> Get()
        {
            db.Configuration.ProxyCreationEnabled = false;//xml için çalışır ama lazy loding kapanır
            /*model sınıfada IgnoreDataMember attribute eklenmeli
             * */

            return db.Categories.Include("Products").ToList();
        }

    }
}
