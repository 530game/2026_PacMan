using UnityEngine;


public class MapManager : MonoBehaviour {
	public MapDataSO map;                                   // 맵 SO데이터
	public static MapDataSO Map { get; private set; }		// 맵 데이터 전역접근

	public GameObject tileRoot;			// 지형 그룹
	public GameObject foodRoot;         // 음식 그룹
	public GameObject[] entity;         // 생성할 프리팹
	public GhostDataSO[] ghostSO;       // 생성할 고스트데이터 연결


	public void Awake() { Map = map; }


	// 맵 생성
	public int MapSpawn() {
		Quaternion rot = Quaternion.identity;       // 회전값

		// 오브젝트 생성
		for (int z = 0; z < map.height; z++) {
			for (int x = 0; x < map.width; x++) {

				int index = map.rows[z].cols[x];			// 1. 인덱스 확인
				if (index == 0) { continue; }

				Vector3 pos = new Vector3(x, 0, -z);		// 2. 생성좌표 설정

				// 3. 생성
				if		(index == 1) { Instantiate(entity[index], pos, rot, tileRoot.transform); }		// 벽
				else if (index <= 3) { Instantiate(entity[index], pos, rot, foodRoot.transform); }		// 음식들
				else if (index == 4) { Instantiate(entity[index], pos, rot); }							// 플레이어
				else if (index >= 5) { GhostSpawn (index, pos, rot); }									// 귀신들
			}
		}
		GameObject ground = Instantiate(entity[0], new Vector3(0, 0, 0), rot, tileRoot.transform);		// 바닥
		ground.transform.localScale = new Vector3(map.width, 1, map.height);


		// 위치 보정
		float centerX = (map.width - 1) / 2f;
		float centerZ = -(map.height - 1) / 2f;

		ground.transform.localPosition   = new Vector3(centerX, -0.75f, centerZ);
		tileRoot.transform.localPosition = new Vector3(0, 0.25f, 0);
		foodRoot.transform.localPosition = new Vector3(0, 0.3f, 0);
		Camera.main.transform.position   = new Vector3(centerX, 27.5f, centerZ - 27.5f - 2);


		StaticBatchingUtility.Combine(tileRoot.gameObject);     // 최적화
		return foodRoot.transform.childCount;                   // 맵 정보 → 게임 매니저
	}


	// 유령 생성 & 설정
	private void GhostSpawn(int index, Vector3 pos, Quaternion rot) {
		
		GameObject ghost = Instantiate(entity[5], pos, rot);                // 유령 스폰

		ghost.name = ghostSO[index - 5].type.ToString();					// 유령 이름 설정
		ghost.GetComponent<GhostController>().data = ghostSO[index - 5];	// 유령 데이터 연결
	}
}





/*
Static Batching Utility . Combine: 정적 일괄처리 도구 . 결합
	- 정적 오브젝트 합침 → Draw Call횟수 감소 → 성능향상
	* 머티리얼이 같아야 가능

	* Draw Call: CPU → GPU 화면출력 명령 횟수
		- Draw Call많음 → overhead 증가 → 병목현상 → 성능저하

		* 오버헤드: 작업 전 준비시간
		* 병목현상: 성능이 느린곳에 맞춰짐
		
		* CPU (중앙 처리장치)	: 전체 계산		- 성능 승부: 복잡한 연산, 적은코어
		* GPU (그래픽 처리장치) : 그래픽 계산	- 물량 승부: 간단한 연산, 많은코어
*/