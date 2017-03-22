using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SuccincT.Build.Metadata
{
    internal class MetadataFilesWriter
    {
        private static readonly IList<(string project,
                                       string template,
                                       double netStdVersion,
                                       bool usePrebuild)> CsProjConfig = new List<(string, string, double, bool)>
        {
            (@"src\SuccincT", "SuccincT", 1.1, false),
            (@"src\SuccincT.JSON", "SuccincT.JSON", 1.1, false),
            (@"build\SuccincT.Build.UWP", "SuccincT", 1.4, true),
            (@"build\SuccincT.Build.JSON.UWP", "SuccincT.JSON", 1.4, true),
            (@"build\SuccincT.Build.Core", "SuccincT", 1.6, true),
            (@"build\SuccincT.Build.JSON.Core", "SuccincT.JSON", 1.6, true)
        };

        // ReSharper disable once UnusedMember.Local
        private static void Main()
        {
            var metadata = GetMetaDataFromThisProject();

            foreach (var (project, template, netStdversion, usePrebuild) in CsProjConfig)
            {
                GenerateCsProjFile(project, template, netStdversion, usePrebuild, metadata);
            }

            GenerateNuspecFile("SuccincT", "Succinc&lt;T&gt;", metadata);
            GenerateNuspecFile("SuccincT.JSON", "SuccincT.JSON", metadata);
        }

        private static IDictionary<string, string> GetMetaDataFromThisProject()
        {
            var xdoc = XDocument.Load(@"..\..\..\SuccincT.Build.Metadata.csproj");
            return (from item in xdoc.Descendants("PropertyGroup").Elements()
                    select item).ToDictionary(item => item.Name.ToString(), item => item.Value);
        }

        private static void GenerateCsProjFile(string projectPath,
                                               string templateFile,
                                               double netStandardVersion,
                                               bool usePrebuildTag,
                                               IDictionary<string, string> metadata)
        {
            var template = File.ReadAllText($@"..\..\..\{templateFile}.template");
            var prebuildTag = usePrebuildTag ? File.ReadAllText($@"..\..\..\{templateFile}.prebuild.template") : "";
            var project = Path.GetFileName(projectPath);
            var csproj = ReplaceMacrosWithValues(template,
                                                 project,
                                                 "",
                                                 templateFile,
                                                 netStandardVersion,
                                                 prebuildTag,
                                                 metadata);

            File.WriteAllText($@"..\..\..\..\..\{projectPath}\{Path.GetFileName(project)}.csproj", csproj);
        }

        private static void GenerateNuspecFile(string project, string title, IDictionary<string, string> metadata)
        {
            var template = File.ReadAllText(@"..\..\..\SuccincT.nuspec.template");
            var nuspecPartial = ReplaceMacrosWithValues(template, project, title, project, 0.0, "", metadata);
            var nuspec = nuspecPartial.Replace("%Description%", File.ReadAllText(@"..\..\..\SuccincT.description"))
                                      .Replace("%Tags%", File.ReadAllText(@"..\..\..\SuccincT.tags"))
                                      .Replace("%ReleaseNotes%", File.ReadAllText(@"..\..\..\SuccincT.releaseNotes"));

            File.WriteAllText($@"..\..\..\..\SuccincT.Build.Packager\{project}.nuspec", nuspec);
        }

        private static string ReplaceMacrosWithValues(string template,
                                                      string project,
                                                      string title,
                                                      string templateFile,
                                                      double netStandardVersion,
                                                      string rawPrebuildTag,
                                                      IDictionary<string, string> metadata)
        {
            var projectSansJson = project.Replace(".JSON", "");

            var prebuildTag = rawPrebuildTag.Replace("%Project%", project);
            return template.Replace("%NetStdVersion%", netStandardVersion.ToString())
                           .Replace("%Version%", metadata["Version"])
                           .Replace("%Template%", templateFile)
                           .Replace("%Authors%", metadata["Authors"])
                           .Replace("%Company%", metadata["Company"])
                           .Replace("%Copyright%", metadata["Copyright"])
                           .Replace("%Summary%", metadata["Description"])
                           .Replace("%RepositoryUrl%", metadata["RepositoryUrl"])
                           .Replace("%RepositoryType%", metadata["RepositoryType"])
                           .Replace("%Prebuild%", prebuildTag)
                           .Replace("%Title", title)
                           .Replace("%SuccinctProj%", $@"..\{projectSansJson}\{projectSansJson}.csproj");
        }
    }
}