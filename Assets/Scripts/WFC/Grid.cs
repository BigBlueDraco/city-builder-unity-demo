using System.Collections.Generic;
using System.Linq;
using Random = System.Random;
using Debug = UnityEngine.Debug;


public class Coordinates
{
	public int x;
	public int y;
	public Coordinates(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}
public class Cell
{
	private Random rand = new Random();
	public Coordinates coordinates;
	public bool isCollapsed; 
	private Tile[] _variants;
	public Tile[] Variants
	{
		get { return _variants; }
		set {		
			HashSet<Tile> newVariants = new HashSet<Tile>();
			foreach(Tile variant in _variants)
			{
				foreach(Tile tile in value)
				{
					if(variant.type == tile.type)
					{
						newVariants.Add(tile);				
					}
				}
			}
			_variants = newVariants.ToArray();}
	}
	public Cell(Coordinates coordinates, Tile[] variants)
	{
		_variants= variants.ToArray();
		this.coordinates = coordinates;
		this.isCollapsed = false;
	}
	public void CollapseCell()
	{
		
		int indx = rand.Next(0, _variants.Length);
		if(_variants.Length ==0 )
		{
			Debug.Log(_variants);
		}
		Debug.Log(indx);
		Tile tile = _variants[indx];
		Tile[] variant = new Tile[1];
		variant[0] = tile;
		this.Variants = variant;
		this.isCollapsed = true;
	}
}

public class Grid
{
	private Random rand = new Random();
	
	private int _width;
	private int _height;
	private Cell[,] _cells;
	private bool _isCollapsed;
	private int _collapsedCellCount;
	private void CrateGrid()
	{
		_cells = new Cell[_width, _height];
	}
	public int Width
	{
		get { return _width; }
	}
	public int Height
	{
		get { return _height; }
	}
	public bool GetIsCollapsed()
	{
		return _isCollapsed;
	}
	private void FillGrid(Tile[] tileSet)
	{
		for(int w = 0; w<= _width-1; w++){
		for(int h = 0; h<= _height-1; h++)
		{
			_cells[w,h] = new Cell(new Coordinates(w,h), tileSet);
		}}
	}
	public Grid(int width, int height, Tile[] tileSet)
	{
		_width = width;
		_height = height;
		CrateGrid();
		FillGrid(tileSet);
		_isCollapsed = false;
	}
	public Cell GetCell(int x, int y)
	{
		return _cells[x, y];
	}
	public Cell GetCell(Coordinates coordinates)
	{
		return _cells[coordinates.x, coordinates.y];
	}
	private Cell RandomUncollapsedCell()
	{
		Cell randCell = _cells[rand.Next(0, _width),rand.Next(0, _height)];
		while(randCell.isCollapsed && !_isCollapsed)
		{
			randCell = _cells[rand.Next(0, _width),rand.Next(0, _height)];
		}
		return randCell;
	}
	private Cell FindCellWithLowEntropy()
	{
		Cell lowEntropyCell = RandomUncollapsedCell();
		for(int h = 0; h<= _height-1; h++){
		for(int w= 0; w<= _width-1; w++)
		{
			Cell cell = _cells[w,h];
			if(cell.Variants.Length < lowEntropyCell.Variants.Length&&!cell.isCollapsed&&cell.coordinates != lowEntropyCell.coordinates)
			{
				lowEntropyCell = _cells[w,h];
			};
			
		}};
		return lowEntropyCell;
	}
	public void ChangeVariantsToNeighborhood(Cell cell)
	{
		Coordinates coordinates = cell.coordinates;
		Tile tile = cell.Variants[0];
		if(tile ==null)
		{
			Debug.LogError($"Tile variants null {cell.coordinates.x} {cell.coordinates.y}");
		}
		if(coordinates.x+1<_width) _cells[coordinates.x+1, coordinates.y].Variants =tile.right;
		if(coordinates.x-1>=0)_cells[coordinates.x-1, coordinates.y].Variants =tile.left;
		if(coordinates.y-1>=0)_cells[coordinates.x, coordinates.y-1].Variants =tile.down;
		if(coordinates.y+1<_height) _cells[coordinates.x, coordinates.y+1].Variants = tile.up;
	}
	public Cell CollapseNextOrReturnNull()
	{
		if(!_isCollapsed)
		{
			Cell cell = FindCellWithLowEntropy();
			cell.CollapseCell();
			_collapsedCellCount++;
			_isCollapsed = _collapsedCellCount >= _width*_height;
			ChangeVariantsToNeighborhood(cell);
			return cell;
		}
		return null;
	}
	public Cell[,] Collapse()
	{
		while(!_isCollapsed)
		{
			CollapseNextOrReturnNull();
		}
		return _cells;
	}
}

