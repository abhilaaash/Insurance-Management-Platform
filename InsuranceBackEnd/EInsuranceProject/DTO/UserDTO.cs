﻿namespace EInsuranceProject.DTO
{
    public class UserDTO
    {
        public int UserId {  get; set; }
        public string Username { get; set; }
        public string Paasword { get; set; }

        public int? RoleId { get; set; }
    }
}
