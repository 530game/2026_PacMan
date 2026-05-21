using System.Collections.Generic;
using UnityEngine;


public class GhostMoveAI : MonoBehaviour {

	private Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };    // 이동방향

	private GhostController owner;		// 본인 컨트롤러
	public void SetOwner(GhostController entity) { owner = entity; }


	// 이동 방향 결정
	public Vector3 MoveDir(Vector3 curDir, Vector3? targetPos) {
		if (targetPos != null) { return MinDir (curDir, targetPos.Value); }		// 목적지O → 최단
		else				   { return RandomDir(curDir); }					// 목적지X → 랜덤
	}


	// 최단경로
	public Vector3 MinDir(Vector3 curDir, Vector3 targetPos) {

		Vector3 newDir = -curDir;				// 새방향
		float minLen = float.MaxValue;			// 최단 거리

		// 경로 확인
		foreach (Vector3 dir in directions) {
			
			if (dir == -curDir) { continue; }					// U턴 X
			if (!owner.moveCheck.CanMove(dir)) { continue; }	// 이동불가 X

			Vector3 diff = (transform.position + dir) - targetPos;		// 다음위치와 목표 Vector 차이
			float sqrLen = diff.sqrMagnitude;							// 목표까지 거리 길이

			if (sqrLen < minLen) {			// 최단경로 → 갱신
				minLen = sqrLen;
				newDir = dir;
			}
		}

		return newDir;
	}


	// 랜덤 경로
	public Vector3 RandomDir(Vector3 curDir) {

		List<Vector3> moveDirs = new List<Vector3>();		// 새방향 후보

		foreach (Vector3 dir in directions) {
			if (dir == -curDir) { continue; }                   // U턴 X
			if (!owner.moveCheck.CanMove(dir)) { continue; }    // 이동불가 X

			moveDirs.Add(dir);		// 후보에 추가
		}


		if (moveDirs.Count > 0) {
			return moveDirs[Random.Range(0, moveDirs.Count)];	// 후보O → 랜덤
		}
		return -curDir;											// 후보X → U턴
	}
}