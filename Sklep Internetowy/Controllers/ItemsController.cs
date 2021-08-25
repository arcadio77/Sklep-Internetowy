using Sklep_Internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sklep_Internetowy.Controllers
{
    public class ItemsController : ApiController
    {
        public IEnumerable<Products> Get()
        {
            using (ShopItemDBContext dbContext = new ShopItemDBContext())
            {
                return dbContext.Products.ToList();
            }
        }
        public Products Get(int id)
        {
            using (ShopItemDBContext dBContext = new ShopItemDBContext())
            {
                return dBContext.Products.FirstOrDefault(e => e.ID == id);
            }
        }
        public HttpResponseMessage GetLaptopy()
        {
            using (ShopItemDBContext dBContext = new ShopItemDBContext())
            {
                var entity = dBContext.Products.Where(x => x.ProdType == "Laptopy").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
        }
        public HttpResponseMessage GetSmartfony()
        {
            using (ShopItemDBContext dBContext = new ShopItemDBContext())
            {
                var entity = dBContext.Products.Where(x => x.ProdType == "Smartfony").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
        }
        public HttpResponseMessage GetFoteleGamingowe()
        {
            using(ShopItemDBContext dBContext = new ShopItemDBContext())
            {
                var entity = dBContext.Products.Where(x => x.ProdType == "Fotele Gamingowe").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
        }
        public HttpResponseMessage GetKartyGraficzne()
        {
            using (ShopItemDBContext dBContext = new ShopItemDBContext())
            {
                var entity = dBContext.Products.Where(x => x.ProdType == "Karty Graficzne").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
        }
        public HttpResponseMessage GetProcesory()
        {
            using(ShopItemDBContext dBContext = new ShopItemDBContext())
            {
                var entity = dBContext.Products.Where(x => x.ProdType == "Procesory").ToList();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
        }
        public HttpResponseMessage Post([FromBody] Products product)
        {
            try
            {
                using (ShopItemDBContext dBContext = new ShopItemDBContext())
                    {
                        dBContext.Products.Add(product);
                        dBContext.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + product.ID.ToString());
                        return message;
                    }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }
        public HttpResponseMessage Put(int id, [FromBody] Products product)
        {
            try
            {
                using (ShopItemDBContext dBContext = new ShopItemDBContext())
                {
                    var entity = dBContext.Products.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.ProdType = product.ProdType;
                        entity.ProdName = product.ProdName;
                        entity.ProdDescription = product.ProdDescription;
                        entity.ProdPrice = product.ProdPrice;

                        dBContext.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (ShopItemDBContext dbContext = new ShopItemDBContext())
                {
                    var entity = dbContext.Products.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Product with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbContext.Products.Remove(entity);
                        dbContext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Item successufully deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
