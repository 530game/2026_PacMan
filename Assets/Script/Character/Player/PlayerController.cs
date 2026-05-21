using System.Collections;
using UnityEngine;

public enum PlayerState { Idle, Move, Die }     // 캐릭터 FSM

public class PlayerController : BaseController<PlayerController> {

	public Vector3 spawnPoint;					// 스폰포인트

	private bool isPowerUp = false;				// 파워업
	private bool isInvincible = false;          // 부활무적
	public float invincibleTime = 1.0f;			// 부활무적 시간


	protected override void Awake() {
		base.Awake();
		spawnPoint = transform.position;

		states = new State<PlayerController>[3];					// 상태 설정
		states[(int)PlayerState.Idle] = new PlayerStateIdle();
		states[(int)PlayerState.Move] = new PlayerStateMove();
		states[(int)PlayerState.Die]  = new PlayerStateDie();

		stateMachine.Setup(this, states[(int)PlayerState.Idle]);
	}
	// Update, ChangeState : BaseController → StateMachine


	// 충돌 → 이벤트
	private void OnTriggerEnter(Collider other) {

		switch (other.tag) {
			case "Power":                                                   // 파워음식 → 게임페이즈 변경
				EventManager.SendGamePhaseChange(GamePhase.PowerUp);
				goto case "Food";
			case "Food":													// 음식 → 점수+
				EventManager.SendScoreUpdate(ScoreType.Food, 1);
				EventManager.SendSfxPlay(Sfx.Food);
				Destroy(other.gameObject);
				break;
			
			case "Ghost":
				if (isInvincible) { break;  }												// 부활무적 → 처리X

				if (!isPowerUp) { ChangeState((int)PlayerState.Die); }						// 파워업X → 플레이어 죽음
				else { other.GetComponent<GhostController>().OnTriggerEatenPlayer(); }      // 파워업O → 고스트 죽음
				break;
		}
	}


	// 게임 페이즈 → 파워업
	protected override void PhaseChange(GamePhase newPhase) {
		isPowerUp = (newPhase == GamePhase.PowerUp);
	}

	// 죽은 뒤 부활 → 일시무적
	public void InvincibleChange() {
		StartCoroutine(CoInvincibleChange());
	}
	private IEnumerator CoInvincibleChange() {
		isInvincible = true;
		yield return new WaitForSeconds(invincibleTime);
		isInvincible = false;
	}
}