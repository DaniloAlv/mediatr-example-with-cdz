namespace MediatrExample.Application.Responses
{
    public class Error
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Error(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}