using System.Collections.Generic;
using System.Linq;
using Random = System.Random;
using Debug = UnityEngine.Debug;

public class Cordinats
{
	public int x;
	public int y;
	public Cordinats(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}
public class Cell
{
	private Random rand = new Random();
	public Cordinats cordinats;
	public bool isCollapsed; 
	private Tile[] _variants;
	public Cell(Cordinats cordinats, Tile[] variants)
	{
		_variants= variants.ToArray();
		this.cordinats = cordinats;
		this.isCollapsed = false;
	}
	public void SetVariants(Tile[] tiles)
	{
		HashSet<Tile> newVariants = new HashSet<Tile>();
		foreach(Tile variant in _variants)
		{
			foreach(Tile tile in tiles)
			{
				if(variant.type == tile.type)
				{
					newVariants.Add(tile);				
				}
			}
		}
		if(newVariants.ToArray().Length <= 0)
		{
			Debug.LogError("Set 0 vars");
		}
		_variants = newVariants.ToArray();
	}
	public Tile[] GetVariants()
	{
		return _variants;
	}
	public void ColabseCell()
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
		this.SetVariants(variant);
		this.isCollapsed = true;
	}
}

public class Grid
{
	private Random rand = new Random();
	
	private int _width;
	private int _height;
	private Cell[,] _cells;
	private bool _isColabsed;
	private int _colabsedCellCount;
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
	public bool GetIsColabsed()
	{
		return _isColabsed;
	}
	private void FildGrid(Tile[] tileSet)
	{
		for(int w = 0; w<= _width-1; w++){
		for(int h = 0; h<= _height-1; h++)
		{
			_cells[w,h] = new Cell(new Cordinats(w,h), tileSet);
		}}
	}
	public Grid(int width, int height, Tile[] tileSet)
	{
		_width = width;
		_height = height;
		CrateGrid();
		FildGrid(tileSet);
		_isColabsed = false;
	}
	public Cell GetCell(int x, int y)
	{
		return _cells[x, y];
	}
	public Cell GetCell(Cordinats cordinats)
	{
		return _cells[cordinats.x, cordinats.y];
	}
	private Cell RandomUncolabsedCell()
	{
		Cell randCell = _cells[rand.Next(0, _width),rand.Next(0, _height)];
		while(randCell.isCollapsed && !_isColabsed)
		{
			randCell = _cells[rand.Next(0, _width),rand.Next(0, _height)];
		}
		return randCell;
	}
	private Cell FindCellWithLowEntropy()
	{
		Cell lowEntropyCell = RandomUncolabsedCell();
		for(int h = 0; h<= _height-1; h++){
		for(int w= 0; w<= _width-1; w++)
		{
			Cell cell = _cells[w,h];
			if(cell.GetVariants().Length < lowEntropyCell.GetVariants().Length&&!cell.isCollapsed&&cell.cordinats != lowEntropyCell.cordinats)
			{
				lowEntropyCell = _cells[w,h];
			};
			
		}};
		return lowEntropyCell;
	}
	public void ChangeVariantsToNeighborhood(Cell cell)
	{
		Cordinats cordinats = cell.cordinats;
		Tile tile = cell.GetVariants()[0];
		if(tile ==null)
		{
			Debug.LogError($"Tile varians null {cell.cordinats.x} {cell.cordinats.y}");
		}
		if(cordinats.x+1<_width) _cells[cordinats.x+1, cordinats.y].SetVariants(tile.right);
		if(cordinats.x-1>=0)_cells[cordinats.x-1, cordinats.y].SetVariants(tile.left);
		if(cordinats.y-1>=0)_cells[cordinats.x, cordinats.y-1].SetVariants(tile.down);
		if(cordinats.y+1<_height) _cells[cordinats.x, cordinats.y+1].SetVariants( tile.up);
	}
	public Cell ColabseNextOrReturnNull()
	{
		if(!_isColabsed)
		{
			Cell cell = FindCellWithLowEntropy();
			cell.ColabseCell();
			_colabsedCellCount++;
			_isColabsed = _colabsedCellCount >= _width*_height;
			ChangeVariantsToNeighborhood(cell);
			return cell;
		}
		return null;
	}
	public Cell[,] Colabse()
	{
		while(!_isColabsed)
		{
			ColabseNextOrReturnNull();
		}
		return _cells;
	}
}
