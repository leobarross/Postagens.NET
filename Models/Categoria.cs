﻿using System.ComponentModel.DataAnnotations;

namespace Postagens.NET.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required]
        public required string Nome { get; set; }
    }
}
