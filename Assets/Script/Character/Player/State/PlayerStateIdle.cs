using UnityEngine;

public class PlayerStateIdle : State<PlayerController> {
	protected override int index => (int)PlayerState.Idle;

	public override void OnEnter(PlayerController entity) {
		entity.animator.SetInteger("State", index);				// 애니메이션 재생
	}

	public override void OnUpdate(PlayerController entity) {
		if (entity.moveCheck.CanMove(entity.moveDir)) {         // 이동가능
			entity.ChangeState((int)PlayerState.Move);			// 이동상태 전이
		}
	}

	public override void OnExit(PlayerController entity) { }
}