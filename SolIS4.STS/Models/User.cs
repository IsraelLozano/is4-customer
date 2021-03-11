using System;

namespace dic.sso.identityserver.oauth.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public int PkId { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public string PasswordTexto { get; set; }
        public string ReturnUrl { get; set; }
        public string Email { get; set; }



    }
}