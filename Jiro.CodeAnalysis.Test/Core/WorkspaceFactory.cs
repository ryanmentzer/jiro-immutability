namespace Jiro.CodeAnalysis.Core
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using System.Linq;

    internal static class WorkspaceFactory
    {
        public static Workspace Create(string code)
        {
            var projectId = ProjectId.CreateNewId();

            var projectInfo =
                ProjectInfo.Create(
                    projectId,
                    VersionStamp.Default,
                    "TestProject",
                    "TestAssembly",
                    LanguageNames.CSharp,
                    parseOptions: new CSharpParseOptions(LanguageVersion.Experimental));

            var documentId = DocumentId.CreateNewId(projectId);

            using (var workspace = new CustomWorkspace())
            {
                var solution =
                    workspace
                        .CurrentSolution
                        .AddProject(projectInfo)
                        .AddMetadataReference(projectId, AssemblyMetadata.CreateFromFile(typeof(object).Assembly.Location).GetReference(display: "mscorlib"))
                        .AddDocument(documentId, "TestDocument.cs", code);

                return
                    new Workspace(
                        solution,
                        solution.Projects.Single(),
                        solution.GetDocument(documentId));
            }
        }
    }
}