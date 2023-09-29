﻿using System.ComponentModel.DataAnnotations;

namespace ProductListAPI.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
