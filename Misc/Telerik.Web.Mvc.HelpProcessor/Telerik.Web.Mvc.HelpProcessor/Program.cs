namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.Linq;
	using System.Diagnostics;
	using System;

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

            Stopwatch watch = new Stopwatch();
            foreach (var command in commands)
            {
                watch.Reset();
                watch.Start();
                command.Execute(path);
                watch.Stop();

                Console.WriteLine(command.ToString() + " took " + watch.ElapsedMilliseconds + "ms");
            }
        }
    }
}