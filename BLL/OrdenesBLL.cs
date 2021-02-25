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
    public class OrdenesBLL
    {
        private Contexto _contexto { get; set; }
        
        public OrdenesBLL(Contexto contexto)
        {
            this._contexto = contexto;

        }

        public async Task<bool> Guardar(Ordenes orden)
        {
            if (!await Existe(orden.OrdenId))
                return await Insertar(orden);
            else
                return await Modificar(orden);
        }

        private async Task<bool> Existe(int id)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.Ordenes.AnyAsync(o => o.OrdenId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Ordenes orden)
        {
            bool ok = false;

            try
            {
                await _contexto.Ordenes.AddAsync(orden);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Ordenes orden)
        {
            bool ok = false;
            try
            {
                Detached(orden.OrdenId);

                _contexto.Database.ExecuteSqlRaw($"DELETE FROM OrdenesDetalle WHERE OrdenId={orden.OrdenId}");
                foreach (var item in orden.Detalle)
                {
                    _contexto.Entry(item).State = EntityState.Added;
                }

                _contexto.Entry(orden).State = EntityState.Modified;
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<Ordenes> Busacar(int id)
        {
            Ordenes orden;
            try
            {
                orden = await _contexto.Ordenes
                    .Where(o => o.OrdenId == id)
                    .Include(d => d.Detalle)
                    .ThenInclude(p => p.Producto)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return orden;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await Busacar(id);
                if(registro != null)
                {
                    _contexto.Ordenes.Remove(registro);
                    ok = await _contexto.SaveChangesAsync() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        public async Task<List<Ordenes>> GetOrdenes(Expression<Func<Ordenes, bool>> criterio)
        {
            List<Ordenes> lista = new List<Ordenes>();

            try
            {
                lista = await _contexto.Ordenes.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        private void Detached(int OrdenId)
        {
            var entidad = _contexto.Set<Ordenes>().Local.FirstOrDefault(o => o.OrdenId == OrdenId);
            if(entidad != null)
            {
                _contexto.Entry(entidad).State = EntityState.Modified;
            }
        }
    }
}
