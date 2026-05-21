using UnityEngine;

public enum ScoreType { HP, Ghost, Food }

public class ScoreData {

	public int[] scoreMult	= { 400, 200, 10 };		// 항목별 점수 배율
	public int[] count		= {   3,   0,  0 };		// 항목별 획득 개수

	
	// 총점 계산
	public int TotalScore() {
		int total = 0;

		for (int i = 0; i < scoreMult.Length; i++) {
			total += scoreMult[i] * count[i];
		}

		return total;
	}
}