using System.Collections;
using System.Collections.Generic;
using UnityEngine;


abstract class Connectors: IRotated
{
	private string[] _up;
	public abstract string[] Up{ get; set; }
	private string[] _right;
	public abstract string[] Right{ get; set; }
	private string[] _down;
	public abstract string[] Down{ get; set; }
	private string[] _left;
	public abstract string[] Left{ get; set; }
	Connectors(string[] connector){}
	Connectors(string[] hor, string[] vert){}
	Connectors(string[] hor, string[] up, string[] down){}
	Connectors(string[] left, string[] right, string[] up, string[] down){}

	public abstract int RotationPosition{ get; }

	public abstract void RotateLeft(int times);

	public abstract void RotateRight(int times);
}
