using UnityEngine;

public class PinkTargetAI : GhostTargetAI {
	protected override Vector3 ChaseStrategy() {
		return player.position + (player.forward * 4);
	}
}

// 핑크유령 추격전략: 플레이어 4칸 앞