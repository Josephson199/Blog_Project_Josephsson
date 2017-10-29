using Infrastructure.Interfaces;
using System;

using Markdig;

namespace Infrastructure.Parsers
{
    public class MarkdigParser : IOutputStrategy
    {
        private readonly MarkdownPipeline _pipeline;

        public MarkdigParser(MarkdownPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        public string[] Transform(IBlogPost blogPost)
        {
            if (string.IsNullOrWhiteSpace(blogPost.Title))
            {
                throw new ArgumentNullException("Title cannot be null or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(blogPost.Body))
            {
                throw new ArgumentNullException("Body cannot be null or whitespace.");
            }

            var array = new string[2];
            array[0] = Markdown.ToHtml(blogPost.Title, _pipeline);
            array[1] = Markdown.ToHtml(blogPost.Body, _pipeline);
            return array;
        }
    }
      

  
}
