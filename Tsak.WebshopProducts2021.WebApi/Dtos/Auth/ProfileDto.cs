using System.Collections.Generic;

namespace Tsak.WebshopProducts2021.WebApi.Dtos.Auth
{
    public class ProfileDto
    {
        public List<string> Permissions { get; set; }
        public string Name { get; set; }
    }
}