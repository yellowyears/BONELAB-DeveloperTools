using MelonLoader;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(DeveloperTools.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(DeveloperTools.BuildInfo.Company)]
[assembly: AssemblyProduct(DeveloperTools.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + DeveloperTools.BuildInfo.Author)]
[assembly: AssemblyTrademark(DeveloperTools.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(DeveloperTools.BuildInfo.Version)]
[assembly: AssemblyFileVersion(DeveloperTools.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(DeveloperTools.Main), DeveloperTools.BuildInfo.Name, DeveloperTools.BuildInfo.Version, DeveloperTools.BuildInfo.Author, DeveloperTools.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]