namespace Entities.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string YearPublished { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Directors { get; set; }
        public string Stars { get; set; }
        public string Votes { get; set; }
        public string Gross { get; set; }

        public MovieViewModel()
        {
            Title = string.Empty;
            Description = string.Empty;
            YearPublished = string.Empty;
            ThumbnailUrl = string.Empty;
            Directors = string.Empty;
            Stars = string.Empty;
            Votes = string.Empty;
            Gross = string.Empty;
        }
    }
}
