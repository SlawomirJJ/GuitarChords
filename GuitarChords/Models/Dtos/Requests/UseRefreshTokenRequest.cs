﻿using System.ComponentModel.DataAnnotations;

namespace GuitarChords.Models.Dtos.Requests
{
    public class UseRefreshTokenRequest
    {
        [Required]
        public string AccessToken { get; set; } = null!;
        [Required]
        public string RefreshToken { get; set; } = null!;
    }
}
