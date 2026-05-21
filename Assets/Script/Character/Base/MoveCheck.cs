using UnityEngine;

public class MoveCheck : MonoBehaviour {
	private MapDataSO map => MapManager.Map;				// 전역 Map데이터 프로퍼티


	// 이동 여부
	public bool CanMove(Vector3 dir) {
		try {
			Vector3 nextPos = transform.position + dir;     // 이동위치

			int x = Mathf.RoundToInt(nextPos.x);            // 월드좌표 → 배열인덱스
			int z = Mathf.RoundToInt(-nextPos.z);

			return map.rows[z].cols[x] != 1;						// 벽이 아니면 이동가능
		}
		catch (System.IndexOutOfRangeException) { return true; }	// 배열 벗어남 = 터널구간 → 이동가능
	}


	// 터널 이동
	public void TunnelMove() {
		Vector3 pos = transform.position;

		// 맵 끝 → 반대편으로 이동
		if		(pos.x < 1)				{ pos.x = map.width - 1; }
		else if (pos.x > map.width - 2) { pos.x = 0; }

		if		(pos.z < -(map.height - 2)) { pos.z = 0; }
		else if (pos.z > 1)					{ pos.z = -(map.height - 1); }

		transform.position = pos;
	}
}