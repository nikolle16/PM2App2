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
            try
            {
                String jsonObject = JsonConvert.SerializeObject(emple);
                System.Net.Http.StringContent contenido = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = null;

                    response = await client.PostAsync(Config.Config.EndPointCreate, contenido);

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            Console.WriteLine($"Ha Ocurrido un Error: {response.ReasonPhrase}");
                            return -1;
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ha Ocurrido un Error: {ex.ToString()}");
                return -1;
            }
        }

        //Read
        public async static Task<List<Models.Empleado>> Get()
        {
            List<Models.Empleado>emplelist= new List<Models.Empleado>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = null;
                    response = await client.GetAsync(Config.Config.EndPointList);
                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                            try
                            {
                                emplelist = JsonConvert.DeserializeObject<List<Models.Empleado>>(result);
                            }
                            catch (JsonException jex)
                            {

                            }
                        }
                    }
                    return emplelist;
                }
            }
            catch(Exception ex) 
            {
                return null;
            }
        }

        //Update
        public static async Task<int> Update(Models.Empleado empleado)
        {
            try
            {
                String jsonObject = JsonConvert.SerializeObject(empleado);
                StringContent contenido = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    // Concatenar el ID del empleado al endpoint de actualización
                    HttpResponseMessage response = await client.PutAsync($"{Config.Config.EndPointUpdate}/{empleado.id}", contenido);

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        return 1;
                    }
                    else
                    {
                        Console.WriteLine($"Error al actualizar: {response?.ReasonPhrase}");
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar: {ex}");
                return -1;
            }
        }

        //Delete
        public async static Task<int> Delete(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = null;

                    response = await client.DeleteAsync($"{Config.Config.EndPointDelete}/{id}");

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var result = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            Console.WriteLine($"Ha Ocurrido un Error: {response.ReasonPhrase}");
                            return -1;
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ha Ocurrido un Error: {ex.ToString()}");
                return -1;
            }
        }
    }
}
