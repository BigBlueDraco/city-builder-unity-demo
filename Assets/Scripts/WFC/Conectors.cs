using System.Linq;

public class Connectors : IRotated
{
    private string[] _up;
    private string[] _right;
    private string[] _down;
    private string[] _left;

    public Connectors(string[] connector) { }
    public Connectors(string[] hor, string[] vert) { }
    public Connectors(string[] hor, string[] up, string[] down) { }
    public Connectors(string[] up, string[] right, string[] down, string[] left)
    {
        Up = up;
        Right = right;
        Down = down;
        Left = left;
    }

    public int RotationPosition { get; }
    public string[] Up { get => _up; set => _up = value.ToArray(); }
    public string[] Right { get => _right; set => _right = value.ToArray(); }
    public string[] Down { get => _down; set => _down = value.ToArray(); }
    public string[] Left { get => _left; set => _left = value.ToArray(); }

    public override bool Equals(object obj)
    {
        if (obj is Connectors item)
        {
            return IsDirectionEqual(Up, item.Up) && IsDirectionEqual(Right, item.Right) &&
                   IsDirectionEqual(Down, item.Down) && IsDirectionEqual(Left, item.Left);
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + GetHashCode(Up);
        hash = hash * 23 + GetHashCode(Right);
        hash = hash * 23 + GetHashCode(Down);
        hash = hash * 23 + GetHashCode(Left);
        return hash;
    }

    private int GetHashCode(string[] arr)
    {
        if (arr == null)
            return 0;

        int hash = 17;
        foreach (string item in arr)
        {
            if (item != null)
                hash = hash * 23 + item.GetHashCode();
        }
        return hash;
    }

    public static bool operator ==(Connectors obj1, Connectors obj2)
    {
        if (ReferenceEquals(obj1, obj2))
            return true;
        if (ReferenceEquals(obj1, null))
            return false;
        if (ReferenceEquals(obj2, null))
            return false;
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Connectors obj1, Connectors obj2) => !(obj1 == obj2);

    private bool IsDirectionEqual(string[] obj1, string[] obj2)
    {
        if (obj1 == null || obj2 == null)
            return false;

        foreach (string connector1 in obj1)
        {
            foreach (string connector2 in obj2)
            {
                if (connector1 == connector2)
                    return true;
            }
        }
        return false;
    }

    public void RotateLeft()
    {
        string[] tmp = this.Up;
        this.Up = this.Right;
        this.Right = this.Down;
        this.Down = this.Left;
        this.Left = tmp;
    }

    public void RotateRight()
    {
        string[] tmp = this.Up;
        this.Up = this.Left;
        this.Left = this.Down;
        this.Down = this.Right;
        this.Right = tmp;
    }
}
