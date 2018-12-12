using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGM : MonoBehaviour {

    [SerializeField]
    private Bgm bgm;

    private AudioManager audioManager;

	// Use this for initialization
	void Start () {
        audioManager = AudioManager.Instance;

        string audioName = "";

        switch (bgm) {
            case Bgm.Title:
                audioName = AUDIO.BGM_TITLE;
                break;
            case Bgm.Main:
                audioName = AUDIO.BGM_MAIN;
                break;
            case Bgm.Boss:
                audioName = AUDIO.BGM_BOSS;
                break;
            case Bgm.Result:
                audioName = AUDIO.BGM_RESULT;
                break;
        }

        audioManager.PlayBGM(audioName);
	}
}
