
using System.CommandLine;

Option<string> yearOption = new("--year", "-y") { Required = true };
Option<string> dayOption = new("--day", "-d") { Required = true };

RootCommand root = new();
root.Options.Add(yearOption);
root.Options.Add(dayOption);

root.SetAction(p =>
{
    var folderName = $"AdventOfCode{p.GetRequiredValue(yearOption)}";
    var solutionName = $"{folderName}.slnx";

    var day = NormalizeDay(p.GetRequiredValue(dayOption));

    GenerateProject(folderName, day);
    UpdateSolutionFile(folderName, solutionName, day);
});

return root.Parse(args).Invoke();



static string NormalizeDay(string day) => day.Length == 1 ? $"0{day}" : day;

static void GenerateProject(string solutionFolderName, string day)
{
    Directory.CreateDirectory($"{solutionFolderName}\\Day{day}");

    File.WriteAllText($"{solutionFolderName}\\Day{day}\\Day{day}.csproj", GenerateProjectFileContent());
    File.WriteAllText($"{solutionFolderName}\\Day{day}\\Day{day}.cs", GenerateProgramContent());
    File.Create($"{solutionFolderName}\\Day{day}\\input.txt").Dispose();
}

static void UpdateSolutionFile(string folderName, string solutionName, string day)
{
    var solutionFilePath = $"{folderName}\\{solutionName}";
    var solutionLines = File.ReadAllLines(solutionFilePath);

    var output = new string[solutionLines.Length + 1];

    var index = solutionLines.IndexOf("</Solution>");

    Array.Copy(solutionLines, output, index);
    output[index] = GenerateSolutionFileContent(day);
    output[index + 1] = "</Solution>";

    File.WriteAllLines(solutionFilePath, output);
}

static string GenerateSolutionFileContent(string day) =>
    @$"  <Project Path=""Day{day}/Day{day}.csproj"" Id=""{Guid.NewGuid()}"" />";

static string GenerateProjectFileContent() =>
@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include=""..\Common\Common.csproj"" />
  </ItemGroup>

  <ItemGroup>
    <None Update=""input.txt"">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </None>
  </ItemGroup>

</Project>";

static string GenerateProgramContent() =>
@"using Common;

var lines = Helpers.ReadInputLines();


";
