﻿using System.ComponentModel.DataAnnotations;

namespace TestTask.Entities
{
    public class ProductInfo
    {
        [Required]
        public string Name {  get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price {  get; set; }

    }
}
