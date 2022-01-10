using Entities.Models.Book;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO.Book
{
    public class Book
    {
        [Required]
        public string Name { get; set; } //nomi

        [Required]
        public string Author { get; set; } //muallifi

        [Required]
        public int Year { get; set; } //nashr yili

        [Required]
        public string Publisher { get; set; } //nashriyot 

        [Required]
        public GenreEnums Genre { get; set; } //janri

        [Required]
        public int PageCount { get; set; } //varaqlar soni

        [Required]
        public LanguageEnums Language { get; set; } //tili
        public string Description { get; set; } //izoh

        [Required]
        public BindEnums Bind { get; set; } //muqova turi
    }
}
