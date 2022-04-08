namespace Application.Model
{
    public class UserPostView
    {
        public string Text { get; set; }
        public string Image { get; set; }
        public int Likes { get; set; }
        public List<string> Tags { get; set; }
        public string PublishDate { get; set; }
    }
}
