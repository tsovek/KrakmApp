﻿using System;
using System.Collections.Generic;

namespace KrakmApp.Entities
{
    public class User : IEntityBase
    {
        public User()
        {
            UserRoles = new List<UserRole>();
            Hotels = new List<Hotel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
