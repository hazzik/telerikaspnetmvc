namespace Telerik.Web.Mvc.HelpProcessor
{
    using System.IO;

    class AddMetaTagsCommand : ICommand
    {
        public void Execute(string path)
        {
            foreach (var htmlFile in Directory.GetFiles(Path.Combine(path, "html"), "*.htm"))
            {
                var contents = File.ReadAllText(htmlFile);
                contents = contents.Replace(@"<META NAME=""save"" CONTENT=""history"" />",
    @"<META NAME=""save"" CONTENT=""history"" />" +
    @"<meta name=""ResourceType"" content=""Documentation"" />" +
    @"<meta name=""ParentProductId"" content=""697"" />" +
    @"<meta name=""ProductId"" content=""697"" />"
);

                File.WriteAllText(htmlFile, contents);
            }
        }
    }
}
