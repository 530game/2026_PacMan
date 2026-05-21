using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour {

	[Header("체력UI")]
	public Slider hpSlider;

	[Header("점수UI")]
	public TMP_Text scoreText;

	[Header("결과UI")]
	public Sprite[] TitleImage;

	public GameObject resultPanel;
	public Image	  resultImage;
	public TMP_Text	  resultText;
	
	public TMP_Text[] scoresText;

	public TMP_Text totalText;
	public TMP_Text timeText;


	// 플레이 UI
	public void HPBarChange(int hp) {
		hpSlider.value = hp;
	}
	public void ScoreUIChange(int score) {
		scoreText.text = score.ToString("N0");
	}


	// 결과 UI
	public void ResultUIShow(bool isClear, float time, ScoreData score) {

		resultPanel.SetActive(true);							// 결과UI 활성화

		resultImage.sprite = TitleImage[isClear ? 0 : 1];		// 결과타이틀 변경
		resultText.text    = isClear ? "Clear!" : "Fail";

		for (int i = 0; i < score.scoreMult.Length; i++) {
			scoresText[i].text = $"{score.count[i]} * {score.scoreMult[i]}";		// 점수항목 
		}
		
		totalText.text = score.TotalScore().ToString("N0");							// 최종집계
		timeText.text  = string.Format("{0:00}:{1:00}", time / 60, time % 60);
	}
}
