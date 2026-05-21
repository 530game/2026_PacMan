using UnityEngine;

public class UIPlatformActive : MonoBehaviour {

	public GameObject[] mobileUI;		// 모바일용 UI
	public GameObject[] ComputerUI;		// PC용 UI


	private void Awake() {

		if (Application.isMobilePlatform								// 빌드  → 모바일인가?
			|| SystemInfo.deviceType == DeviceType.Handheld) {          // WebGL → 손에 드는 기기인가?

			SetUIActive(mobileUI, true);
			SetUIActive(ComputerUI, false);
		}
		else {															// PC
			SetUIActive(mobileUI, false);
			SetUIActive(ComputerUI, true);
		}
	}


	private void SetUIActive(GameObject[] uiArray, bool state) {

		foreach (GameObject ui in uiArray) { ui.gameObject.SetActive(state); }		// 상태에 따라 활성화 / 비활성화
	}
}