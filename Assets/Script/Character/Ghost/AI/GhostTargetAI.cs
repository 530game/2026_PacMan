using System;
using UnityEngine;

public abstract class GhostTargetAI : MonoBehaviour {
	protected MapDataSO map => MapManager.Map;		// 전역 Map데이터 프로퍼티

	protected Transform pairGhost;					// 협력: 유령 위치
	protected Transform player;						// 추적: 플레이어 위치

	public Func<Vector3>[] targetPoint = new Func<Vector3>[4];    // State별 목표위치함수 배열


	public void SetTarget(GhostDataSO data) {

		// 다른 오브젝트 찾기
		if (data.pairGhost != GhostType.Null) {										// 협력 유령 (이름)
			pairGhost = GameObject.Find(data.pairGhost.ToString()).transform;
		}
		player = GameObject.FindWithTag("Player").transform;                        // 플레이어 (태그)


		// 목표위치 설정												*  공포: 랜덤 → NULL
		targetPoint[(int)GhostState.Chase] = ChaseStrategy;				// 추적: 유령별 전략

		Vector3 scatterPoint = Scatter(data);
		targetPoint[(int)GhostState.Scatter] = () => scatterPoint;		// 해산: 유령별 맵 모서리

		Vector3 spawnPoint = transform.position;
		targetPoint[(int)GhostState.Die]	 = () => spawnPoint;		// 죽음: 시작위치
	}


	protected abstract Vector3 ChaseStrategy();					// 추적
	
	public Vector3 Scatter(GhostDataSO data) {					// 해산
		Vector3 pos = Vector3.zero;
		pos.x = (data.ScatterTarget.x < 0) ? 1 : map.width  - 2;
		pos.z = (data.ScatterTarget.z > 0) ? 1 : -(map.height - 2);

		return pos;
	}
}





/* 삼항연산자 설명
if (data.pairGhost != GhostType.Null) { pairGhostPos = GameObject.Find(data.pairGhost.ToString()).transform;  }
pairGhostPos = (data.pairGhost == GhostType.Null) ? null : GameObject.Find(data.pairGhost.ToString()).transform;
*/

/*
Func<반환값>[] : 함수 실행 → 값 반환 배열

* => : 람다식
	 * 람다식 매개변수 들어가면 GC 걸림

* Data Type
	- Vector3	= 복사 값 → 자동 갱신X
	- Transform	= 참조 값 → 자동 갱신O
 */