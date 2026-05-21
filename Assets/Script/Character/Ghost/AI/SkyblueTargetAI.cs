using UnityEngine;

public class SkyblueTargetAI : GhostTargetAI {
	protected override Vector3 ChaseStrategy() {

		Vector3 point = player.position + (player.forward * 2);		// 플레이어 2칸 앞 목표
		Vector3 dir = point - pairGhost.position;                   // 빨강유령과 목표 Vector차이

		return point + dir;											// 빨강유령과 대칭 지점
	}
}

// 하늘유령 추적전략 : 플레이어 2칸 앞, 빨강유령과 대칭 지점