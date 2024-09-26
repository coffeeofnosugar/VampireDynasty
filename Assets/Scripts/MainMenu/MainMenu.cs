//-----------------------------------------------------------------------
// <summary>
// 文件名: MainMenu
// 描述: #DESCRIPTION#
// 作者: #AUTHOR#
// 创建日期: #CREATIONDATE#
// 修改记录: #MODIFICATIONHISTORY#
// </summary>
//-----------------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;
using R3;
using UnityEngine.SceneManagement;

namespace VampireDynasty
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private Button startButton;
		[SerializeField] private Button continueButton;
		[SerializeField] private Button settingButton;
		[SerializeField] private Button exitButton;

		private void Awake()
		{
			startButton.OnClickAsObservable().Subscribe(_ => StartGame());
			continueButton.OnClickAsObservable().Subscribe(_ => ContinueGame());
			settingButton.OnClickAsObservable().Subscribe(_ => Setting());
			exitButton.OnClickAsObservable().Subscribe(_ => ExitGame());
		}
		
		private void StartGame()
		{
			SceneManager.LoadScene(1);
		}

		private void ContinueGame()
		{
			
		}

		private void Setting()
		{
			
		}

		private void ExitGame()
		{
			
		}
	}
}