using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPedidosBlazor.Models
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        [Required(ErrorMessage ="Debe ingresar una descripción del producto.")]
        public string Descripcion { get; set; }
        [Range(minimum:10, maximum:1000000, ErrorMessage ="Costo no valido.")]
        public decimal Costo { get; set; }
        [Range(minimum:1, maximum:999, ErrorMessage ="Cantidad de inventario no valido.")]
        public float Inventario { get; set; }
    }
}
