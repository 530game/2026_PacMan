using UnityEngine;

public class YellowTargetAI : GhostTargetAI {
	protected override Vector3 ChaseStrategy() {

		Vector3 dir = player.position - transform.position;		// 플레이어와 유령 Vector 차이
		float sqrLen = dir.sqrMagnitude;						// Vector 제곱 → 루트X 거리 길이

		// 루트X → 8^2 = 64f
		if (sqrLen >= 64f) { return player.position; }								// 8칸 초과 : 추적
		else			   { return targetPoint[(int)GhostState.Scatter](); }		// 8칸 이하 : 해산
	}
}

// 노랑유령 추적전략 : 플레이어와의 거리에 따라 추적, 해산

/*
두 점 사이의 직선거리 → 피타고라스의 정리 : a^2 + b^2 = c^2
											 = √(a^2 + b^2) = c

- Vector3.Distance		: 거리 계산		  → 루트 사용	 → 연산 느림
- Vector3.sqrMagnitude	: 거리의 제곱반환 → 루트 사용 X → 연산 빠름
*/