﻿namespace MagniboardBackend.Data.EntityModels
{
    public class User
    {
        public int Id { get; set; }
        public string role { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
