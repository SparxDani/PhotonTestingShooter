using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour {

	public TextMeshProUGUI fpstext;
	FPSCounter fpsCounter;

	void Awake () {
		fpsCounter = GetComponent<FPSCounter>();
	}

	void Update () {
		fpstext.text = fpsCounter.FPS.ToString(System.Globalization.CultureInfo.InvariantCulture);
	}
}