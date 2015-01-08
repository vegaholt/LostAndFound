using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace LostAndFound.Models
{
    public class Item
    {
        public int ID { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public Category Category { get; set; }

        public string ImgUrl { get; set; }

        public double Reward { get; set; }

        public string Currency { get; set; }

        public double Lat { get; set; }
        
        public double Lon { get; set; }

        //public string County { get; set; }

        public DateTime FoundDate { get; set; }

        public DateTime LostDate { get; set; }

        public bool Lost { get; set; }
    }

    public class MyItemsViewModel 
    {
        public List<Item> LostItems { get; set; }
        public List<Item> FoundItems { get; set; }
    }

    public class ItemDBContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
    }
}