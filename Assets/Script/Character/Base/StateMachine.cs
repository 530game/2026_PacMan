using UnityEngine;

public class StateMachine<T> where T : class {

	private T owner;                // 소유주

	private State<T> curState;					// 현재상태
	public State<T> CurState => curState;		// 현재상태 접근용 (Get O, Set X)


	// 초기화
	public void Setup(T entity, State<T> startState) {
		owner = entity;
		curState = startState;
	}


	// 실행
	public void Execute() {
		curState?.OnUpdate(owner);                      // 현재상태 실행
	}


	// 상태변경
	public void ChangeState(State<T> newState) {
		curState?.OnExit(owner);                        // 이전상태 종료
		curState = newState;                            // 현재상태 변경
		curState.OnEnter(owner);
	}

}