﻿namespace MIS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Address { get; set; } = "";
        public string TelephoneNumber { get; set; } = "";
        public int Age { get; set; }
        public string Email { get; set; } = "";
    }
}
