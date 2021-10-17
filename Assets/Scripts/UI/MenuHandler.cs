using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    private void Start() {
        AudioManager.instance.Stop("Game");
        AudioManager.instance.Play("Menu");
    }

    // Start is called before the first frame update
    public void ChangeToGameScene() {
        SceneManager.LoadScene("JOGO");
    }
}
