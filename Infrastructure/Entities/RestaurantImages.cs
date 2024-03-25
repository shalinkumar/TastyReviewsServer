using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
   
    public class RestaurantImages
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Guid")]
        //[Key]
        public Guid? Guid { get; set; }
        public bool IsInterior { get; set; }
        public Byte[] Image { get; set; }
        [NotMapped]
        public List<Byte[]> Images { get; set; }

        //public RestaurantModel RestaurantModel { get; set; }

        //public RestaurantModel RestaurantModel { get; set; }
      
    }    
}
