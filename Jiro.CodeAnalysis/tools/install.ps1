param($installPath, $toolsPath, $package, $project)

$analyzerPath = join-path $toolsPath "Jiro.CodeAnalysis"
$analyzerFilePath = join-path $analyzerPath "Jiro.CodeAnalysis.dll"

$project.Object.AnalyzerReferences.Add("$analyzerFilePath")