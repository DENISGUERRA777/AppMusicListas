using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMusicListas
{
    internal class Track
    {
        //Atributos
        public string Artist {  get; set; }
        public long Id {  get; set; }
        public string Title { get; set; }
        public string Album {  get; set; }

        //constructor
        public Track(string title, string artist, long id, string album) 
        { 
            Title = title;
            Artist = artist;
            Id = id;
            Album = album;

        }

        //sobreescribe el metodo tostring
        public override string ToString()
        {
            return $"{Title} - {Artist} - {Album} - {Id}";
        }
    }
}
