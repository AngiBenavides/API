using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using Newtonsoft.Json;

namespace API.Controller
{
    public class RESTListView
    {
        private string URL = "http://127.0.0.1/apirestpersona/post.php";

        public async Task<HttpResponseMessage> GetPersonas()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(URL);
            return response;
        }

        public async Task<HttpResponseMessage> CreatePersona(string nombres, string apellidos, string anio, string celular, string direccion)
        {
            var client = new HttpClient();
            var model = new Persona
            {
                Nombres = nombres,
                Apellidos = apellidos,
                Anio = anio,
                Celular = celular,
                Direccion = direccion
            };
            var json = JsonConvert.SerializeObject(model);
            StringContent body = new StringContent(json, Encoding.UTF8,"application/json");
            var response = await client.PostAsync(URL, body);
            return response;
        }

        public async Task< HttpResponseMessage> UpdatePersona(string ID, string nombres, string apellidos, string anio, string celular, string direccion)
        {
            var client = new HttpClient();
            var model = new Persona()
            {
                Nombres = nombres,
                Apellidos = apellidos,
                Anio = anio,
                Celular = celular,
                Direccion = direccion
            };
            var json = JsonConvert.SerializeObject(model);
            StringContent body = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(URL+ ID, body);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteProducto(string ID)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync(URL+ ID);
            return response;
        }
    }
}
