using UnityEngine;

public class WFC : MonoBehaviour
{

	[SerializeField]
	private int width = 5;
	[SerializeField]
	private int height = 5;
	[SerializeField]
	private TileSet tiles;
	private Grid _grid;
	public bool  isGenerated;
	void SpawnTile(Cell cell)
	{
		if(cell!=null)
		{
			Tile tile = cell.Variants[0];
			Tile newTile = tile.Render();
			newTile.transform.position = new Vector3(1+cell.coordinates.x*2, 0, 1+cell.coordinates.y*2);
			newTile.name = $"Tile {cell.coordinates.x }, {cell.coordinates.y}";
			newTile.transform.parent = this.transform;
		}

	}
	public void GenerateAll()
	{
		Cell[,] cells = _grid.Collapse();
		for(int w = 0; w< _grid.Width; w++){
		for(int h = 0; h< _grid.Height; h++)
		{
			Cell cell = cells[w, h];
			SpawnTile(cell);
			isGenerated = !_grid.GetIsCollapsed();
		}
		}
	}
	public void GenerateNext()
	{
		Cell cell = _grid.CollapseNextOrReturnNull();
		SpawnTile(cell);
		isGenerated = !_grid.GetIsCollapsed();
	}
	public void Reset()
	{
		int count = this.transform.childCount;
		for(int i = 0; i<count; i++)
		{
			Destroy(this.transform.GetChild(i).gameObject);
		}
		_grid = new Grid(width, height, tiles.tiles);
		isGenerated = _grid.GetIsCollapsed();
	}
	void Start()
	{	
		_grid = new Grid(width, height, tiles.tiles);

	}
}
