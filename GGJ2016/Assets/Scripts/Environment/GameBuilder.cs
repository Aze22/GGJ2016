using UnityEngine;
using System.Collections;

public class GameBuilder : MonoBehaviour
{
	public GameObject floorPrefab;
	public GameObject wallPrefab;
	public GameObject doorWallPrefab;

	private const int MAX_ROOMS = 25;
	private GameObject[] ground = new GameObject[MAX_ROOMS];
	private GameObject[] wall = new GameObject[2 * MAX_ROOMS];
	private GameObject[] doorWall = new GameObject[2 * MAX_ROOMS];
	private bool[,] exists = new bool[MAX_ROOMS,MAX_ROOMS];
	private int mCount = 0;

	void Awake () {
		int xIndex, zIndex;

		for (xIndex = 0; xIndex < MAX_ROOMS; xIndex++) {
			for (zIndex = 0; zIndex < MAX_ROOMS; zIndex++) {
				exists[zIndex,xIndex] = false;
			}
		}

		BuildLevel(10, 10, 10);
	}

	void BuildLevel(int xIndex, int zIndex, int maxDepth) {
		bool left = false;
		bool right = false;

		if ((mCount < MAX_ROOMS) && (maxDepth > 0)) {
			// Create a floor
			ground[mCount] = GameObject.Instantiate(floorPrefab, new Vector3(10f * (float)xIndex, 0f, 10f * (float)zIndex), Quaternion.identity) as GameObject;

			// Create a top-left wall
			if (Random.Range(0, 2) == 0) {
				wall[mCount * 2] = GameObject.Instantiate(wallPrefab, new Vector3(0f + (10f * (float)xIndex), 2f, 5f + (10f * (float)zIndex)), Quaternion.Euler(0f, 90f, 0f)) as GameObject;
			} else {
				doorWall[mCount * 2] = GameObject.Instantiate(doorWallPrefab, new Vector3(0f + (10f * (float)xIndex), 0f, 10f + (10f * (float)zIndex)), Quaternion.Euler(0f, 90f, 0f)) as GameObject;
				left = true;
			}

			// Create a top-right wall
			if (Random.Range(0, 2) == 0) {
				wall[(mCount * 2) + 1] = GameObject.Instantiate(wallPrefab, new Vector3(5f + (10f * (float)xIndex), 2f, 10f * (float)zIndex), Quaternion.identity) as GameObject;
			} else {
				doorWall[(mCount * 2) + 1] = GameObject.Instantiate(doorWallPrefab, new Vector3(0f + (10f * (float)xIndex), 0f, 10f * (float)zIndex), Quaternion.identity) as GameObject;
				right = true;
			}

			// Increment the count of rooms so far
			mCount++;

			// Maybe create a top-left room
			if ((left) && (zIndex + 1 < MAX_ROOMS) && (!exists[zIndex + 1, xIndex])) {
				BuildLevel(xIndex, zIndex + 1, Random.Range(0, maxDepth));
			}

			// Maybe create a top-right room
			if ((right) && (xIndex + 1 < MAX_ROOMS) && (!exists[zIndex, xIndex + 1])) {
				BuildLevel(xIndex + 1, zIndex, Random.Range(0, maxDepth));
			}

			// Maybe create a bottom-left room
			if ((xIndex - 1 > 0) && (!exists[zIndex, xIndex - 1])) {
				BuildLevel(xIndex - 1, zIndex, Random.Range(0, maxDepth + 1));
			}

			// Maybe create a bottom-right room
			if ((zIndex - 1 > 0) && (!exists[zIndex - 1, xIndex])) {
				BuildLevel(xIndex, zIndex - 1, 1 + Random.Range(0, 2 * maxDepth));
			}
		}
	}

	void Start () {
	}
}