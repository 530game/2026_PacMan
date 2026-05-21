using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour {

	public void Play() {						// Play 씬 이동
		Time.timeScale = 1;
		SceneManager.LoadScene("Play");
	}

	public void Main() {						// Main 씬 이동
		Time.timeScale = 1;
		SceneManager.LoadScene("Main");
	}

	public void Pause(GameObject panel) {		// 일시정지
		Time.timeScale = 0;
		panel.SetActive(true);
	}

	public void UnPause(GameObject panel) {		// 일시정지 해제
		Time.timeScale = 1;
		panel.SetActive(false);
	}

	public void Exit() {                        // 게임 종료
		Debug.Log("종료");
		Application.Quit();
	}
}
