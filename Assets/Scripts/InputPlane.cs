﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlane : MonoBehaviour
{//as seen on https://www.youtube.com/watch?v=I1BocNFIkwI
	private Camera mainCam;
	private RaycastHit hitInfo;
	public Transform cubePrefab;

	private void Awake()
	{
		mainCam = Camera.main;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
			{
				Color c = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
				ICommand command = new PlaceCubeCommand(hitInfo.point, c, cubePrefab);
				CommandInvoker.AddCommand(command);
			}
		}
	}
}
