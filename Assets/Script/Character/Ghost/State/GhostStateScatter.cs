using UnityEngine;

public class GhostStateScatter : GhostBaseState {
	protected override int index => (int)GhostState.Scatter;

	public override void OnEnter(GhostController entity) {
		entity.moveDir = -entity.moveDir;                       // U턴
	}

	public override void OnUpdate(GhostController entity) {
		if (!entity.movement.isMove) {      // 이동 중X (코루틴 종료)
			OnMove(entity);                 // 이동
		}
	}

	public override void OnExit(GhostController entity) { }
}

// 해산: 지정된 모서리로 이동함