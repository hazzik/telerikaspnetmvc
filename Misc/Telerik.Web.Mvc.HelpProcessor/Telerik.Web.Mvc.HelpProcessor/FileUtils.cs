namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.Collections.Generic;
    using System.IO;

    public static class FileUtils
    {
        public static void ReplaceInFile(string path, string source, string destination)
        {
            var contents = File.ReadAllText(path);
            contents = contents.Replace(source, destination);
            File.WriteAllText(path, contents);
        }

        public static void ReplaceInFile(string path, IEnumerable<Topic> topics)
        {
            var contents = File.ReadAllText(path);
            
            foreach (var topic in topics)
            {
                contents = contents.Replace(topic.Id, topic.FriendlyUrl);    
            }
            
            File.WriteAllText(path, contents);
        }
    }
}
