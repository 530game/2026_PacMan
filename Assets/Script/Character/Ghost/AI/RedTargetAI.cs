using UnityEngine;

public class RedTargetAI : GhostTargetAI {
	protected override Vector3 ChaseStrategy() {
		return player.position;
	}
}

// 빨간유령 추격전략: 플레이어 위치