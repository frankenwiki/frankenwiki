# frankenwiki
Markdown based statically generated wiki engine

## Usage:

TBD: configuration. It needs to point to the wiki root, at the moment this is a fully-qualified static path.

On startup, build a `Frankengenerator` instance and call `GenerateFromSource()`. It requires a path to the wiki files (a heirarchy of Markdown files) and an implementation of `IFrankenstore`. An in-memory frankenstore is provided in the core project and a store using Azure table/blob storage is planned.

Files are indexed using the path to the file within the source wiki file with extensions stripped - the slug.

- `c:\frankenwiki\wiki-files\index.md` would have a slug of `index`
- `c:\frankenwiki\wiki-files\types-of-crystal-confectionaries\purple-haze.md` would have a slug of `types-of-crystal-confectionaries/purple-haze`

In the example NancyFx site that hosts a Frankenwiki, pages are served using a greedy segment route pattern such that `/wiki/index` will serve the `index` slug, and `/wiki/types-of-crystal-confectionaries/purple-haze` will serve the `types-of-crystal-confectionaries/purple-haze` slug. It uses the `IFrankenstore` method `GetPageAsync` to retrieve the page content. Thus, links in the source wiki files of the form `[/wiki/foo/bar](Link)` will link directly to a page with the `foo/bar` slug. See `PageModule` for more information.


## To do:


- [x] scan /wiki and generate static pages
- [ ] configure where the wiki .md files live ('/wiki')
- [ ] save said pages to table storage
- [x] pipe out stored static pages on request
- [ ] scan and serve images and other assets
- [ ] templates / themes
- [ ] scan for links on pages, record: pages that this page links to, pages that link to this page
- [ ] file system watcher - rebuild on change
- [ ] record errors: for each page, all pages that this page links to should exist
- [ ] record errors: for each page except for /index, there should be at least one page linking to that page (no orphans)
- [ ] search functionality (Lucene?)
- [ ] pluggable security
- [ ] NuGet it




