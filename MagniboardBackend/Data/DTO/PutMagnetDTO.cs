﻿using System.ComponentModel.DataAnnotations;

namespace MagniboardBackend.Data.DTO
{
    public class PutMagnetDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Your table name is too long!")]
        public string MagnetName { get; set; } = null!;
        public string MagnetColor { get; set; } = null!;
        public string TextColor { get; set; } = null!;
    }
}
