using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApp.Web.Repositories.Contracts
{
    public interface IRepository<TViewModel, TInputModel>
        where TViewModel : class
        where TInputModel : class
    {
        IEnumerable<TViewModel> GetAll();

        TViewModel Get(string id);

        Task<TInputModel> Create(TInputModel model);

        Task Delete(string id);

        Task Edit(TInputModel model);
    }
}