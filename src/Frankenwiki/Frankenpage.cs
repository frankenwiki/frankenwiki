using System.Collections.Generic;
using System.Linq;
using CsQuery;
using Frankenwiki.Plumbing;

namespace Frankenwiki
{
    public class Frankenpage
    {
        public string Html { get; private set; }
        public string Markdown { get; private set; }
        public string Slug { get; private set; }
        public FrankenpageCategory[] Categories { get; private set; }
        public string Title { get; private set; }
        private IEnumerable<Frankenpage> _allPages;
        private FrankindexItem[] _allLinks;
        private FrankindexItem[] _allLinksToMe;

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
        }

        public FrankindexItem[] AllLinks
        {
            get
            {
                _allLinks = _allLinks ?? GetAllLinks();
                return _allLinks;
            }
        }

        public FrankindexItem[] AllLinksToMe
        {
            get
            {
                _allLinksToMe = _allLinksToMe ?? GetAllLinksToMe();
                return _allLinksToMe;
            }
        }

        public void SetAllPages(IEnumerable<Frankenpage> allPages)
        {
            _allPages = allPages;
            _allLinks = null;
        }

        private FrankindexItem[] GetAllLinks()
        {
            var anchors = CQ.CreateFragment(Html).Find("a").ToArray();

            return (
                from anchor in anchors
                let href = anchor.GetAttribute("href")
                where href.StartsWith("/wiki/")
                let slug = href.Substring(6)
                let linkedPage = _allPages.SingleOrDefault(x => x.Slug == slug)
                where linkedPage != null
                select new FrankindexItem(linkedPage.Slug, linkedPage.Title)
                ).DistinctBy(x => x.PageSlug)
                .ToArray();
        }

        private FrankindexItem[] GetAllLinksToMe()
        {
            return (
                from linkingPage in _allPages
                where linkingPage.AllLinks.Any(x => x.PageSlug == Slug)
                select new FrankindexItem(linkingPage.Slug, linkingPage.Title)
                ).DistinctBy(x => x.PageSlug)
                .ToArray();
        }
    }
}