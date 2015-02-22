using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Frankenwiki.Plumbing;
using MarkdownSharp;

namespace Frankenwiki
{
    public class Frankengenerator: IFrankengenerator
    {
        static readonly Markdown Markdown = new Markdown();

        public void GenerateFromSource(
            string sourcePath,
            IFrankenstore store)
        {
            sourcePath = Path.GetDirectoryName(sourcePath + "\\");

            var pages =
                from filePath in EnumerateFiles(sourcePath, "*.md", "*.markdown")
                let fileAsMarkdown = File.ReadAllText(filePath)
                let slug = GetSlug(filePath, sourcePath)
                select BuildUpPage(slug, fileAsMarkdown);
            
            store.StoreAsync(pages);
        }

        static Frankenpage BuildUpPage(
            string slug,
            string fileAsMarkdown)
        {
            var frontMatter = fileAsMarkdown.FromYamlHeader();

            return new Frankenpage(
                slug: slug,
                title: GetTitle(frontMatter),
                markdown: fileAsMarkdown,
                html: Markdown.Transform(GetMarkdownWithoutYamlFrontMatter(fileAsMarkdown)));
        }

        static string GetTitle(IDictionary<string, object> frontMatter)
        {
            if (frontMatter.ContainsKey("title"))
            {
                return (string) frontMatter["title"];
            }

            return "eh?";
        }

        private static string GetMarkdownWithoutYamlFrontMatter(string fileAsMarkdown)
        {
            var frontMatter = GetYamlFrontMatter(fileAsMarkdown);
            return string.IsNullOrEmpty(frontMatter) 
                ? fileAsMarkdown 
                : fileAsMarkdown.Replace(frontMatter, "");
        }

        public static string GetYamlFrontMatter(string fileAsMarkdown)
        {
            var matches = YamlExtensions.YamlFrontMatterRegex.Matches(fileAsMarkdown);
            return matches.Count == 0 
                ? string.Empty 
                : matches[0].Value;
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