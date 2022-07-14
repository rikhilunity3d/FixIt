using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class AddElementsInfoplist
{
    /// <summary>
    /// Post-build processing
    /// </summary>
    /// <param name="buildTarget">Build target information</param>
    /// <param name="path">output path</param>
    [PostProcessBuild(1)]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            BuildIOS(buildTarget, path);
        }
    }

    /// <summary>
    /// Post-build processing for iOS
    /// </summary>
    /// <param name="buildTarget">Build target information</param>
    /// <param name="path">output path</param>
    public static void BuildIOS(BuildTarget buildTarget, string path)
    {
        var exportPath = new DirectoryInfo(path).FullName;
        var projectPath = new DirectoryInfo(
            Path.Combine(Path.Combine(exportPath, "Unity-iPhone.xcodeproj"), "project.pbxproj")).FullName;

        // Load the Xcode project file output by Unity
        var project = new PBXProject();
        project.ReadFromFile(projectPath);

        // Target reference in Xcode project
        var frameworks = project.GetUnityMainTargetGuid();

        // Add framework reference for ATT
        project.AddFrameworkToProject(frameworks, "AppTrackingTransparency.framework", false);

        // Rewrite Info.plist
        var plist = new PlistDocument();
        var plistPath = Path.Combine(path, "Info.plist");
        plist.ReadFromFile(plistPath);

        // Add description for ATT
        plist.root.SetString("NSUserTrackingUsageDescription", "The above information will be used to display ads and analyze usage.");

        // Write back to Xcode project file
        plist.WriteToFile(plistPath);
        project.WriteToFile(projectPath);
    }
}