﻿namespace GuitarChords.Models.Dtos.Requests
{
    public class ResetPasswordRequest
    {
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}
