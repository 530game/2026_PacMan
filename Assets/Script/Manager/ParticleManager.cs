using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	public int effectCount;
	public GameObject effectPrefab;
	private ParticleSystem[] effect;

	private void Awake() {

		effect = new ParticleSystem[effectCount];               // 이펙트 생성
		for (int i = 0; i < effectCount; i++) {
			GameObject obj = Instantiate(effectPrefab);
			effect[i] = obj.GetComponent<ParticleSystem>();
		}
	}

	private void OnEnable()  { EventManager.OnEffectPlay += EffectPlay; }
	private void OnDisable() { EventManager.OnEffectPlay -= EffectPlay; }


	// 이펙트 재생
	private void EffectPlay(Vector3 pos, Color color) {

		for (int i = 0; i < effectCount; i++) {
			if (effect[i].isPlaying && effect[i].particleCount > 0) { continue; }       // 사용중인 이펙트 패스

			// 이펙트 세팅 → 재생
			effect[i].transform.position = pos;		// 위치
			var mainModule = effect[i].main;		// 색상
			mainModule.startColor = color;

			effect[i].Play();
			return;
		}
	}
}
