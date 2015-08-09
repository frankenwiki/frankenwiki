using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CsQuery;
using Frankenwiki.Domain.EventHandlers;
using Frankenwiki.Events;
using Frankenwiki.Plumbing;
using Humanizer;
using MarkdownSharp;

namespace Frankenwiki
{
    public class Frankengenerator: IFrankengenerator
    {
        static readonly Markdown Markdown = new Markdown();
        private readonly IDomainEventBroker _domainEventBroker;

        public Frankengenerator(IDomainEventBroker domainEventBroker)
        {
            _domainEventBroker = domainEventBroker;
        }

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

            _domainEventBroker.Raised(new FrankenwikiGeneratedEvent());
        }

        static Frankenpage BuildUpPage(
            string slug,
            string fileAsMarkdown)
        {
            var frontMatter = fileAsMarkdown.FromYamlHeader();
            var markdown = GetMarkdownWithoutYamlFrontMatter(fileAsMarkdown);
            var html = Transform(markdown);
            var title = GetTitle(frontMatter, ref html, slug);

            return new Frankenpage(
                slug: slug,
                title: title,
                markdown: fileAsMarkdown,
                html: html,
                categories: GetCategories(frontMatter));
        }

        public static string Transform(string markdown)
        {
            string html;

            lock (Markdown)
            {
                html = Markdown.Transform(markdown);
            }
            
            return html;
        }

        private static FrankenpageCategory[] GetCategories(
            IDictionary<string, object> frontMatter)
        {
            return new[]
            {
                frontMatter.ContainsKey("category") ? frontMatter["category"] : null,
                frontMatter.ContainsKey("categories") ? frontMatter["categories"] : null
            }
                .Where(x => x != null)
                .Cast<string>()
                .SelectMany(x => x.Split(','))
                .Select(x => x.Trim())
                .Select(x => new FrankenpageCategory(x, x.Humanize(LetterCasing.Title)))
                .ToArray();
        }

        static string GetTitle(
            IDictionary<string, object> frontMatter,
            ref string html,
            string slug)
        {
            if (frontMatter.ContainsKey("title"))
            {
                return (string) frontMatter["title"];
            }

            var dom = new CQ(html);
            var firstH1 = dom["h1"]
                .Select(x => x.InnerText)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(firstH1))
            {
                dom["h1"].FirstElement().Remove();
                html = dom.Render();
                return firstH1;
            }

            return slug
                .Split('/')
                .Last()
                .Humanize(LetterCasing.Title);
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