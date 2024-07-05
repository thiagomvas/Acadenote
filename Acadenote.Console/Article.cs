namespace Acadenote.Console
{
    namespace StudyProject.Core.ArticleAggregate
    {
        /// <summary>
        /// Represents an article.
        /// </summary>
        public class Article
        {
            /// <summary>
            /// Gets or sets the ID of the article.
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the title of the article.
            /// </summary>
            public string Title { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the content of the article.
            /// </summary>
            public string Content { get; set; } = string.Empty;

            /// <summary>
            /// Gets or sets the disciplines associated with the article.
            /// </summary>
            public string[] DisciplineIds { get; set; } = new string[0];

            /// <summary>
            /// Gets or sets the subjects associated with the article.
            /// </summary>
            public string[] SubjectIds { get; set; } = new string[0];

            /// <summary>
            /// Gets or sets the topics associated with the article.
            /// </summary>
            public string[] TopicIds { get; set; } = new string[0];

            public DateTime LastEdit { get; set; } = DateTime.Now;

            public static readonly Article NotFound = new Article
            {
                Id = "0",
                Title = "Oops! Article Not Found",
                Content = "The article you are looking for could not be found. Please try again later.",
            };

        }
    }
}
