﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MamaController : MonoBehaviour
{

    private GameObject winText;
    private GameObject UIController;
    
    void Start() {
        winText = GameObject.Find("WinText");
        winText.SetActive(false);

        UIController = GameObject.Find("UIController");
    }
    void OnTriggerEnter2D(Collider2D c) {
        if (c.gameObject.tag == "Player") {
            winText.SetActive(true);
            StartCoroutine("PauseBeforeFadeOut");
        }
    }

    IEnumerator PauseBeforeFadeOut() {
        yield return new WaitForSeconds(3f);
        UIController.GetComponent<UIController>().FadeOutToEnd(3f);
    }
}
