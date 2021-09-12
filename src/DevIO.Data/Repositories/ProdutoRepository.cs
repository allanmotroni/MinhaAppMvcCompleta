using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repositories
{
   public class ProdutoRepository : Repository<Produto>, IProdutoRepository
   {
      public ProdutoRepository(MeuDbContext db)
         : base(db) { }
      
      public async Task<Produto> ObterProdutosFornecedor(Guid id)
      {
         return await DbSet.AsNoTracking().Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
      }

      public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
      {
         return await DbSet.AsNoTracking().Include(p => p.Fornecedor).OrderBy(p => p.Nome).ToListAsync();
      }

      public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
      {
         return await Buscar(p => p.FornecedorId == fornecedorId);
      }
   }
}
