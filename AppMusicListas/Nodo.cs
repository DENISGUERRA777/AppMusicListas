using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusicListas
{
    internal class Node
    {
        //atributos
        public Track Track { get; set; }
        public Node Next { get; set; }
        public Node Prev {  get; set; }

        //constructor
        public Node(Track cancion)
        {
            Track = cancion;
            Next = null;
            Prev = null;
        }
    }
}
