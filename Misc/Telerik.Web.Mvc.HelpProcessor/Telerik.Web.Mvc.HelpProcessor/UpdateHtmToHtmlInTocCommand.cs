namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.IO;

    internal class UpdateHtmToHtmlInTocCommand : ICommand
    {
        public void Execute(string path)
        {
            FileUtils.ReplaceInFile(Path.Combine(path, Constants.TokFile), ".htm", ".html");
        }
    }
}