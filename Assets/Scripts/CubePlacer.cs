using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CubePlacer
{
	public static List<Transform> cubes;

	public static Transform PlaceCube(Vector3 position, Color color, Transform cube)
	{
		Transform tmpC = Resources.Load<Transform>("Cube");
		Transform newCube = GameObject.Instantiate(tmpC, position, Quaternion.identity);//GameObject.Instantiate(cube, position, Quaternion.identity);
		newCube.GetComponentInChildren<MeshRenderer>().material.color = color;
		if(cubes == null)
		{
			cubes = new List<Transform>();
		}
		cubes.Add(newCube);
		newCube.gameObject.name = newCube.gameObject.name.Replace("(Clone)",""); ;
		newCube.gameObject.SetActive(true);
		return newCube;
	}

	
	public static void RemoveCube(GameObject cube)
	{
		cubes.Remove(cube.transform);		
		GameObject.Destroy(cube);		
	}
}
