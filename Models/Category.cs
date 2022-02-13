using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFramework;
using System.Data.Entity;

namespace WebApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
    }
}
