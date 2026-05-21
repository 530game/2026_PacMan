using UnityEngine;

public enum Bgm { Normal, PowerUp, Result }				// Bgm 열거
public enum Sfx { Buttion, Food, PlayerDie, GhostDie }		// Sfx 열거

public class AudioManager : MonoBehaviour {

	[Header("Bgm")]
	private AudioSource bgmSource;
	public AudioClip[] bgmClip;
	public float bgmVolume;

	[Header("Sfx")]
	private AudioSource sfxSource;
	public AudioClip[] sfxClip;
	public float sfxVolume;


	private void Awake() {
		GameObject audio = new GameObject("Audio");				// 오디오 오브젝트
		audio.transform.parent = transform;

		bgmSource = audio.AddComponent<AudioSource>();			// 배경음
		bgmSource.volume = bgmVolume;
		bgmSource.loop = true;
		PlayBgm(Bgm.Normal);

		sfxSource = audio.AddComponent<AudioSource>();			// 효과음
		sfxSource.volume = sfxVolume;
		sfxSource.playOnAwake = false;
	}


	// 이벤트 구독/해제
	private void OnEnable() {
		EventManager.OnBgmPlay += PlayBgm;
		EventManager.OnSfxPlay += PlaySfx;
	}
	private void OnDisable() {
		EventManager.OnBgmPlay -= PlayBgm;
		EventManager.OnSfxPlay -= PlaySfx;
	}


	// 배경음 재생
	private void PlayBgm(Bgm bgm) {
		if (bgmSource.clip == bgmClip[(int)bgm]) { return; }	// 똑같은 배경음 재생 → 패스

		bgmSource.clip = bgmClip[(int)bgm];
		bgmSource.Play();
	}

	// 효과음 재생
	private void PlaySfx(Sfx sfx) {
		sfxSource.PlayOneShot(sfxClip[(int)sfx]);
	}

	// 버튼 효과음 재생
	public void PlayButtion(int index) => PlaySfx((Sfx)index);
}
