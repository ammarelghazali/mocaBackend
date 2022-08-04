using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCA.Core.DTOs.SSO.User
{
    public class RefreshTokenRespose
    {
        public long Id { get; set; }
        public string JWToken { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public string Model { get; set; }
        public string Uniquly_Identifier { get; set; }
        public string OS { get; set; }
        public string DeviceType { get; set; }
        public string Brand { get; set; }
    }
}
