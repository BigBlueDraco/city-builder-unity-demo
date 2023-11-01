using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
	[SerializeField]
	private bool isSymmetrical;
	private int rotationPosition;
	[SerializeField]
	private TypeOfSymmetry typeOfSymmetry;
	[SerializeField]
	public string[] test;
	[SerializeField]
	public Tile[] up = new Tile[3];

	[SerializeField]
	public Tile[] left = new Tile[0];

	[SerializeField]
	public Tile[] right = new Tile[0];

	[SerializeField]
	public Tile[] down = new Tile[0];
	[ExecuteInEditMode]
	[SerializeField]
	public string type = "";
	public void RotateLeft(int times = 1)
	{
	}
	public void RotateRight(int times = 1)
	{

	}
}
