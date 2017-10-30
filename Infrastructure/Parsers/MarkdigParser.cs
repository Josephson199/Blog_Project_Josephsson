using Infrastructure.Interfaces;
using System;

using Markdig;
using DataStore.Models;

namespace Infrastructure.Parsers
{
    public class MarkdigParser : IOutputStrategy
    {
        private static MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
                .UseDiagrams()
                .UseAdvancedExtensions()
                .UseYamlFrontMatter()
                .DisableHtml()
                .Build();

        public string Transform(BlogPost blogPost)
        {
            if (string.IsNullOrWhiteSpace(blogPost.Body))
            {
                throw new ArgumentNullException("Body cannot be null or whitespace.");
            }

            return Markdown.ToHtml(blogPost.Body, pipeline);
            
        }
    }
      

  
}
