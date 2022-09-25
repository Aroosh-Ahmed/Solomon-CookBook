using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolomonCookBook.Models{
    public class Recepies{
        [Key]
        public int Recepie_ID{get;set;}
        public string? Recepie_Name{get;set;}
        public string? Category {get;set;}
        public string? video_url{get;set;}
        public string image_url{get;set;}
        public string? Ingredients{get;set;}
        public int? Likes{get;set;}
        public string? Directions { get; set; }
        public string? Comments{get;set;}
        public string? Country { get; set; }
        public string? type { get; set; }

        public string? status { get; set; }

    }
}