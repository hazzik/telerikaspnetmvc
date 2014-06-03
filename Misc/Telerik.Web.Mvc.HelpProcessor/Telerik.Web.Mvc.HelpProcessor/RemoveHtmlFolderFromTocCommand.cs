namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.IO;

    class RemoveHtmlFolderFromTocCommand : ICommand
    {
        public void Execute(string path)
        {
            FileUtils.ReplaceInFile(Path.Combine(path, Constants.TokFile), "html/", "");
        }
    }
}
