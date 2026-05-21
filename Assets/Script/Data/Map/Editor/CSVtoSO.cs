using System.IO;
using UnityEngine;
using UnityEditor;

public class CSVtoSO {

	private static string loadPath =	   "/Script/Data/Map/MapDataCSV.csv";		// 읽기 경로
	private static string savePath = "Assets/Script/Data/Map/MapData.asset";		// 저장 경로

	[MenuItem("Tools/ConvertMapData")]        // 유니티 메뉴 생성

	public static void Convert() {

		// 1. CSV 읽기
		string[] lines = File.ReadAllLines(Application.dataPath + loadPath);	// 전체  - "\n" 단위로 자르기
		string[] split = lines[0].Split(",");									// 한 줄 -  "," 단위로 자르기

		int height = lines.Length;
		int width = split.Length;


		// 2. SO 인스턴스 생성
		MapDataSO mapData = ScriptableObject.CreateInstance<MapDataSO>();
		mapData.rows = new MapDataSO.MapRow[height];								// Row 초기화

		for (int y = 0; y < height; y++) {
			mapData.rows[y] = new MapDataSO.MapRow { cols = new int[width] };		// Col 초기화

			// 3. 한 줄씩 처리
			split = lines[y].Split(",");							// 한 줄 -  "," 단위로 자르기

			for (int x = 0; x < width; x++) {
				mapData.rows[y].cols[x] = int.Parse(split[x]);		// 문자 → 숫자 변환 후 저장
			}
		}

		mapData.height = height;
		mapData.width = width;


		// 4. 파일로 저장
		AssetDatabase.CreateAsset(mapData, savePath);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		Debug.Log("CSV → SO 변환 성공");
	}
}





/*
* UnityEditor 빌드오류 주의
	1. Editor/ 폴더 안에 배치
	2. #if UNITY_EDITOR ~ #endif로 감싸기

* 직접변환+
	- 같은타입 여러파일: 읽어올 폴더의 모든 CSV 읽고 저장
	- 다른타입 여러파일: 제네릭사용

* 라이브러리 사용
	- unity excel importer
	- https://github.com/mikito/unity-excel-importer
 */