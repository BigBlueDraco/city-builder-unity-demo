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
	}

	public Connectors Connectors { get => _connectors; set
	{
		_connectors = value;
		SetVariantsBaseByConnectors();
	}}

	public Cell(Coordinates coordinates, Tile[] variants)
	{
		HashSet<String> up = new HashSet<String>();
		HashSet<String> right = new HashSet<String>();
		HashSet<String> down = new HashSet<String>();
		HashSet<String> left = new HashSet<String>();
		foreach(Tile variant in variants.ToArray())
		{
			foreach(string str in variant.Connectors.Up)
			{
				up.Add(str);
			}
			foreach(string str in variant.Connectors.Right)
			{
				right.Add(str);
			}
			foreach(string str in variant.Connectors.Down)
			{
				down.Add(str);
			}
			foreach(string str in variant.Connectors.Left)
			{
				left.Add(str);
			}
		}
		_variants= variants.ToArray();
		this.coordinates = coordinates;
		this.isCollapsed = false;
		_connectors = new Connectors(up.ToArray(), right.ToArray(), down.ToArray(), left.ToArray());
		SetVariantsBaseByConnectors();
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
		_variants = variant;
		this.isCollapsed = true;
	}
	public void ExecuteVariantByType(string type)
	{
		HashSet<Tile> newVariants = new HashSet<Tile>();
		foreach(Tile variant in Variants)
		{
			if (variant.type != type) 
			{
				newVariants.Add(variant);
			}
		}
		_variants = newVariants.ToArray();
	}
}
