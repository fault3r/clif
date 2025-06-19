using System;

namespace Clif.Infrastructure.Services.Markdown.Application
{
    public interface IClifMarkdown
    {
        string Render(string line);
    }
}