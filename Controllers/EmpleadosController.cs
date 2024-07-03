using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PM2App2.Controllers
{
    public static class EmpleadosController
    {
        //Crud
        //Create
        public async static Task<int> Create(Models.Empleado emple)
        {
            String jsonObject = JsonConvert.SerializeObject(emple);
            System.Net.Http.StringContent contenido = new StringContent(jsonObject,Encoding.UTF8,"application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;

                response = await client.PostAsync(Config.Config.EndPointCreate,contenido);

                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            return 1;
        }

        //Read


        //Update


        //Delete

    }
}
