namespace AspWebApi.Data.Models
{
    /// <summary>
    /// on a créee un DTO data trensfer object Personne
    /// 
    /// </summary>
    //
    public class Personne
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime? Birthday { get; set; }
        public string Address { get; internal set; }
    }
}
