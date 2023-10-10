using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IOrderRepository : IGenericRepository<Order>
    {

        //Task<Alunos> GetAllAlunosMatriculadosTurma();
        //Task<List<Order>> GetAlunosAtivos();
       
    }
}
