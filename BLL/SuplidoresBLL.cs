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
    public class SuplidoresBLL
    {
        private Contexto _contexto { get; set; }

        public SuplidoresBLL(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> Guardar(Suplidores suplidor)
        {
            if (!await Existe(suplidor.SuplidorId))
                return await Insertar(suplidor);
            else
                return await Modificar(suplidor);
        }

        private async Task<bool> Existe(int id)
        {
            bool ok = false;

            try
            {
                ok = await _contexto.Suplidores.AnyAsync(s => s.SuplidorId == id);
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Insertar(Suplidores suplidor)
        {
            bool ok = false;

            try
            {
                await _contexto.Suplidores.AddAsync(suplidor);
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;
        }

        private async Task<bool> Modificar(Suplidores suplidor)
        {
            bool ok = false;

            try
            {
                Detached(suplidor.SuplidorId);
                _contexto.Entry(suplidor).State = EntityState.Modified;
                ok = await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {

                throw;
            }

            return ok;

        }

        public async Task<Suplidores> Buscar(int id)
        {
            Suplidores suplidor;

            try
            {
                suplidor = await _contexto.Suplidores
                    .Where(s => s.SuplidorId == id)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return suplidor;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool ok = false;
            try
            {
                var registro = await _contexto.Suplidores.FindAsync(id);
                if(registro != null)
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

        public async Task<List<Suplidores>> GetSuplidores(Expression<Func<Suplidores, bool>> criterio)
        {
            List<Suplidores> lista = new List<Suplidores>();

            try
            {
                lista = await _contexto.Suplidores.Where(criterio).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }

            return lista;
        }

        private void Detached(int suplidorId)
        {
            var aux = _contexto
                .Set<Suplidores>()
                .Local
                .FirstOrDefault(s => s.SuplidorId == suplidorId);

            if (aux != null)
                _contexto.Entry(aux).State = EntityState.Detached;
        }
    }
}
