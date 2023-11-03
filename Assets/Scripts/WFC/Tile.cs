using UnityEngine;

[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
	[SerializeField]
	private TileWithConnectorsData _tileWithConnectors;
	private Connectors _connectors;
	private bool _isSymmetrical;
	private TypeOfSymmetry _typeOfSymmetry;
	private int _rotatePosition;
	[ExecuteInEditMode]
	[SerializeField]
	public string type = "";
	[SerializeField]
	public bool canConnectToHimselfUp = true;
	[SerializeField]
	public bool canConnectToHimselfRight = true;
	[SerializeField]
	public bool canConnectToHimselfLeft = true;
	[SerializeField]
	public bool canConnectToHimselfDown = true;


	public Connectors Connectors { get
	{		
		if(_connectors == null)
		{
			_connectors = new Connectors(_tileWithConnectors.Up, _tileWithConnectors.Right, _tileWithConnectors.Down, _tileWithConnectors.Left );	
		}
		return _connectors;
	} }

	public bool IsSymmetrical { get
	{
		if(_isSymmetrical == _tileWithConnectors.IsSymmetrical )
		{
			_isSymmetrical = _tileWithConnectors.IsSymmetrical;
		}
		return _isSymmetrical;
	} }
	internal TypeOfSymmetry TypeOfSymmetry { get
	{
		if(_typeOfSymmetry == _tileWithConnectors.TypeOfSymmetry )
		{
			_typeOfSymmetry = _tileWithConnectors.TypeOfSymmetry;
		}
		return _typeOfSymmetry;
	}}

	public Connectors[] GetConnectorsForAllRotatePositions()
	{
		Connectors[] connectorsArray = new Connectors[4];
		for(int i = 0; i<=3; i++)
		{
			RotateRight();
			connectorsArray[i] = Connectors;
		}
		return connectorsArray;
	}
	public void RotateRight(int times = 1)
	{
		bool tmp = canConnectToHimselfUp;
		canConnectToHimselfUp = canConnectToHimselfLeft;
		canConnectToHimselfLeft = canConnectToHimselfDown;
		canConnectToHimselfDown = canConnectToHimselfRight;
		canConnectToHimselfRight = tmp;
		_rotatePosition = times%4;
		Connectors.RotateRight();
	}
	public Tile Render(int rotatePosition = 0)
	{	
		Tile newTile = Instantiate(this);
		newTile.transform.Rotate(new Vector3(0, 1, 0), 90*rotatePosition);
		return newTile;
	}
}

