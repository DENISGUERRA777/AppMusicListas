using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppMusicListas
{
    internal class DeezerManager
    {
        //se declara un cliente 
        private readonly HttpClient client = new HttpClient();

        //busca las canciones consumiendo la api de deezer
        public async Task SearchTrack(string query, Track[]  tmp)
        {
            if (string.IsNullOrWhiteSpace(query))//valida la consulta
            {
                Console.WriteLine("Debe ingresar un término de búsqueda.");
                return;
            }

            if (tmp == null || tmp.Length == 0)//valida el array
            {
                Console.WriteLine("Array de resultados no inicializado o de tamaño cero.");
                return;
            }
            if (int.TryParse(query, out _))
                query = $"\"{query}\"";

            //consulta la api y espera una respuesta
            string url = $"https://api.deezer.com/search?q={Uri.EscapeDataString(query)}";
            try 
            {
                var response = await client.GetStringAsync(url);
                if (string.IsNullOrWhiteSpace(response))//valida los datos obtenidos de la api
                {
                    Console.WriteLine("Respuesta vacía de la API.");
                    return;
                }
                var json = JsonDocument.Parse(response);

                //muestra los resiltados de la busqueda
                Console.WriteLine("\n==== Resultados de la Busqueda ====");
                int count = 0;
                foreach (var item in json.RootElement.GetProperty("data").EnumerateArray())
                {
                    //guarda los resultado en un array temporal
                    Track c = new Track(item.GetProperty("title").GetString(),
                        item.GetProperty("artist").GetProperty("name").GetString(),
                        item.GetProperty("id").GetInt64(),
                        item.GetProperty("album").GetProperty("title").GetString());
                    tmp[count] = c;
                    //los muestra en consola
                    Console.WriteLine($"{++count}. {c.Title} - {c.Artist} - {c.Album}");
                    if (count >= tmp.Length) break;//solo muestra hasta q se llene el array
                }
                if (count == 0)//si no se encuentran resultados
                    Console.WriteLine("No se encontraron resultados.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error al conectar con la API: " + ex.Message);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Respuesta inválida de la API: " + ex.Message);
            }
            
        }

    }
}
