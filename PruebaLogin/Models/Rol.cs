﻿using System;
using System.Collections.Generic;

namespace PruebaLogin.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolOperacion = new HashSet<RolOperacion>();
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<RolOperacion> RolOperacion { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
