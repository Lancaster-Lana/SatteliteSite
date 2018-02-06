namespace Sattelite.Entities.ProjectAgg
{
    public class NewsContentFactory
    {
        public static NewsContent CreateNewsContent(string title, string shortDes, string content)
        {
            return CreateNewsContent(title, shortDes, content, null, null, null);
        }

        public static NewsContent CreateNewsContent(string title, string shortDes, string content, string smallImagePath, string mediumImagePath, string largeImagePath)
        {
            return new NewsContent
                {
                    Title = title,
                    ShortDescription = shortDes,
                    Content = content,
                    SmallImage = smallImagePath,
                    MediumImage = mediumImagePath,
                    BigImage = largeImagePath
                };
        }
    }
}