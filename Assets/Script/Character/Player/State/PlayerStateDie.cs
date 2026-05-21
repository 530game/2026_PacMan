using UnityEngine;

public class PlayerStateDie : State<PlayerController> {
	protected override int index => (int)PlayerState.Die;

	public override void OnEnter(PlayerController entity) {

		// 이벤트
		EventManager.SendScoreUpdate(ScoreType.HP, -1);							// 데이터 갱신
		EventManager.SendSfxPlay(Sfx.PlayerDie);								// 오디오 재생
		EventManager.SendEffectPlay(entity.transform.position, Color.red);		// 이펙트 재생


		// 플레이어 내부
		entity.skin.ChangeColor(Color.red);                     // 색상 변경

		entity.movement.StopMove();								// 이동코루틴 정지
		entity.transform.position = entity.spawnPoint;			// 스폰포인트 이동

		entity.ChangeState((int)PlayerState.Idle);				// 대기상태 전이
	}

	public override void OnUpdate(PlayerController entity) {}

	public override void OnExit(PlayerController entity) {
		entity.skin.FadeColor(Color.white, entity.invincibleTime);		// 색상 복구
		entity.InvincibleChange();										// 부활 무적
	}
}