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

        public string Transform(string markdown)
        {
            if (string.IsNullOrWhiteSpace(markdown))
            {
                throw new ArgumentNullException(nameof(markdown));
            }

            var result = Markdown.ToHtml(markdown, pipeline);

            return result;
        }
    }
      

  
}
