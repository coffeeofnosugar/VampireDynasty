//-----------------------------------------------------------------------
// <summary>
// 文件名: NewBehaviourScript
// 描述: #DESCRIPTION#
// 作者: #AUTHOR#
// 创建日期: #CREATIONDATE#
// 修改记录: #MODIFICATIONHISTORY#
// </summary>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

using static System.Environment;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;

public static class ProjectSetUp
{
	[MenuItem("Tools/SetUp/Import Essential Assets")]
	static void ImportEssentials()
	{
#if !UNITY_6000
		Assets.ImportAsset("Rainbow Folders 2.unitypackage", "Borodar/Editor ExtensionsSystem");
#endif
		Assets.ImportAsset("vHierarchy 2.unitypackage", "kubacho lab/Editor ExtensionsUtilities");
		Assets.ImportAsset("Easy Save - The Complete Save Data Serializer System.unitypackage", "Moodkie/Editor ExtensionsUtilities");
		Assets.ImportAsset("Inspector Gadgets Lite.unitypackage", "Kybernetik/Editor ExtensionsGUI");
		
		// Assets.ImportAsset("DOTween Pro.unitypackage", "Demigiant/Editor ExtensionsVisual Scripting");
		// Assets.ImportAsset("DOTween HOTween v2.unitypackage", "Demigiant/Editor ExtensionsAnimation");
		Assets.ImportAsset("PrimeTween High-Performance Animations and Sequences.unitypackage", "Kyrylo Kuzyk/Editor ExtensionsAnimation");
		Assets.ImportAsset("Odin Inspector and Serializer.unitypackage", "Sirenix/Editor ExtensionsSystem");
		Assets.ImportAsset("Hot Reload Edit Code Without Compiling.unitypackage", "The Naughty Cult/Editor ExtensionsUtilities");
	}
	
#if !UNITY_6000
	[MenuItem("Tools/SetUp/Install Essential Packages")]
	public static void InstallPackages()
	{
		Packages.InstallPackages(new[] {
			"https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
			"https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity",
			"https://github.com/Cysharp/R3.git?path=src/R3.Unity/Assets/R3.Unity",
			
			"com.unity.inputsystem"		// 最后再InputSystem，因为它需要重启Unity
		});
	}
#endif
	
	[MenuItem("Tools/SetUp/Create Folders")]
	public static void CreateFolders()
	{
		Folders.Create("Animation", "Art", "AssetsPacks", "Editor/Tools", "Materials", "Plugins", "Prefabs", "ScriptableObjects", "Scripts", "Settings/PipelineSettings", "Shaders", "Volume");
		Refresh();

		if (EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode2D)
		{
			Folders.Create("Settings/TemplateScene");
#if UNITY_6000
			MoveAsset("Assets/DefaultVolumeProfile.asset", "Assets/Volume/DefaultVolumeProfile.asset");
#endif
			
			MoveAsset("Assets/UniversalRenderPipelineGlobalSettings.asset", "Assets/Settings/PipelineSettings/UniversalRenderPipelineGlobalSettings.asset");
			MoveAsset("Assets/Settings/Scenes/URP2DSceneTemplate.unity", "Assets/Settings/TemplateScene/URP2DSceneTemplate.unity");
			MoveAsset("Assets/Settings/Lit2DSceneTemplate.scenetemplate", "Assets/Settings/TemplateScene/Lit2DSceneTemplate.scenetemplate");
			Refresh();
			
			Folders.Delete("Assets/Settings/Scenes");
			Refresh();
		}
		else if (EditorSettings.defaultBehaviorMode == EditorBehaviorMode.Mode3D)
		{
#if UNITY_6000
			Folders.Delete("Assets/TutorialInfo");
			Refresh();
			
			DeleteAsset("Assets/Readme.asset");
			MoveAsset("Assets/Settings/DefaultVolumeProfile.asset", "Assets/Volume/DefaultVolumeProfile.asset");
			MoveAsset("Assets/Settings/SampleSceneProfile.asset", "Assets/Volume/SampleSceneProfile.asset");
			Refresh();
#endif
		}
		
		Directory.GetFiles("Assets/Settings").Where(a => !a.EndsWith(".meta")).ToList()
			.ForEach(a => MoveAsset(a, $"Assets/Settings/PipelineSettings/{a.Split('\\')[^1]}"));
		Refresh();

		Folders.Move("Assets/vHierarchy", "Assets/Plugins/vHierarchy");
		MoveAsset("Assets/Resources/DOTweenSettings.asset", "Assets/Settings/DOTweenSettings.asset");
    
#if UNITY_6000
		MoveAsset("Assets/InputSystem_Actions.inputactions", "Assets/Settings/InputSystem/InputSystem_Actions.inputactions");
		Refresh();
		EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload | EnterPlayModeOptions.DisableSceneReload;
#else
		EditorSettings.enterPlayModeOptionsEnabled = true;
		EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;
#endif
	}
	
