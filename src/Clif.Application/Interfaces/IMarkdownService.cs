using System;

namespace Clif.Application.Interfaces
{
    public interface IMarkdownService
    {
        string Render(string text);
        
        string Gradient(string text);
    }
}