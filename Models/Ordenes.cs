﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPedidosBlazor.Models
{
    public class Ordenes
    {
        [Key]
        public int OrdenId { get; set; }
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage ="Debe selecionar un suplidor para realizar la ordén.")]
        public int SuplidorId { get; set; }
        public decimal Monto { get; set; }

        public Ordenes()
        {
            Fecha = DateTime.Now;
        }
    }
}
