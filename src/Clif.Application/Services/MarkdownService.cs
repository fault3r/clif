using System;
using Clif.Application.Interfaces;
using Clif.Infrastructure.Services.Markdown.Application;
using Clif.Infrastructure.Services.Markdown.Infrastructure;

namespace Clif.Application.Services
{
    public class MarkdownService : IMarkdownService
    {
        private readonly IClifMarkdown _clifMarkdown;

        public MarkdownService(IClifMarkdown clifMarkdown)
        {
            _clifMarkdown = clifMarkdown;
        }

        public string Render(string text) => _clifMarkdown.Render(text);

        public string Gradient(string text) => GradientText.ToGradient(text);
    }
}