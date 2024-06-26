﻿namespace FTech.Application.DataTransferObjects.Auth
{
    public class JWTOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public string ExpiredInMinutes { get; set; }
    }
}
