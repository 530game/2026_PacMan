using UnityEngine;

public enum GamePhase { Chase, Scatter, PowerUp }	// 게임페이즈 FSM

public class GameManager : MonoBehaviour {

	private GamePhase phase;                        // 게임페이즈 관리

	public UIView uiView;							// UI 표시
	private MapManager mapManager;					// 맵 생성

	private ScoreData scoreData;					// 점수 데이터
	private int maxFood;

	private float playTime = 0.0f;					// 플레이 시간
	private float phaseTimer = 0.0f;				// 게임페이즈 타이머
	private float phaseChangeTime = 8.0f;			// 게임페이즈 전환시간


	// 이벤트 구독/해제
	private void OnEnable() {
		EventManager.OnGamePhaseChange += GamePhaseChange;
		EventManager.OnScoreUpdate += ScoreUpdate;
	}
	private void OnDisable() {
		EventManager.OnGamePhaseChange -= GamePhaseChange;
		EventManager.OnScoreUpdate -= ScoreUpdate;
	}


	private void Awake() {
		mapManager = GetComponent<MapManager>();
		maxFood = mapManager.MapSpawn();
		scoreData = new ScoreData();

		phase = GamePhase.Chase;
	}
	private void Start() {
		GamePhaseChange(phase);
	}


	private void Update() {
		playTime += Time.deltaTime;
		phaseTimer += Time.deltaTime;

		if (phaseTimer >= phaseChangeTime) {														// 전환시간 경과
			GamePhaseChange((phase == GamePhase.Chase) ? GamePhase.Scatter : GamePhase.Chase);		// 게임페이즈 전환
		}
	}


	// 게임페이즈 변경
	private void GamePhaseChange(GamePhase newPhase) {
		phaseTimer = 0.0f;
		phase = newPhase;

		EventManager.SendEntityPhaseChange(phase);      // 엔티티 상태 변경

		if		(phase == GamePhase.Chase)	 { EventManager.SendBgmPlay(Bgm.Normal); }		// 배경음 변경
		else if (phase == GamePhase.PowerUp) { EventManager.SendBgmPlay(Bgm.PowerUp); }

		// 페이즈 종료 예고
	}

	// 공포 페이즈 종료 예고


	// 점수 갱신
	private void ScoreUpdate(ScoreType type, int value) {
		scoreData.count[(int)type] += value;
		
		uiView.ScoreUIChange(scoreData.TotalScore());										// UI 갱신
		if (type == ScoreType.HP) { uiView.HPBarChange(scoreData.count[(int)type]); }

		if		(type == ScoreType.Food && scoreData.count[(int)type] == maxFood) { GameResult(true); }		// 클리어 판정
		else if (type == ScoreType.HP   && scoreData.count[(int)type] <= 0)		  { GameResult(false); }	// 게임오버 판정
	}


	// 게임 결과 
	private void GameResult(bool isClear) {
		Time.timeScale = 0;
		EventManager.SendBgmPlay(Bgm.Result);
		uiView.ResultUIShow(isClear, playTime, scoreData);
	}
}