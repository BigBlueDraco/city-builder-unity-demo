using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

partial class Cell
{
	public int x;
	public int y;
	public bool isCollapsed; 
	public Tile[] variants;
	public Cell(int x, int y, Tile[] variants)
	{
		this.variants= variants;
		this.x =x;
		this.y =y;
		this.isCollapsed = false;
	}
	public void SetVariants(Tile[] tiles)
	{
		HashSet<Tile> newVariants = new HashSet<Tile>();
		foreach(Tile variant in variants)
		{
			foreach(Tile tile in tiles)
			{
				if(variant.type == tile.type)
				{
					newVariants.Add(tile);				
				}
			}
		}
		this.variants = newVariants.ToArray();
	}
}
public class WFC : MonoBehaviour
{
	[SerializeField] private Button MyButton = null;
	private Random rand = new Random();
	[SerializeField]
	private int width = 5;
	[SerializeField]
	private int height = 5;
	[SerializeField]
	private Tile[] tiles = new Tile[1];
	private Cell[,] grid;
	private bool isGridColabsed;

	public void ColabseCell(int x, int y)
	{
			Cell cell = grid[x,y];
			Tile tile = cell.variants[rand.Next(0, cell.variants.Length)];
			Tile newTile = Instantiate(tile);
			newTile.name = $"tile({x},{y})";
			newTile.transform.position = new Vector3(1+x*2,0,1+y*2);
			if(tile.type == "Road"&& x+1 < width)
			{
				Debug.Log($"{x}, {y}");
				Debug.Log(grid[x,y].variants.Length);
			}
			grid[x,y].isCollapsed = true;
			if(x+1<width) grid[x+1, y].SetVariants( tile.right);
			if(x-1>=0)grid[x-1, y].SetVariants(tile.left);
			if(y-1>=0)grid[x, y-1].SetVariants(tile.down);
			if(y+1<height) grid[x, y+1].SetVariants( tile.up);
			
	}
	private void GenerateMap()
	{
		if(!this.isGridColabsed)
		{
					Cell lowEntropyCell = grid[0, 0]; 
		int colabsedCellCount = 0;
					Cell randCell = grid[rand.Next(0, width),rand.Next(0, height)];
			while(randCell.isCollapsed && !isGridColabsed)
			{
				randCell = grid[rand.Next(0, width),rand.Next(0, height)];
			}
			lowEntropyCell = randCell;
		// while(!this.isGridColabsed)
		// {
			for(int h = 0; h<= height-1; h++){
			for(int w= 0; w<= width-1; w++)
			{
				Cell cell = grid[w,h];
				if(cell.variants.Length < lowEntropyCell.variants.Length&&!cell.isCollapsed&&cell.x != lowEntropyCell.x && cell.y != lowEntropyCell.y)
				{
					lowEntropyCell = grid[w,h];
				};
				
			}};
			ColabseCell(lowEntropyCell.x, lowEntropyCell.y);
			colabsedCellCount++;	
			this.isGridColabsed = colabsedCellCount+1 >= width*height;
		}


		Debug.Log("Colabsed");
	}
	void Start()
	{
		this.grid= new Cell[width, height];
		for(int w = 0; w<= this.width-1; w++){
		for(int h = 0; h<= this.height-1; h++)
		{
			grid[w,h] = new Cell(w, h, tiles);
		}}

		ColabseCell(rand.Next(0, width),rand.Next(0, height));
		MyButton.onClick.AddListener(() => { GenerateMap();});
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
