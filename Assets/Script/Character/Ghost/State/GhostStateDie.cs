using UnityEngine;

public class GhostStateDie : GhostBaseState {
	protected override int index => (int)GhostState.Die;

	public override void OnEnter(GhostController entity) {

		// 이벤트
		EventManager.SendScoreUpdate(ScoreType.Ghost, +1);						// 데이터 갱신
		EventManager.SendSfxPlay(Sfx.GhostDie);									// 오디오 재생
		EventManager.SendEffectPlay(entity.transform.position, Color.blue);     // 이펙트 재생

		// 고스트 내부
		entity.col.enabled = false;                                 // 콜라이더 끄기
		entity.skin.ChangeColor(entity.data.GhostColor[index]);		// 색상 변경
	}


	public override void OnUpdate(GhostController entity) {
		if (!entity.movement.isMove) {      // 이동 중X (코루틴 종료)

			var spawnPoint = entity.targetAI.targetPoint[index]?.Invoke();
			if (entity.transform.position == spawnPoint) {                  // 시작장소 복귀

				if (entity.isScary) { entity.ChangeState((int)GhostState.Scary); }		// 공포상태 전이
				else				{ entity.ChangeState((int)GhostState.Chase); }		// 추적상태 전이
				return;
			}

			OnMove(entity, 0.5f);           // 이동
		}
	}


	public override void OnExit(GhostController entity) {
		entity.col.enabled = true;									// 콜라이더 복구
		entity.skin.ChangeColor(entity.data.GhostColor[0]);			// 색상 복구
	}
}

// 죽음: 집으로 이동함