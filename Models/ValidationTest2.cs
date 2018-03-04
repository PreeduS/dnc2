using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dnc2.Models{

    public class ValidationTest2 : DbContext {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "TextData is required")]
        //[MinLength(3, ErrorMessage = "TextData min length = 3")]
        public string TextData { get; set; }



    }

}