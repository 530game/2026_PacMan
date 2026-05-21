using System;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;


public static class EventManager {

	// 플레이 데이터 변경
	public static event Action<ScoreType, int> OnScoreUpdate;
	public static void SendScoreUpdate(ScoreType type, int value) => OnScoreUpdate?.Invoke(type, value);


	// 게임 페이즈 변경
	public static event Action<GamePhase> OnGamePhaseChange;
	public static void SendGamePhaseChange(GamePhase phase) => OnGamePhaseChange?.Invoke(phase);

	// 게임 페이즈 → 엔티티 상태 변경
	public static event Action<GamePhase> OnEntityPhaseChange;
	public static void SendEntityPhaseChange(GamePhase phase) => OnEntityPhaseChange?.Invoke(phase);


	// 배경음 재생
	public static event Action<Bgm> OnBgmPlay;
	public static void SendBgmPlay(Bgm bgm) => OnBgmPlay?.Invoke(bgm);

	// 효과음 재생
	public static event Action<Sfx> OnSfxPlay;
	public static void SendSfxPlay(Sfx sfx) => OnSfxPlay?.Invoke(sfx);


	// 이펙트 재생
	public static event Action<Vector3, UnityEngine.Color> OnEffectPlay;
	public static void SendEffectPlay(Vector3 pos, UnityEngine.Color color) => OnEffectPlay?.Invoke(pos, color);
}