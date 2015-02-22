using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace Frankenwiki.Plumbing
{
    // Shamefully stolen from Pretzel - https://github.com/Code52/pretzel/blob/7b84d876d6d4c105f9f52f4c1d466ca732c89ed0/src/Pretzel.Logic/Extensions/YamlExtensions.cs
    internal static class YamlExtensions
    {
        public static readonly Regex YamlFrontMatterRegex = new Regex(@"(?s:^---(.*?)---)");

        public static IDictionary<string, object> FromYamlHeader(this string text, bool skipHeader = false)
        {
            StringReader input;
            var results = new Dictionary<string, object>();

            if (!skipHeader)
            {
                var m = YamlFrontMatterRegex.Matches(text);
                if (m.Count == 0)
                    return results;

                input = new StringReader(m[0].Groups[1].Value);
            }
            else
            {
                input = new StringReader(text);
            }

            var yaml = new YamlStream();
            yaml.Load(input);

            if (yaml.Documents.Count == 0)
                return results;

            var root = yaml.Documents[0].RootNode;

            var collection = root as YamlMappingNode;
            if (collection != null)
            {
                foreach (var entry in collection.Children)
                {
                    var node = entry.Key as YamlScalarNode;
                    if (node != null)
                    {
                        results.Add(node.Value, GetValue(entry.Value));
                    }
                }
            }

            return results;
        }

        private static object GetValue(YamlNode value)
        {
            var collection = value as YamlMappingNode;
            if (collection != null)
            {
                var results = new Dictionary<string, object>();
                foreach (var entry in collection.Children)
                {
                    var node = entry.Key as YamlScalarNode;
                    if (node != null)
                    {
                        results.Add(node.Value, GetValue(entry.Value));
                    }
                }

                return results;
            }

            var list = value as YamlSequenceNode;
            if (list != null)
            {
                var listResults = new List<string>();
                foreach (var entry in list.Children)
                {
                    var node = entry as YamlScalarNode;
                    if (node != null)
                    {
                        listResults.Add(node.Value);
                    }
                }
                return listResults;
            }

            bool valueBool;
            if (bool.TryParse(value.ToString(), out valueBool))
            {
                return valueBool;
            }

            return value.ToString();
        }

        public static string ToYaml<T>(this T model)
        {
            var stringWriter = new StringWriter();
            var serializer = new Serializer();
            serializer.Serialize(stringWriter, model, typeof(T));
            return stringWriter.ToString();
        }

        public static string ExcludeHeader(this string text)
        {
            var m = YamlFrontMatterRegex.Matches(text);
            if (m.Count == 0)
                return text;

            return text.Replace(m[0].Groups[0].Value, "").TrimStart('\r', '\n').TrimEnd();
        }
    }
}
