using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour {
	void Start() {
		KitchenGameManager.Instance.OnStateChanged += KitchenGameManager_OnStateChanged;
		Show();
	}

	private void KitchenGameManager_OnStateChanged(object sender, EventArgs e) {
		if (KitchenGameManager.Instance.IsCountdownToStartActive()) {
			Hide();
		}
	}

	private void Show() {
		gameObject.SetActive(true);
	}

	private void Hide() {
		gameObject.SetActive(false);
	}
}