using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace LostAndFound.Models
{
    public class Item
    {
        public int ID { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Tittel")]
        public string Name { get; set; }

        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }

        [Display(Name = "Kategori")]
        public Category Category { get; set; }

        [Display(Name = "Bilde")]
        public string ImgUrl { get; set; }

        [Display(Name = "Finnerlønn")]
        public double Reward { get; set; }

        [Display(Name = "Valutta")]
        public string Currency { get; set; }

        [Display(Name = "Eierskapsbevis")]
        public string Claim { get; set; }

        public double Lat { get; set; }
        
        public double Lon { get; set; }

        [Display(Name="Fylke")]
        public County County { get; set; }

        [Display(Name = "Adresse")]
        public string Adress { get; set; }
        
        [Display(Name = "Dato funnet")]
        public DateTime FoundDate { get; set; }

        [Display(Name = "Dato tapt, fra")]
        public DateTime LostDateFrom { get; set; }

        [Display(Name = "Dato tapt, til")]
        public DateTime LostDateTo { get; set; }

        [Required]
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