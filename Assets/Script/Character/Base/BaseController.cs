using UnityEngine;


public class BaseController<T> : MonoBehaviour where T : MonoBehaviour {

	public Animator  animator;
	public Skin		 skin;
	public Movement  movement;
	public MoveCheck moveCheck;

	protected StateMachine<T> stateMachine;         // 상태 관리머신
	protected State<T>[] states;                    // 상태 배열

	public Vector3 moveDir = Vector3.right;			// 이동방향
	public float moveTime = 0.2f;                   // 이동시간 (한 칸 단위)


	// 이벤트 구독/해제
	private void OnEnable()  { EventManager.OnEntityPhaseChange += PhaseChange; }
	private void OnDisable() { EventManager.OnEntityPhaseChange -= PhaseChange; }


	// 공통설정
	protected virtual void Awake() {
		animator  = GetComponent<Animator>();
		skin	  = GetComponent<Skin>();
		movement  = GetComponent<Movement>();
		moveCheck = GetComponent<MoveCheck>();

		stateMachine = new StateMachine<T>();
	}


	// FSM
	protected void Update() {
		stateMachine?.Execute();		// 현재상태 실행
	}

	public void ChangeState(int stateIndex) {
		stateMachine.ChangeState(states[stateIndex]);				// 본인내부 → 상태변경
	}

	protected virtual void PhaseChange(GamePhase newPhase) {}		// 게임페이즈 → 상태변경
}