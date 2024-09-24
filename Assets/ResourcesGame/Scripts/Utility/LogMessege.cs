using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LogMessege : MonoBehaviour
{
	static LogMessege _instance;

	static public bool isActive
	{
		get
		{
			return _instance != null;
		}
	}

	static public LogMessege instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = Object.FindObjectOfType(typeof(LogMessege)) as LogMessege;

				if (_instance == null)
				{
					GameObject go = new GameObject("LogMessege");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<LogMessege>();
				}
			}
			return _instance;
		}
	}

	[SerializeField]
	private TextMeshProUGUI LogoText;

	public GameObject windowl;
    private void Start()
    {
		//this.windowl.SetActive(false);
	}
    public void Messege(string messeg)
	{
		LogoText.text += "\n" + messeg;
	}

	public void CloseWindow()
    {
		this.windowl.SetActive(false);
		SandController.instance.ResumePlayerGame();

	}
	public void OpenWindow()
	{
		this.windowl.SetActive(true);

	}
	public void ClearText()
	{
		LogoText.text = "";
	}
}
