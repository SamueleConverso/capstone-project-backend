﻿namespace Capstone_Project.DTOs.Account {
    public class LoginRequestDto {
        public required string Email {
            get; set;
        }

        public required string Password {
            get; set;
        }

        //public bool? RememberMe {
        //    get; set;
        //}
    }
}
