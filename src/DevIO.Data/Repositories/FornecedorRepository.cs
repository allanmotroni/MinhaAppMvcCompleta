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
   public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
   {
      public FornecedorRepository(MeuDbContext context) 
         : base(context) { }

      public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
      {
         return await DbSet
            .AsNoTracking()
            .Include(p => p.Endereco)
            .FirstOrDefaultAsync(p => p.Id == id);
      }

      public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
      {
         return await DbSet
            .AsNoTracking()
            .Include(p => p.Produtos)
            .Include(p => p.Endereco)
            .FirstOrDefaultAsync(p => p.Id == id);

      }
   }
}
