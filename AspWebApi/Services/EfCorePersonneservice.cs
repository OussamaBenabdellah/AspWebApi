using AspWebApi.Data;
using AspWebApi.Data.Models;
using AspWebApi.DTO;
using AspWebApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
 namespace AspWebApi.Services
{
    public class EfCorePersonneservice : IPersonService
    {
        

        //private readonly ApiDbContext context;
        ////injection par constructeur du ApiDbContext
        //public EfCorePersonneservice(ApiDbContext context)
        //{
        //    this.context = context;
        //}
        ////public async Task<Personne > Add(Personne  person)
        ////{

        ////    context.People.Add( person);
        ////    await context.SaveChangesAsync();
        ////    return  person;
        ////}
        ////public async Task<bool> Delete(int id)
        ////{
        ////    return await context.People.Where(p => p.Id == id).ExecuteDeleteAsync() > 0;
        ////}
        ////public async Task<List<Personne>> GetAll()
        ////{
        ////    return await context.People.ToListAsync();
        ////}
        ////public async Task<Personne?> GetById(int id)
        ////{
        ////    return await context.People.Where(p => p.Id == id).FirstOrDefaultAsync();
        ////}
        ////public async Task<bool> Update(int id, Personne person)
        ////{
        ////    return await context.People.Where(p => p.Id == id).ExecuteUpdateAsync(
        ////        per =>
        ////        per.SetProperty(pe => pe.LastName, person.LastName)
        ////           .SetProperty(pe => pe.Birthday, person.Birthday)
        ////           .SetProperty(pe => pe.Name, person.Name)) > 0;
        ////}


        ////private PersonneOutPutModel ToOutPutModel(Personne dbPerson)

        ////       => new PersonneOutPutModel(
        ////            dbPerson.Id,
        ////            $"{dbPerson.LastName} {dbPerson.Name}",
        ////            dbPerson.Birthday == DateTime.MinValue ? null : dbPerson.Birthday);

        //public async Task<PersonneOutPutModel> Add(PersonneInPutModel person)
        //{
        //    var dbPerson = new Personne
        //    {
        //        Birthday = person.Birthday.GetValueOrDefault(),
        //        LastName = person.LastName,
        //        Name = person.Name,
        //    };
        //    context.People.Add(dbPerson);
        //    await context.SaveChangesAsync();
        //    return ToOutPutModel(dbPerson);
        //}
        //public async Task<bool> Delete(int id)
        //{
        //    return await context.People.Where(p => p.Id == id).ExecuteDeleteAsync() > 0;
        //}
        //public async Task<List<PersonneOutPutModel>> GetAll()
        //{
        //    return (await context.People.ToListAsync()).ConvertAll(ToOutPutModel);
        //}
        //public async Task<PersonneOutPutModel?> GetById(int id)
        //{
        //    var dbPerson = await context.People.Where(p => p.Id == id).FirstOrDefaultAsync();
        //    if (dbPerson is not null) return ToOutPutModel(dbPerson);
        //    return  null;
        //}
        //public async Task<bool> Update(int id, PersonneInPutModel person)
        //{
        //    return await context.People
        //        .Where(p => p.Id == id)
        //        .ExecuteUpdateAsync(
        //        per =>
        //        per.SetProperty(pe => pe.LastName, person.LastName)
        //           .SetProperty(pe => pe.Birthday, person.Birthday)
        //           .SetProperty(pe => pe.Name, person.Name)) > 0;
        //}

        #region AutoMapper


        private readonly ApiDbContext context;
        private readonly IMapper mapper;
        public EfCorePersonneservice(
             IMapper mapper, //interface fournie par auto mapper 
             ApiDbContext context)
        {
            this.mapper = mapper; 
            this.context = context;
        }
         
        public async Task<PersonneOutPutModel> Add(PersonneInPutModel person)
        {
            var dbPerson = mapper.Map<Personne>(person);
            context.People.Add(dbPerson);
            await context.SaveChangesAsync();
            // cette méthode transforme model de Personne issue de la BDD a PersonneOutPutModel avec l'autoMapper
            return mapper.Map<PersonneOutPutModel>(dbPerson);
        }
        public async Task<bool> Delete(int id)
        {
            return await context.People.Where(p => p.Id == id).ExecuteDeleteAsync() > 0;
        }
        public async Task<List<PersonneOutPutModel>> GetAll()
        {
            return (await context.People.ToListAsync()).ConvertAll(mapper.Map<PersonneOutPutModel>);
        }
        public async Task<PersonneOutPutModel?> GetById(int id)
        {
            var dbPerson = await context.People.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (dbPerson is not null) return  mapper.Map<PersonneOutPutModel>(dbPerson);
            return null;
        }
        public async Task<bool> Update(int id, PersonneInPutModel person)
        {
            return await context.People
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(
                per =>
                per.SetProperty(pe => pe.LastName, person.LastName)
                   .SetProperty(pe => pe.Birthday, person.Birthday)
                .SetProperty(pe => pe.Address, person.Address)
                   .SetProperty(pe => pe.Name, person.Name)) > 0;
        }
        #endregion
    }

}

     

