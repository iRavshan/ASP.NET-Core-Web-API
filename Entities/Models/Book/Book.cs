using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Book
{
    public class Book
    {
        [Key]
        [Column("BookId")]
        public Guid Id { get; set; } //Id

        [Required]
        public string Name { get; set; } //nomi

        [Required]
        public string Author { get; set; } //muallifi

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public int Year { get; set; } //nashr yili

        [Required]
        public string Publisher { get; set; } //nashriyot 
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
