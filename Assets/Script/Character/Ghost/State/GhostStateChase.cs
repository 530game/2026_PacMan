using Unity.VisualScripting;
using UnityEngine;

public class GhostStateChase : GhostBaseState {
	protected override int index => (int)GhostState.Chase;

	public override void OnEnter(GhostController entity) {
		entity.moveDir = -entity.moveDir;						// U턴
	}

	public override void OnUpdate(GhostController entity) {
		if (!entity.movement.isMove) {      // 이동 중X (코루틴 종료)
			OnMove(entity);					// 이동
		}
	}

	public override void OnExit(GhostController entity) { }
}

// 추격: 팩맨을 쫓음