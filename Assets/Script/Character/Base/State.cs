using UnityEngine;

public abstract class State<T> where T : class {
	protected abstract int index { get; }			// enum 번호

	public abstract void OnEnter(T entity);         // 시작 시 1회 실행
	public abstract void OnUpdate(T entity);        // 매 프레임 호출
	public abstract void OnExit(T entity);          // 종료 시 1회 실행
}



/*
Generic <T>
	- 데이터 형식지정X → 사용시점에 지정
	- 일반화 프로그래밍 → 재사용↑

MonoBehaviour 사용X
	- 순수로직 → 오브젝트X
	- 경량화 / 상속의 자유
	- Scene에 존재하지 않아도 OK
 */