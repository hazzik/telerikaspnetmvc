namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    internal class GenerateFriendlyUrlCommand : ICommand
    {
        public void Execute(string path)
        {
            var tocFileName = Path.Combine(path, Constants.TokFile);

            var guidTopics = GetTopicsWhoseNameIsGuid(tocFileName);

            GenerateFriendlyNameForGuidTopics(Path.Combine(path, "html"), guidTopics);
            UpdateToc(tocFileName, guidTopics);
        }

        private static void UpdateToc(string tocFileName, IEnumerable<Topic> guidTopics)
        {
            foreach (var topic in guidTopics)
            {
                FileUtils.ReplaceInFile(tocFileName, topic.Id, topic.FriendlyUrl);
            }
        }
        
        private static void GenerateFriendlyNameForGuidTopics(string path, IEnumerable<Topic> guidTopics)
        {
            foreach (var file in Directory.GetFiles(path, "*.html"))
            {
                FileUtils.ReplaceInFile(file, guidTopics);
                if (IsGuid(Path.GetFileNameWithoutExtension(file)))
                {
                    var matchingTopic = guidTopics.Where(t => t.Id == Path.GetFileName(file))
                        .Select(t => t.FriendlyUrl)
                        .FirstOrDefault();
                    if (matchingTopic != null)
                        File.Move(file, Path.Combine(Path.GetDirectoryName(file), matchingTopic));
                }
            }
        }

        private static IEnumerable<Topic> GetTopicsWhoseNameIsGuid(string tocFileName)
        {
            var toc = XDocument.Load(tocFileName);

            return from topic in toc.Root.Descendants("HelpTOCNode")
                   where IsGuid(topic.Attribute("Url"))
                   select new Topic
                   {
                       Id = topic.Attribute("Url").Value,
                       FriendlyUrl = CreateFriendlyUrl(topic)
                   };
        }

        private static string CreateFriendlyUrl(XElement topic)
        {
            var result = new StringBuilder();
            var topicsRelevantForUrlGeneration = topic.AncestorsAndSelf("HelpTOCNode");
            foreach (var element in topicsRelevantForUrlGeneration.Reverse())
            {
                result.AppendFormat(" {0}", element.Attribute("Title").Value);
            }
            return result.ToString().Trim().Replace(" ", "-").Replace("/", "-") + ".html";
        }


        public static bool IsGuid(XAttribute urlAttribute)
        {
            if (urlAttribute == null) return false;
            if (string.IsNullOrEmpty(urlAttribute.Value)) return false;

            return IsGuid(urlAttribute.Value.Replace(".html", ""));
        }

        private static bool IsGuid(string input)
        {
            return Regex.IsMatch(input,
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
        }
    }
}