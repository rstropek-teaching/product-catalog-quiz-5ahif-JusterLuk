using Newtonsoft.Json;
using ProductCatalogMVC.Models;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalogMVC.Services
{
    public class ProductRepository:IProductRepository
    {
       
        public IRestResponse Connect(Method method)
        {
            var client = new RestClient("https://products-16c7.restdb.io/rest/product-catalog");
            var request = new RestRequest(method);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "3ea8f2bc19bf3d42a426f57c4eec323c3eeca");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);
            return response;
        }

       
        public IEnumerable<Product> GetAll()
        {
            IRestResponse response = Connect(Method.GET);
            string json = response.Content.ToString();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json);
            var orderedList = products.OrderBy(prod => prod.ProductID).ToList();
            return orderedList.ToArray();
        }
        
       
        public Product GetById(int id)
        {
            IEnumerable<Product> products = this.GetAll();
            return products.FirstOrDefault(prod => prod.ProductID == id);
        }

      
        public void CreateProduct(Product prod)
        {
            var client = new RestClient("https://products-16c7.restdb.io/rest/product-catalog");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "3ea8f2bc19bf3d42a426f57c4eec323c3eeca");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"Name\":\"" + prod.Name + "\",\"Description\":\"" + prod.Description + "\",\"PricePerUnit\":\"" + prod.PricePerUnit + "\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }

       
        public void DeleteProduct(int id)
        {
            IRestResponse response = Connect(Method.GET);
            string json = response.Content.ToString();
            string docid ="unknown";
             
            string[] arr = json.Split("}");
            foreach(string item in arr)
            {  
                if (item.Contains("\"ProductID\":"+id))
                {
                    string[] specarr = item.Split(",");
                    foreach(string str in specarr)
                    {
                        System.Console.WriteLine("item: "+str);
                        if (str.Contains("_id"))
                        {
                            string[] idstr = str.Split(":");
                            docid = idstr[1].Trim().Replace("\"","");
                            System.Console.WriteLine("Document ID: "+docid);
                        }
                    }
                }
            }
            var client = new RestClient("https://products-16c7.restdb.io/rest/product-catalog/" + docid);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "3ea8f2bc19bf3d42a426f57c4eec323c3eeca");
            request.AddHeader("content-type", "application/json");
            IRestResponse responsedelete = client.Execute(request);
        }

      
        public IEnumerable<Product> ProductSearch(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IEnumerable<Product> products = this.GetAll();
                var sol = new List<Product>();

                foreach (var item in products)
                {
                    if (item.Name.Trim().ToLower().Contains(name.Trim().ToLower()))
                    {
                        sol.Add(item);
                    }
                }
                return sol.ToArray();
            }
            return null;
        }
    }
}
