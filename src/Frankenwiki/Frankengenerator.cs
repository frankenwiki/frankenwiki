using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarkdownSharp;

namespace Frankenwiki
{
    public class Frankengenerator: IFrankengenerator
    {
        public void GenerateFromSource(
            string sourcePath,
            IFrankenstore store)
        {
            sourcePath = Path.GetDirectoryName(sourcePath + "\\");

            var markdown = new Markdown();

            var pages =
                from filePath in EnumerateFiles(sourcePath, "*.md", "*.markdown")
                let fileAsMarkdown = File.ReadAllText(filePath)
                let fileAsHtml = markdown.Transform(fileAsMarkdown)
                let slug = GetSlug(filePath, sourcePath)
                select new Frankenpage(
                    fileAsMarkdown,
                    fileAsHtml,
                    slug);
            
            store.StoreAsync(pages);
        }

        static string GetSlug(string filePath, string sourcePath)
        {

            return filePath
                // strip the source path out
                .Replace(sourcePath, "")
                // replace backslash with forward slash
                .Replace("\\", "/")
                // drop .md|.markdown extensions
                .Replace(".md", "")
                .Replace(".MD", "")
                .Replace(".markdown", "")
                .Replace(".MARKDOWN", "")
                // cut leading slash
                .Substring(1);
        }

        static IEnumerable<string> EnumerateFiles(string path, params string[] patterns)
        {
            return patterns.AsParallel()
                .SelectMany(x => Directory.EnumerateFiles(path, x, SearchOption.AllDirectories));
        }
    }
}