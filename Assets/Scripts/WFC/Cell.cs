using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Cell: ICollapsable
{
	private Random rand = new Random();
	public Coordinates coordinates;
	public bool isCollapsed;
	private Connectors connectors;
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

	public Connectors Connectors { get => connectors; set
	{
		connectors = value;
		foreach(Tile tile in Variants)
		{
			if(tile.Connectors == connectors)
			{
				
			} 
		}
	}}

	public Cell(Coordinates coordinates, Tile[] variants)
	{
		_variants= variants.ToArray();
		this.coordinates = coordinates;
		this.isCollapsed = false;
	}
	public void Collapse()
	{
		int indx = rand.Next(0, _variants.Length);
		Debug.Log(indx);
		Tile tile = _variants[indx];
		Tile[] variant = new Tile[1];
		variant[0] = tile;
		this.Variants = variant;
		this.isCollapsed = true;
	}
}
