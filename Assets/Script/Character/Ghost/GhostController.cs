using UnityEngine;

public enum GhostState { Chase, Scatter, Scary, Die }		// 고스트 FSM

public class GhostController : BaseController<GhostController> {

	public CapsuleCollider col;			// 콜라이더

	public GhostDataSO data;			// 유령 데이터
	public GhostMoveAI moveAI;          // 이동 AI
	public GhostTargetAI targetAI;      // 타겟 AI

	public bool isScary;				// 공포상태 (부활 후 판단)


	protected override void Awake() {
		base.Awake();
		col = GetComponent<CapsuleCollider>();
		moveAI = GetComponent<GhostMoveAI>();

		states = new State<GhostController>[4];                         // FSM 설정
		states[(int)GhostState.Chase]	= new GhostStateChase();
		states[(int)GhostState.Scatter] = new GhostStateScatter();
		states[(int)GhostState.Scary]	= new GhostStateScary();
		states[(int)GhostState.Die]		= new GhostStateDie();
		
		stateMachine.Setup(this, states[(int)GhostState.Chase]);
	}

	private void Start() {
		switch (data.type) {
			case GhostType.Red:		targetAI = gameObject.AddComponent<RedTargetAI>();		break;
			case GhostType.Pink:	targetAI = gameObject.AddComponent<PinkTargetAI>();		break;
			case GhostType.Skyblue: targetAI = gameObject.AddComponent<SkyblueTargetAI>();	break;
			case GhostType.Yellow:	targetAI = gameObject.AddComponent<YellowTargetAI>();	break;
		}
		targetAI.SetTarget(data);                                   // 유령별 타겟AI 설정
		moveAI.SetOwner(this);                                      // 유령별 이동AI 설정

		skin.ChangeColor(data.GhostColor[(int)GhostState.Chase]);      // 색상 설정
	}
	// data관련 설정: Start 처리 (호출순서)
	// Update, ChangeState : BaseController → StateMachine


	// FSM변경
	public void OnTriggerEatenPlayer() {		// 플레이어 → 충돌 죽음
		ChangeState((int)GhostState.Die);
	}

	protected override void PhaseChange(GamePhase newPhase) {
		if (stateMachine.CurState != states[(int)GhostState.Die]) { ChangeState((int)newPhase); }       // 게임매니저 → 상태변경(죽은상태X)
		isScary = (stateMachine.CurState == states[(int)GhostState.Scary]);
	}
}