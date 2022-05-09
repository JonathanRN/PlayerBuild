#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
//using UnityEditor.AddressableAssets.Settings;
using UnityEditor.Build.Reporting;
#endif
using UnityEngine;

public class PlayerBuild
{
#if UNITY_EDITOR
    private static void Build(BuildTarget target)
    {
        // Uncomment this line and its using statement to also build Addressables
        //AddressableAssetSettings.BuildPlayerContent();

        string filename = $"{Application.productName}_v{Application.version}";
        string foldername = $"{target}Build";
	
		BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
		{
			// Grab all the scenes from the build player settings
			scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray(),
			target = target,
			options = BuildOptions.None
		};
	
        string extension = EditorUserBuildSettings.buildAppBundle ? ".aab" : ".apk";
		if (target == BuildTarget.Android)
        {
            buildPlayerOptions.locationPathName = $"{foldername}/{filename}{extension}";
        }
        else
        {
            buildPlayerOptions.locationPathName = $"{foldername}/{filename}";
        }

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        switch (summary.result)
        {
            case BuildResult.Succeeded:
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
                break;
            default:
                Debug.Log("Build failed");
                break;
        }
    }

    public static void BuildAndroid()
    {
        Build(BuildTarget.Android);
    }

    public static void BuildiOS()
    {
        Build(BuildTarget.iOS);
    }
	
	public static void BuildStandaloneWindows()
	{
		Build(BuildTarget.StandaloneWindows);
	}
	
	public static void BuildStandaloneOSX()
	{
		Build(BuildTarget.StandaloneOSX);
	}
#endif
}