using UnityEngine;
using UnityEngine.UI;

public class WFC : MonoBehaviour
{

	[SerializeField]
	private int width = 5;
	[SerializeField]
	private int height = 5;
	[SerializeField]
	private TileSet tiles;
	[SerializeField]
	private Tile error;
	private Grid _grid;
	public bool  isGenerated;
	void SpawnTile(Cell cell)
	{
		if(cell!=null)
		{
			Tile tile = cell.GetVariants()[0];
			Tile newTile = Instantiate(tile);
			newTile.transform.position = new Vector3(1+cell.cordinats.x*2, 0, 1+cell.cordinats.y*2);
			newTile.name = $"Tile {cell.cordinats.x }, {cell.cordinats.y}";
			newTile.transform.parent = this.transform;

		}

	}
	public void GenerateAll()
	{
		Cell[,] cells = _grid.Colabse();
		for(int w = 0; w< _grid.Width; w++){
		for(int h = 0; h< _grid.Height; h++)
		{
			Cell cell = cells[w, h];
			SpawnTile(cell);
			isGenerated = !_grid.GetIsColabsed();
		}
		}
	}
	public void GenerateNext()
	{
		Cell cell = _grid.ColabseNextOrReturnNull();
		SpawnTile(cell);
		isGenerated = !_grid.GetIsColabsed();
	}
	public void Reset()
	{
		int count = this.transform.childCount;
		for(int i = 0; i<count; i++)
		{
			Destroy(this.transform.GetChild(i).gameObject);
		}
		_grid = new Grid(width, height, tiles.tiles);
		isGenerated = _grid.GetIsColabsed();
	}
	void Start()
	{	
		_grid = new Grid(width, height, tiles.tiles);

	}
}
