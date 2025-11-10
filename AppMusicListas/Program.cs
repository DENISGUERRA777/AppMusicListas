using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AppMusicListas
{
    internal class Program
    {
        static async Task Main(string[] args)
        { 
            //se declaran las propiedades a ocupar
            DeezerManager api = new DeezerManager();
            ListaEnlazada[] playLists = new ListaEnlazada[10];
            
            
            int option;

            //menu del usuario
            do
            {
                //opciones del menu
                Console.WriteLine("\n ====Fia Music PlayList Maker ====");
                Console.WriteLine("1.Buscar cancion en Deezer");
                Console.WriteLine("2.Mostrar mis  playlist");
                Console.WriteLine("3.Eliminar PlayList o canciones de una playList");
                Console.WriteLine("4. Exportar mis listas");
                Console.WriteLine("5.Importar lista");
                Console.WriteLine("6.salir");
                //solicita una opcion al usuario
                if (!int.TryParse(Console.ReadLine(), out option))
                    Console.WriteLine("Debe de introducir un dato valdio");
                Console.WriteLine();
                //maneja la  opcion del usuario
                switch (option)
                {
                    case 1:
                        //se pide que informacion del artista o de la cancion
                        Console.WriteLine("ingrese el nombre de la cancion o artista");

                        // se realiza la busqueda y se muestran los resultado
                        string query = Console.ReadLine();
                        Track[] resultados = new Track[10];
                        await api.SearchTrack(query, resultados);
                        Console.WriteLine();

                        //se le pregunta al usuario si desea agregar a una playlist
                        Console.WriteLine("seleccione una cancion para gregar a la playList" +
                            "o precione 0 para salir");
                        int selection;
                        while (!int.TryParse(Console.ReadLine(), out selection) || selection < 0 || selection > resultados.Length)
                            Console.WriteLine("debe de elejir una opcion de la lista de resultados");
                        Console.WriteLine();

                        if (selection == 0)//si el usuario desea salir
                            break;

                        for (int i = 0; i<resultados.Length; i++)//agrega canciones a la lista de reproduccion
                        {
                            if(i+1 == selection)
                            {
                                //sub menu elegir lista o crar una
                                int nListas  = ShowLists(playLists);
                                Console.WriteLine("0.Crear una nueva lista de reproduccion");
                                Console.WriteLine("Seleccione una opcion");
                                int selecList;
                                while(!int.TryParse(Console.ReadLine(), out selecList) || selecList<0 || selecList>nListas)//validacion de selelccion
                                    Console.WriteLine("Debde de elejir un opcion valida");
                                if (selecList == 0)//si se desea crear una nueva lista
                                {
                                    Console.WriteLine("Nombre de la lista:");
                                    string nombre = Console.ReadLine();
                                    ListaEnlazada myPlayLista = new ListaEnlazada(nombre);
                                    myPlayLista.AddEnd(resultados[i]);
                                    InsertList(playLists, myPlayLista);
                                    Console.WriteLine("la cancion se agrego con exito");
                                    break;
                                }
                                //elije una lista e ingresa la cancion
                                playLists[selecList-1].AddEnd(resultados[i]);
                                Console.WriteLine("Cancion agregada con exito");
                                Console.WriteLine();
                                break;
                            }
                        }
                        break;

                    case 2:
                        //nuestra las listas que tiene el usuario
                        int listas = ShowLists(playLists);

                        //si no hay listas disponibles
                        if (listas == 0)
                        { Console.WriteLine("Debe de crar un lista de reproducion primero");
                            break;
                        }
                        //muestra la lista seleccionada por el usuario
                        Console.WriteLine("Selecione una lista para mostrar las canciones");
                        int listS;
                        while(!int.TryParse(Console.ReadLine(), out listS) || listS<=0 || listS>listas)
                            Console.WriteLine("Sellecione una lista pra mostrar las canciones");
                        Console.WriteLine();
                        playLists[listS-1].Print();

                        break;

                    case 3:
                        //nuestra las listas que tiene el usuario
                        int listasD = ShowLists(playLists);

                        //si no hay listas disponibles
                        if (listasD == 0)
                        {
                            Console.WriteLine("Debe de crar un lista de reproducion primero");
                            break;
                        }
                        //muestra la lista seleccionada por el usuario
                        Console.WriteLine("Selecione una lista que quiera gestionar");
                        int listG;
                        while (!int.TryParse(Console.ReadLine(), out listG) || listG <= 0 || listG > listasD)
                            Console.WriteLine("Selecione una lista para gestionar");
                        Console.WriteLine();
                        //pregunta si desea eliminar la lista o una cancion
                        Console.WriteLine("Desea eliminar la lista(1) o eliminar canciones(2)");
                        int loC;
                        while (!int.TryParse(Console.ReadLine(), out loC) || loC <= 0 || loC >= 3)
                            Console.WriteLine("seleccione una opcion valida");
                        Console.WriteLine() ;

                        if(loC == 1)
                        {
                            playLists[listG - 1] = null;
                            
                            for (int i = listG - 1; i < playLists.Length - 1; i++)
                            {
                                playLists[i] = playLists[i + 1]; 
                            }
                            playLists[playLists.Length - 1] = null;

                            Console.WriteLine("Lista eliminada con exito");
                            Console.WriteLine();
                            break;
                        }

                        //elimina canciones
                        string seguir = "s";
                        while (seguir.ToLower().Equals("s"))
                        {
                            playLists[listG - 1].Print();
                            Console.WriteLine("Introduzca el id de la cancion");
                            int nCancion;

                            while (!int.TryParse(Console.ReadLine(), out nCancion))
                                Console.WriteLine(" debe de introducir un numero valio");
                            Console.WriteLine();
                            playLists[listG - 1].Remove(nCancion);

                            Console.WriteLine("desea seguir eliminando Si(s)/No cualquier tecla");
                            seguir = Console.ReadLine();
                            Console.WriteLine();
                        }

                        break;

                    case 4:
                        //muestra las lisats
                        int numeroLs = ShowLists(playLists);
                        //solicuta seleccionar una lista
                        Console.WriteLine("Seleccione una lista para exportar");
                        int listEX;
                        while(!int.TryParse(Console.ReadLine(), out listEX) || listEX <0 || listEX> numeroLs)
                            Console.WriteLine("Seleccione una lista valida para exportar o 0 para salir ");
                        Console.WriteLine();

                        //si desea salir
                        if (listEX == 0)
                            break;
                        
                        //exporta la lista
                        playLists[listEX-1].ExportTxt($"{playLists[listEX-1].Name}.txt");
                        break;

                    case 5:
                        //solicita a ruta de la lista
                        Console.WriteLine("Introduzca ruta del archivo de texto(o precione 0 para salir)");
                        Console.WriteLine("Puedes probar con las rutas: \"coldplay hits.txt\" \"metalica hits.txt\")");
                        
                        while (true)
                        {
                            string ruta = Console.ReadLine();
                            if (ruta == "0")
                            {
                                Console.WriteLine("Importación cancelada");
                                break;
                            }
                            try
                            {
                                //si el archivo no es encontrado
                                if (!File.Exists(ruta))
                                {
                                    Console.WriteLine("El archivo especificado no existe, intente nuevamente.");
                                    continue;
                                }
                                //lee el archivo
                                StreamReader sr = new StreamReader(ruta);
                                string line = sr.ReadLine();
                                //crea la lista y le asigna un nombre
                                ListaEnlazada myPlayListEx = new ListaEnlazada(line);
                                //introduce los datos a la nueva lista
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] partes = line.Split(new string[] { " - " }, StringSplitOptions.None);

                                    string title = partes[0];
                                    string artist = partes[1];
                                    string album = partes[2];
                                    long id = long.Parse(partes[3]);
                                    Track song = new Track(title, artist, id, album);
                                    myPlayListEx.AddEnd(song);
                                }
                                //busca un espacio vacio en el array
                                InsertList(playLists, myPlayListEx);
                                sr.Close();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($" Error al importar la lista: {ex.Message}");
                            }
                            break;
                        }
                        
                        break;

                    case 6:
                        Console.WriteLine("Saliendo de la aplicacion");
                        break;

                    default:
                        Console.WriteLine("Debe de elegir una opcion del 1 al 5");
                        break;
                }

            } while (option != 6);
        }

        // muestra las listas existentes y retorna el numero de listas 
        public static int ShowLists(ListaEnlazada[] listas)
        {
            int count = 0;
            
            for (int i = 0; i < listas.Length; i++)
            {
                if (listas[i] != null)
                {
                    Console.WriteLine(i + 1 + "." + listas[i].Name);
                    count++;
                }
            }
            if (count == 0)
            {
                Console.WriteLine("No hay niguna lista de reproducion");
            }
            return count;
        }

        //inserta una nueva lista al array
        public static void InsertList(ListaEnlazada[] listas, ListaEnlazada l)
        {
            for (int i = 0; i< listas.Length; i++)
            {
                if (listas[i] == null)
                {
                    listas[i] = l;
                    Console.WriteLine("Nueva lista agregada correctamente");
                    return;
                }
            }
            Console.WriteLine("No se pueden agregar mas listas llegaste al limite, Suscribete el plan preium");
        }

        
    }

    
}
