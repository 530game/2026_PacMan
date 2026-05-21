using UnityEngine;

public class PlayerStateMove : State<PlayerController> {
	protected override int index => (int)PlayerState.Move;

	public override void OnEnter(PlayerController entity) {
		entity.animator.SetInteger("State", index);					// 애니메이션 재생
	}

	public override void OnUpdate(PlayerController entity) {
		if (!entity.movement.isMove) {									// 이동 중X (코루틴 종료)
			entity.moveCheck.TunnelMove();								// 터널이동 확인

			if (!entity.moveCheck.CanMove(entity.moveDir)) {			// 이동불가
				entity.ChangeState((int)PlayerState.Idle);				// 대기상태 전이
				return;
			}
			entity.movement.GridMove(entity.moveDir, entity.moveTime);	// 이동
		}
	}

	public override void OnExit(PlayerController entity) { }
}