namespace Dictionary.Domain.Models
{
    public class Word
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Title} - {Description}";
        }
    }
}
