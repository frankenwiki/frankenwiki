# frankenwiki
Markdown based statically generated wiki engine

## Usage:

The example site (`Frankenwiki.Example.NancyWeb`) has the example wiki source in the `/test-wiki` folder (`/src/Frankenwiki.Example.NancyWeb/test-wiki`). The `test-wiki` part of that is configured in `Web.config` (app settings -> `WikiSourcePath`).

On startup, build a `Frankengenerator` instance and call `GenerateFromSource()`. It requires a path to the wiki files (a heirarchy of Markdown files) and an implementation of `IFrankenstore`. An in-memory `IFrankenstore` implementation is provided in the core project (`InMemoryFrankenstore`) and a store using Azure table/blob storage is planned.

Files are indexed using the path to the file within the source wiki file with extensions stripped - the slug.

- `c:\frankenwiki\wiki-files\index.md` would have a slug of `index`
- `c:\frankenwiki\wiki-files\types-of-crystal-confectionaries\purple-haze.md` would have a slug of `types-of-crystal-confectionaries/purple-haze`

In the example NancyFx site that hosts a Frankenwiki, pages are served using a greedy segment route pattern such that `/wiki/index` will serve the `index` slug, and `/wiki/types-of-crystal-confectionaries/purple-haze` will serve the `types-of-crystal-confectionaries/purple-haze` slug. It uses the `IFrankenstore` method `GetPageAsync` to retrieve the page content. Thus, links in the source wiki files of the form `[/wiki/foo/bar](Link)` will link directly to a page with the `foo/bar` slug. See `PageModule` for more information.

### Static resources

Any files within the wiki site that are referenced directly are served as static content. For example, if the wiki site is `test-wiki` and there is an image at `test-wiki/images/cat.jpg`, a wiki page could reference the image using something like `![](/wiki/images/cat.jpg/`) and the image will be served when the page is rendered.

Note the trailing forward-slash in the path the image:

	![](/wiki/images/cat.jpg/

This is required due to a Nancy bug. See the "Known issues" section below.



## To do:


- [x] scan /wiki and generate static pages
- [x] configure where the wiki .md files live ('/wiki')
- [x] pipe out stored static pages on request
- [x] serve images and other static assets
- [x] templates / themes
- [x] Page title - use YAML front matter (http://jekyllrb.com/docs/frontmatter/), pull out H1, or fall back to humanised slug
- [ ] scan for links on pages, record: pages that this page links to, pages that link to this page
- [ ] file system watcher - rebuild on change
- [ ] record errors: for each page, all pages that this page links to should exist
- [ ] record errors: for each page except for /index, there should be at least one page linking to that page (no orphans)
- [ ] search functionality (Lucene?)
- [ ] pluggable security
- [ ] NuGet it
- [x] Categories - pull out of YAML front matter
- [ ] Wiki title in test site - to be configurable
- [x] Index of all pages
- [x] Index of all categories
- [ ] table storage IFrankenstore


## Known issues

- The `/wiki` route will fail to match if the requested slug has a dot somewhere in the last part of the segment (eg. `/wiki/images/cat.jpg`). This is due to a [possible bug](https://github.com/NancyFx/Nancy/issues/1829) in NancyFx. A workaround is to add a trailing slash (`/wiki/images/cat.jpg/`) which will allow the route to match, then the trailing slash is trimmed out before processing the route.


## License

Frankenwiki uses Apache License Version 2.0. See <LICENSE.md> for the full license.

Includes code from [Code 52's Pretzel](https://github.com/Code52/pretzel) which uses the Microsoft Public License. See <LICENSE-MS-PL.md> for the full license.

