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
	private Connectors _connectors;
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
					if(variant.Connectors == tile.Connectors)
					{
						newVariants.Add(tile);				
					}
				}
			}
			_variants = newVariants.ToArray();
		}
	}

	public Connectors Connectors { get => _connectors; set
	{
		_connectors = value;
		SetVariantsBaseByConnectors();
	}}

	public Cell(Coordinates coordinates, Tile[] variants)
	{
		_variants= variants.ToArray();
		this.coordinates = coordinates;
		this.isCollapsed = false;
	}
	private void SetVariantsBaseByConnectors()
	{
		HashSet<Tile> newVariants = new HashSet<Tile>();
		foreach(Tile variant in Variants)
		{
			if(variant.Connectors == this.Connectors)
			{
				newVariants.Add(variant);
			}
		}
		_variants = newVariants.ToArray();
	}
	public void Collapse()
	{
		int indx = rand.Next(0, _variants.Length);
		Tile tile = _variants[indx];
		Tile[] variant = new Tile[1];
		variant[0] = tile;
		Connectors = new Connectors(variant[0].Connectors.Up, variant[0].Connectors.Right,variant[0].Connectors.Down,variant[0].Connectors.Left);
		this.Variants = variant;
		this.isCollapsed = true;
	}
}
