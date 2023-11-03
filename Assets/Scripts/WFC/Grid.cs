using System.Collections.Generic;
using System.Linq;
using Random = System.Random;
using Debug = UnityEngine.Debug;

interface ICollapsable
{
	public void Collapse(){}
}
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


public class Grid: ICollapsable
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
		Connectors cellConnectors = cell.Connectors;
		Tile  tile = cell.Variants[0];
		Coordinates coordinates = cell.coordinates;	
		if(coordinates.x+1<_width)
		{
			Connectors rightConnectors = _cells[coordinates.x + 1, coordinates.y].Connectors;
			if(!tile.canConnectToHimselfRight)_cells[coordinates.x + 1, coordinates.y].ExecuteVariantByType(cell.Variants[0].type);
			_cells[coordinates.x + 1, coordinates.y].Connectors = new Connectors(up: rightConnectors.Up , right: rightConnectors.Right, down: rightConnectors.Down, left: cellConnectors.Right);	
		};
		if(coordinates.x-1>=0)
		{
			Connectors leftConnectors = _cells[coordinates.x - 1, coordinates.y].Connectors;
			if(!tile.canConnectToHimselfLeft)_cells[coordinates.x - 1, coordinates.y].ExecuteVariantByType(cell.Variants[0].type);
			_cells[coordinates.x - 1, coordinates.y].Connectors = new Connectors(up: leftConnectors.Up , right: cellConnectors.Left, down: leftConnectors.Down, left: leftConnectors.Left);
			
		};
		if(coordinates.y-1>=0)
		{
			Connectors downConnectors = _cells[coordinates.x, coordinates.y-1].Connectors;
			if(!tile.canConnectToHimselfDown)_cells[coordinates.x, coordinates.y-1].ExecuteVariantByType(cell.Variants[0].type);
			_cells[coordinates.x, coordinates.y-1].Connectors = new Connectors(up: cellConnectors.Down , right: downConnectors.Right, down: downConnectors.Down, left: downConnectors.Left);
		};
		if(coordinates.y+1<_height)
		{
			Connectors upConnectors = _cells[coordinates.x , coordinates.y+1].Connectors;
			if(!tile.canConnectToHimselfUp)_cells[coordinates.x, coordinates.y + 1].ExecuteVariantByType(cell.Variants[0].type);
			_cells[coordinates.x, coordinates.y+1].Connectors = new Connectors(up: upConnectors.Up , right: upConnectors.Right, down: cellConnectors.Down, left: upConnectors.Left);	

		};
	}
	public Cell CollapseNextOrReturnNull()
	{
		if(!_isCollapsed)
		{
			Cell cell = FindCellWithLowEntropy();
			cell.Collapse();
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

