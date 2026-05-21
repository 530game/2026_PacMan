using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour {
	public bool isMove { get; private set; } = false;       // 현재 이동중


	// 이동
	public void GridMove(Vector3 dir, float moveTime) {
		if (isMove) { return; }
		isMove = true;

		transform.rotation = Quaternion.LookRotation(dir);      // 회전
		StartCoroutine(CoGridMove(dir, moveTime));				// 이동
	}

	// 이동 코루틴
	private IEnumerator CoGridMove(Vector3 dir, float moveTime) {
		Vector3 startPos = transform.position;			// 시작위치
		Vector3 endPos	 = transform.position + dir;	// 종료위치
		float percent = 0.0f;							// 진행도

		while (percent < 1) {
			percent += Time.deltaTime / moveTime;                               // 진행도 = 누적프레임시간 / 전체이동시간
			transform.position = Vector3.Lerp(startPos, endPos, percent);       // 선형이동 Lerp(시작위치, 종료위치, 비율)

			yield return null;
		}
		transform.position = endPos;		// 소수점 방지
		
		isMove = false;
	}


	// 이동 코루틴 강제종료
	public void StopMove() {
		StopAllCoroutines();		// 클래스 내 모든 코루틴 종료
		isMove = false;
	}
}