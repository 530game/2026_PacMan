using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour {

	private PlayerController player;


	private void Awake() {
		player = GetComponent<PlayerController>();
	}


	// 이벤트 입력
	public void OnMove(InputValue value) {
		Vector2 vec = value.Get<Vector2>();
		if (vec == Vector2.zero) return;        // 방향 0 방지

		Vector3 newDir = Vector3.zero;

		if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y)) { newDir.x = (vec.x > 0) ? 1 : -1; }   // 대각선 이동X
		else { newDir.z = (vec.y > 0) ? 1 : -1; }

		player.moveDir = newDir;
	}
}