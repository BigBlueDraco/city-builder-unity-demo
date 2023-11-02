public class CellWithConnectors : Cell
{
	public Connectors connectors;
	public CellWithConnectors(Coordinates coordinates, Tile[] variants) : base(coordinates, variants)
	{
	}
	public Connectors Connectors { get => connectors; set => connectors = value; }
}