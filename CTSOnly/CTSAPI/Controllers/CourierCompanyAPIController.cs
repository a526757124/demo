using CTS.Areas.ReceiptManagement.Controllers;
using CTS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CTSAPI.Controllers
{
    public class CourierCompanyAPIController : ApiController
    {
        // GET api/couriercompanyapi
        public IEnumerable<ComboData> Get()
        {
            using (CTSContext context = new CTSContext())
            {

                var result = context.CourierCompanys
                    .Where(p => !p.IsDeleted)
                    .Select(s => new ComboData
                    {
                        id = s.Id,
                        text = s.CourierName
                    }).ToList();
                return result;
            }
        }

        // GET api/couriercompanyapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/couriercompanyapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/couriercompanyapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/couriercompanyapi/5
        public void Delete(int id)
        {
        }
    }
}
