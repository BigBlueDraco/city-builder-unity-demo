using UnityEditor.Build;
using UnityEngine;

public class Tile : MonoBehaviour
{
	private int rotationPosition; 
	[SerializeField]
	public Tile[] up = new Tile[0];
	[SerializeField]
	public Tile[] left = new Tile[0];
	[SerializeField]
	public Tile[] right = new Tile[0];
	[SerializeField]
	public Tile[] down = new Tile[0];
	[SerializeField]
	public string type = "";

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	public void RotateLeft(int times = 1)
	{
	}
	public void RotateRight(int times = 1)
	{

	}
}
