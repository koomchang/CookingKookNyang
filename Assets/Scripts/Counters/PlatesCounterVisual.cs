using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
	[SerializeField] private PlatesCounter platesCounter;
	[SerializeField] private Transform counterTopPoint;
	[SerializeField] private Transform plateVisualPrefab;

	private void Start() {
		platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
	}

	private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e) {
		var plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
	}
}