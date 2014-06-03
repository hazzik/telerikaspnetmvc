namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = args.FirstOrDefault() ??
                @"C:\Work\MVC\Trunk\Documentation\Help";
            var commands = new ICommand[]
            {
                new AddMetaTagsCommand(),
                new RenameHtmFilesToHtmlCommand(),
                new RemoveHtmlFolderFromTocCommand(),
                new UpdateHtmToHtmlInTocCommand(),
                new GenerateFriendlyUrlCommand(),
                new RenameTocFileToHxt(),
                new UpdateIconAndMediaPathCommand()
            };

            foreach (var command in commands)
            {
                command.Execute(path);
            }
        }
    }
}