using UnityEngine;

public abstract class GhostBaseState : State<GhostController> {

	protected void OnMove(GhostController entity, float speedMult = 1.0f) {
		entity.moveCheck.TunnelMove();												// 터널이동 확인

		var targetPos = entity.targetAI.targetPoint[index]?.Invoke();
		entity.moveDir = entity.moveAI.MoveDir(entity.moveDir, targetPos);			// 이동방향 설정

		entity.movement.GridMove(entity.moveDir, entity.moveTime * speedMult);		// 이동
	}
}