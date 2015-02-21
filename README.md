# frankenwiki
Markdown based statically generated wiki engine


## To do:

- scan /wiki and generate static pages
- configure where the wiki .md files live ('/wiki')
- save said pages to table storage
- pipe out stored static pages on request
- templates / themes
- scan for links on pages, record: pages that this page links to, pages that link to this page
- convention test: for each page, all pages that this page links to should exist
- convention test: for each page except for /index, there should be at least one page linking to that page (no orphans)
- index all page content in Lucene
- search functionality
- pluggable security
- NuGet the .Core project




