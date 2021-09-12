using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
   public interface IEnderecoRepository
   {
      Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
   }
}
