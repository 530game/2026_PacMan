using UnityEngine;

public enum GhostType { Null, Red, Pink, Skyblue, Yellow }

[CreateAssetMenu(fileName = "GhostDataSO", menuName = "Scriptable Objects/GhostDataSO")]
public class GhostDataSO : ScriptableObject {

	public GhostType type;				// 유령 타입
	public Color[] GhostColor;			// 유령 색상
	public Vector3 ScatterTarget;		// 해산 위치 부호벡터

	public GhostType pairGhost;			// 협력 유령
}