	/// <summary>
	/// 从"C:/Users/username/AppData/Roaming/Unity/Asset Store-5.x"下导入unitypackage资源包
	/// </summary>
	static class Assets
	{
		public static void ImportAsset(string asset, string folder)
		{
			string basePath = GetFolderPath(SpecialFolder.ApplicationData);
			string assetsFolder = Combine(basePath, "Unity/Asset Store-5.x");	// C:/Users/<username>/AppData/Roaming/Unity/Asset Store-5.x
			
			asset = asset.EndsWith(".unitypackage") ? asset : asset + ".unitypackage";
			
			string fullPath = Combine(assetsFolder, folder, asset);

			if (!File.Exists(fullPath))
				throw new FileNotFoundException($"The asset package was not found at the path: {fullPath}");
			
			ImportPackage(fullPath, false);
			ImportPackage(Combine(assetsFolder, folder, asset), false);
		}
	}

	/// <summary>
	/// 从PackageManager的UnityRegistry或github中导入资源包
	/// </summary>
	static class Packages
	{
		private static AddRequest _request;
		private static Queue<string> _packagesToInstall = new Queue<string>();

		public static void InstallPackages(string[] packages)
		{
			foreach (var package in packages)
			{
				_packagesToInstall.Enqueue(package);
			}

			if (_packagesToInstall.Count > 0)
				StartNextPackageInstallation();
		}

		static async void StartNextPackageInstallation()
		{
			_request = Client.Add(_packagesToInstall.Dequeue());

			while (!_request.IsCompleted) await Task.Delay(10);
			
			if (_request.Status == StatusCode.Success) Debug.Log("Installed: " + _request.Result.packageId);
			else if (_request.Status >= StatusCode.Failure) Debug.LogError(_request.Error.message);
			
			if (_packagesToInstall.Count > 0)
			{
				await Task.Delay(1000);
				StartNextPackageInstallation();
			}
		}
	}

	/// <summary>
	/// 整理项目文件
	/// </summary>
	static class Folders
	{
		public static void Create(params string[] folders)
		{
			var rootPath = Application.dataPath;
			
			foreach (var folder in folders)
			{
				CreateFolders(rootPath, folder);
			}
		}
		
		static void CreateFolders(string rootPath, string folderHierarchy)
		{
			var currentPath = rootPath;
			var folders = folderHierarchy.Split('/');

			foreach (var folder in folders)
			{
				currentPath = Combine(currentPath, folder);
				
				if (!Directory.Exists(currentPath))
					Directory.CreateDirectory(currentPath);
			}
		}
		
		public static void Move(string folderName, string newPath)
		{
			if (IsValidFolder(folderName))
			{
				var error = MoveAsset(folderName, newPath);

				if (!string.IsNullOrEmpty(error))
					Debug.LogError($"Failed to move {folderName}: {error}");
			}
		}

		public static void Delete(string folderName)
		{
			if (IsValidFolder(folderName))
				DeleteAsset(folderName);
		}
		
		public static void Rename(string folderName, string newName)
		{
			if (IsValidFolder(folderName))
			{
				var error = RenameAsset(folderName, newName);
				
				if (!string.IsNullOrEmpty(error))
					Debug.LogError($"Failed to rename {folderName}: {error}");
			}
		}
	}
}
