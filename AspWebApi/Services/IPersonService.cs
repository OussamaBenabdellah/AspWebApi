using AspWebApi.Data.Models;
using AspWebApi.DTO;
using AspWebApi.Models;

namespace AspWebApi.Services
{
    public interface IPersonService
    {
        //Task<List<Personne>> GetAll();
        //Task<Personne?> GetById(int id);
        //Task<Personne> Add(Personne person);
        //Task<bool> Update(int id, Personne person);
        //Task<bool> Delete(int id);

        //création model DTO avec input et les out put 
        Task<List<PersonneOutPutModel>> GetAll();
        Task<PersonneOutPutModel?> GetById(int id);
        Task<PersonneOutPutModel> Add(PersonneInPutModel person);
        Task<bool> Update(int id, PersonneInPutModel person);
        Task<bool> Delete(int id);
       
    }
}
