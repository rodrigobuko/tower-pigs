using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {
    public string clipName;

    public void PlayButtonSound() {
        AudioManager.instance.Play(clipName);
    }
}
