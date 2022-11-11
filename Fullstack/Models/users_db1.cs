using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Fullstack.Models
{
    public class users_db1
    {
        [Key]
        
        public int id { get; set; }

        public string name { get; set; }
        
        public string lastname { get; set; }
        
        public string email { get; set; }
    }
}