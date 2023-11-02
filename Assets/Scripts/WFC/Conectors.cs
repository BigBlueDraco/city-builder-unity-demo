using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Connectors: IRotated
{
	private string[] _up;
	private string[] _right;
	private string[] _down;
	private string[] _left;
	public Connectors(string[] connector){}
	public Connectors(string[] hor, string[] vert){}
	public Connectors(string[] hor, string[] up, string[] down){}
	public Connectors(string[] up, string[] right,  string[] down, string[] left)
	{
		Up = up;
		Right = right;
		Down = down;
		Left = left;
	}

	public  int RotationPosition{ get; }
	public string[] Up { get => _up; set => _up = value.ToArray(); }
	public string[] Right { get => _right; set => _right = value.ToArray(); }
	public string[] Down { get => _down; set => _down = value.ToArray(); }
	public string[] Left { get => _left; set => _left = value.ToArray(); }

	public  void RotateLeft()
	{
			string[] tmp = this.Up;
			this.Up = this.Right;
			this.Right = this.Down;
			this.Down = this.Left;
			this.Left = tmp;
	}

    public  void RotateRight()
	{
			string[] tmp = this.Up;
			this.Up = this.Left;
			this.Left = this.Down;
			this.Down = this.Right;
			this.Right = tmp;
	}
}
