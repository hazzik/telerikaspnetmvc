namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.IO;

    internal class UpdateIconAndMediaPathCommand : ICommand
    {
        public void Execute(string path)
        {
            foreach (var file in Directory.GetFiles(path, "*.html", SearchOption.AllDirectories))
            {
                FileUtils.ReplaceInFile(file, "../icons", "icons");
                FileUtils.ReplaceInFile(file, "../media", "media");
            }
        }
    }
}