namespace AspWebApi.DTO
{
    public record PersonneInPutModel
    {
        public string  Name { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
    }
}
