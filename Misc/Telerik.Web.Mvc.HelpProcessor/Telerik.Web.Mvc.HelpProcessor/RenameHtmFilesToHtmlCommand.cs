namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.IO;

    class RenameHtmFilesToHtmlCommand : ICommand
    {
        public void Execute(string path)
        {
            var htmFiles = Directory.GetFiles(path, "*.htm", SearchOption.AllDirectories);

            foreach (var fileName in htmFiles)
            {
                var newFileName = Path.ChangeExtension(fileName, "html");
                File.Move(fileName, newFileName);
                FileUtils.ReplaceInFile(newFileName, ".htm", ".html");
            }
        }
    }
}
