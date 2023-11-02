using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "TileWithConnectors", menuName = "Tiles", order = 1)]
public class TileWithConnectorsData: ScriptableObject
{
	[SerializeField]
	private bool _isSymmetrical;
	[SerializeField]
	private TypeOfSymmetry _typeOfSymmetry;
	[SerializeField]
	private string[] _up = new string[0];

	[SerializeField]
	private string[] _left = new string[0];

	[SerializeField]
	private string[] _right = new string[0];

	[SerializeField]
	private string[] _down = new string[0];
	[SerializeField]

	public bool IsSymmetrical{get{ return _isSymmetrical; }}

	public string[] Up { get => _up; }
	public string[] Left { get => _left; }
	public string[] Right { get => _right;  }
	public string[] Down { get => _down; }
	internal TypeOfSymmetry TypeOfSymmetry { get => _typeOfSymmetry;}
}