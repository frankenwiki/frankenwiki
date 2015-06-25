using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;

namespace Frankenwiki
{
    public class Frankenpage
    {
        public string Html { get; private set; }
        public string Markdown { get; private set; }
        public string Slug { get; private set; }
        public FrankenpageCategory[] Categories { get; private set; }
        public string Title { get; private set; }

        public Frankenpage(
            string slug,
            string markdown, 
            string html,
            string title,
            FrankenpageCategory[] categories)
        {
            Markdown = markdown;
            Html = html;
            Slug = slug;
            Categories = categories;
            Title = title;

            _allLinks = new Lazy<IEnumerable<string>>(() => (
                from anchor in CQ.CreateFragment(Html).Find("a").ToList()
                select anchor.GetAttribute("href")).Distinct());
        }

        private readonly Lazy<IEnumerable<string>> _allLinks;
        private Lazy<IEnumerable<string>> _allLinksToMe;

        public IEnumerable<string> AllLinks { get { return _allLinks.Value; } }
        public IEnumerable<string> AllLinksToMe { get { return _allLinksToMe.Value; } }

        public void SetAllLinksToMe(Lazy<IEnumerable<string>> allLinksToMe)
        {
            _allLinksToMe = allLinksToMe;
        }
    }

    public class FrankenpageCategory
    {
        public string Slug { get; private set; }
        public string Name { get; private set; }

        public FrankenpageCategory(
            string slug,
            string name)
        {
            Slug = slug;
            Name = name;
        }
    }
}