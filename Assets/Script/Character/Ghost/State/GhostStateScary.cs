using UnityEngine;

public class GhostStateScary : GhostBaseState {
	protected override int index => (int)GhostState.Scary;

	public override void OnEnter(GhostController entity) {
		entity.moveDir = -entity.moveDir;                       // U턴
		entity.skin.ChangeColor(entity.data.GhostColor[index]);	// 색상 변경
	}

	public override void OnUpdate(GhostController entity) {
		if (!entity.movement.isMove) {      // 이동 중X (코루틴 종료)
			OnMove(entity, 1.5f);			// 이동
		}
	}

	public override void OnExit(GhostController entity) {
		entity.skin.ChangeColor(entity.data.GhostColor[0]);		// 색상 복구
	}
}

// 공포: 랜덤한 방향으로 이동함