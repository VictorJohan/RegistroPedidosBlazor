using Microsoft.EntityFrameworkCore;
using RegistroPedidosBlazor.DAL;
using RegistroPedidosBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RegistroPedidosBlazor.BLL
{
    public class ProductosBLL
    {
        public Contexto _contexto { get; set; }

        public ProductosBLL(Contexto contexto)
        {
            this._contexto = contexto;
        }


        public async Task<bool> Guardar(Productos producto)
        {
            if (!await Existe(producto.ProductoId))
                return await Insertar(producto);
            else
                return await Modificar(producto);
        }

        private async Task<bool> Existe(int id)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.Productos.AnyAsync(p => p.ProductoId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Productos producto)
        {
            bool ok = false;

            try
            {
                await _contexto.Productos.AddAsync(producto);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Productos producto)
        {
            bool ok = false;

            try
            {
                Detached(producto.ProductoId);
                _contexto.Entry(producto).State = EntityState.Modified;
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;

        }

        public async Task<Productos> Buscar(int id)
        {
            Productos producto;

            try
            {
                producto = await _contexto.Productos
                    .Where(s => s.ProductoId == id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return producto;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await _contexto.Productos.FindAsync(id);
                if (registro != null)
                {
                    _contexto.Entry(registro).State = EntityState.Deleted;
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Productos>> GetSuplidores(Expression<Func<Productos, bool>> criterio)
        {
            List<Productos> lista = new List<Productos>();

            try
            {
                lista = await _contexto.Productos.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        private void Detached(int productoId)
        {
            var aux = _contexto
                .Set<Productos>()
                .Local
                .FirstOrDefault(p => p.ProductoId == productoId);

            if (aux != null)
                _contexto.Entry(aux).State = EntityState.Detached;
        }
    }
}
