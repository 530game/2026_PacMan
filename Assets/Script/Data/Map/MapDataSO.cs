using UnityEngine;
// 생성: Edtior → CSVtoSO

public class MapDataSO : ScriptableObject {

	[System.Serializable]							// 직렬화 → 2차원 배열
	public class MapRow { public int[] cols; }		// 열
	public MapRow[] rows;                           // 행

	public int width;		// col크기 = 가로 길이 = 너비
	public int height;      // row크기 = 세로 길이 = 높이
}