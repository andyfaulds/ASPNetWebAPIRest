using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASPNetWebAPIRest.Models;



namespace ASPNetWebAPIRest.Controllers
{
    [Authorize]
    public class ListItemsController : ApiController
    {
        private static List<CustomListItem> _ListItems { get; set; } = new List<CustomListItem>();
        
        // GET: api/ListItems
        public IEnumerable<CustomListItem> Get()
        {
            // return new string[] { "value1", "value2" };
            return _ListItems;
        }

        // GET: api/ListItems/5
        public HttpResponseMessage Get(int id)
        {
            var item = _ListItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // POST: api/ListItems
        public HttpResponseMessage Post([FromBody]CustomListItem model)
        {
            if (string.IsNullOrEmpty(model?.Text))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var maxId = 0;
            if (_ListItems.Count > 0)
            {
                maxId = _ListItems.Max(x => x.Id);
            }
            model.Id = maxId + 1;
            _ListItems.Add(model);
            return Request.CreateResponse(HttpStatusCode.Created, model);
        }

        public HttpResponseMessage Put(int id, [FromBody]CustomListItem model)
        {
            if (string.IsNullOrEmpty(model?.Text))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var item = _ListItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                // Update *all* of the item's properties
                item.Text = model.Text;

                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // DELETE: api/ListItems/5
        public HttpResponseMessage Delete(int id)
        {
            var item = _ListItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _ListItems.Remove(item);
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
