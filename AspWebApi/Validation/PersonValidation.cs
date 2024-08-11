using FluentValidation;

namespace AspWebApi.Validation
{
    /// <summary>
    /// cette class vas hériter de  AbstractValidator qui est definie dans l'"espace de nom fluent validation 
    /// l'ajout de regle de fluentValidation passe par le constructeur 
    /// </summary>
    public class PersonValidation : AbstractValidator<Personne>
    {
        public PersonValidation() 
        {
            //Rulefor: (loi pour) la proprieté souhaité qui  ce trouve dans la class
            //ou on veut faire notre validation 
            // the Rull ici pour confirmer que l'ajout ne doit pas etre vide 
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
             
        }
    }
}
