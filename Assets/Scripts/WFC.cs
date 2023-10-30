using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Random = System.Random;

partial class Cell
{
	public bool isCollapsed; 
	public Tile[] variants;
	public Cell(Tile[] variants)
	{
		this.variants= variants;
		this.isCollapsed = false;
	}
	public void SetVariants(Tile[] tiles)
	{
		List<Tile> newVariants = new List<Tile>();
		int newArrayLenght = 0;
		foreach(Tile variant in variants)
		{
			foreach(Tile tile in tiles)
			{
				if(variant.type == tile.type)
				{
					newArrayLenght++;
					newVariants.Add(variant);				
				}
			}
		}
		this.variants = newVariants.ToArray();
	}
}
public class WFC : MonoBehaviour
{
	private Random rand = new Random();
	[SerializeField]
	private int width = 5;
	[SerializeField]
	private int height = 5;
	[SerializeField]
	private Tile[] tiles = new Tile[1];
	void Start()
	{
		Cell[,] grid = new Cell[width, height];
		for(int w = 0; w<= width-1; w++){
		for(int h = 0; h<= height-1; h++)
		{
			grid[w,h] = new Cell(tiles);
		}}
		for(int h = 0; h<= height-1; h++){
		for(int w= 0; w<= width-1; w++)
		{
			Cell cell = grid[w,h];

			Tile tile = cell.variants[rand.Next(0, cell.variants.Length)];
			Tile newTile = Instantiate(tile);
			newTile.name = $"tile({w},{h})";
			newTile.transform.position = new Vector3(1+w*2,0,1+h*2);
			cell.isCollapsed = true;
			if(w+1<width) grid[w+1, h].SetVariants( tile.right);
			if(w-1>0)grid[w-1, h].SetVariants(tile.left);
			if(h-1>0)grid[w, h-1].SetVariants(tile.down);
			if(h+1<height) grid[w, h+1].SetVariants( tile.up);
		}};
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
