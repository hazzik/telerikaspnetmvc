namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.IO;

    class RenameTocFileToHxt : ICommand
    {
        public void Execute(string path)
        {
            var tocPath = Path.Combine(path, Constants.TokFile);
            File.Move(tocPath, Path.ChangeExtension(tocPath, "hxt"));
        }
    }
}
