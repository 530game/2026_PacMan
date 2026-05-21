using System.Collections;
using UnityEngine;

public class Skin : MonoBehaviour {

	public SkinnedMeshRenderer smr;
	public MaterialPropertyBlock mpv;

	public bool isChange { get; private set; } = false;		// 코루틴 확인

	private void Awake() {
		smr = GetComponentInChildren<SkinnedMeshRenderer>();
		mpv = new MaterialPropertyBlock();
	}


	private Color ReturnNowColor() {                // 현재 색상반환
		smr.GetPropertyBlock(mpv); 
		return mpv.GetColor("_BaseColor");
	}

	private void SetColor(Color newColor) {         // 색상 설정
		smr.GetPropertyBlock(mpv);
		mpv.SetColor("_BaseColor", newColor);
		smr.SetPropertyBlock(mpv);
	}

	public void StopColor() {                       // 모든 코루틴 종료
		StopAllCoroutines();
		isChange = false;
	}



	// 즉시 변경
	public void ChangeColor(Color newColor) {
		if (isChange) { StopColor(); }              // 코루틴 실행중 → 종료
		SetColor(newColor);
	}


	// 부드럽게 변경
	public void FadeColor(Color newColor, float time) {
		if (isChange) { StopColor(); }
		isChange = true;

		StartCoroutine(CoFadeColor(newColor, time));
	}
	private IEnumerator CoFadeColor(Color newColor, float time) {
		Color nowColor = ReturnNowColor();
		float percent = 0.0f;

		while (percent < 1) {
			percent += Time.deltaTime / time;								// 진행도
			Color lerpColor = Color.Lerp(nowColor, newColor, percent);      // 선형변경 Lerp

			SetColor(lerpColor);	// 중간색상
			yield return null;
		}
		SetColor(newColor);         // 최종색상
		isChange = false;
	}
}