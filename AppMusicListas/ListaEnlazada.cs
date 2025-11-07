using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusicListas
{
    internal class ListaEnlazada
    {
        //atributos
        private Node head;
        public string Name {  get; set; }

        //constructor 
        public ListaEnlazada(string name)
        {
            head = null;
            Name = name;
        }

        //devueleve si la lista esta vacia
        public bool IsEmpty()
        {
            return head == null;
        }

        //agrega un elemto al final de la lista
        public void AddEnd(Track c)
        {
            if (c == null || string.IsNullOrWhiteSpace(c.Title))//valida los datos
            {
                Console.WriteLine("Datos inalidos, no se puede agregar a la lista");
                return;
            }
            Node nuevo = new Node(c);
            if (IsEmpty())//si la lista esta vacia
            {
                head = nuevo;
                head.Next = head;
                head.Prev = head;
                Console.WriteLine("Canción agregada correctamente.");
                return;
            }
            
            // insertar al final usando head.Prev como cola
            Node tail = head.Prev;
            tail.Next = nuevo;
            nuevo.Prev = tail;
            nuevo.Next = head;
            head.Prev = nuevo;
            Console.WriteLine("Canción agregada correctamente.");
        }

        //elimina un elemnto de la lista
        public void Remove(int id)
        {
            if (IsEmpty())//si la lista esta vacia
            {
                Console.WriteLine("La lista esta vacia");
                return;
            }
            Node actual = head;
            do
            {
                if (actual.Track.Id == id)
                {
                    // un solo elemento
                    if (actual.Next == actual)
                    {
                        head = null;
                        Console.WriteLine("Canción eliminada.");
                        return;
                    }

                    // enlazar vecinos
                    actual.Prev.Next = actual.Next;
                    actual.Next.Prev = actual.Prev;

                    // si eliminamos la cabeza, avanzar la cabeza
                    if (actual == head)
                        head = actual.Next;

                    Console.WriteLine("Canción eliminada.");
                    return;
                }

                actual = actual.Next;
            } while (actual != head);
            Console.WriteLine("Canción no encontrada.");//si no se encuentra en la lista
        }

        //busca una canion por su nombre
        public Track SearchByTitle(string title)
        {
            if (IsEmpty())//si la lista esta vacia
            {
                Console.WriteLine("La lista esta vacia.");
                return null;
            }
            Node actual = head;//busca el elemnto 
            do
            {
                if (string.Equals(actual.Track.Title, title, StringComparison.OrdinalIgnoreCase))//si encuentra el elemento
                {
                    return actual.Track;
                }
                actual = actual.Next;
            } while (actual != head);
                Console.WriteLine("Cancion no encontrada.");//si no se encuentra la cancion
            return null;
        }

        //muestra l toda la playlist
        public void Print()
        {
            if (IsEmpty())//si esta vacia
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }
            Console.WriteLine($"\n==== {Name} ====");
            Node actual = head;//si no esta vacia, imprime la data
            int i = 1;
            do
            {
                Console.WriteLine($"==>{i++}.{actual.Track.ToString()}");
                actual = actual.Next;
            } while (actual != head) ;
        }

        //exporta la  lista a un documento de texto plano
        public void ExportTxt (string ruta)
        {
            if (IsEmpty())//si esta vacia
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            using (StreamWriter writer = new StreamWriter(ruta))
            {
                writer.WriteLine(Name);
                Node actual = head;
                do
                {
                    writer.WriteLine(actual.Track.ToString());
                    actual = actual.Next;
                } while (actual != head);
            }
            Console.WriteLine($"Lista exportada a {ruta}");
        }


    }
}
