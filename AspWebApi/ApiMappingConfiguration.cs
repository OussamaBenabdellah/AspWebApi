using AspWebApi.Data.Models;
using AspWebApi.DTO;
using AspWebApi.Models;
using AutoMapper;
namespace AspWebApi
{
    /// <summary>
    /// on vas hériter de la class Profile pour utiliser ces méthode
    /// </summary>
    public class ApiMappingConfiguration : Profile

    {
        public ApiMappingConfiguration()
        {
            //cette methode nous permet a
            //crée un mapping entre class Personne et PersonneOutPutModel
            CreateMap<Personne, PersonneOutPutModel>()
                .ConstructUsing(p => new 
                PersonneOutPutModel(p.Id,
                $"{p.LastName}{p.Name}",
                p.Birthday == DateTime.MinValue ? null : p.Birthday,
                p.Address));
            //avec le formember on previen l'auto mapper qu'il ignore
            //le fait que l'Id ne ce trouve pas dans PersonneInPutModel
            CreateMap<PersonneInPutModel, Personne>();
                //.ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}
