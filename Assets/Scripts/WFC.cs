using UnityEngine;
using UnityEngine.UI;

public class WFC : MonoBehaviour
{
	[SerializeField] private Button Next = null;
	[SerializeField] private Button All = null;
	[SerializeField]
	private int width = 5;
	[SerializeField]
	private int height = 5;
	[SerializeField]
	private TileSet tiles;
	[SerializeField]
	private Tile error;

	void Start()
	{
		Grid grid = new Grid(width, height, tiles);
		Next.onClick.AddListener(() => {
			Cell cell = grid.ColabseNextOrReturnNull();
			if(cell!=null)
			{
				Tile tile = cell.GetVariants()[0];
				tile.transform.position = new Vector3(1+cell.cordinats.x*2, 0, 1+cell.cordinats.y*2);
				Instantiate(tile);
				Next.interactable = !grid.GetIsColabsed();
			}
		 });
		All.onClick.AddListener(() => {
			Cell[,] cells = grid.Colabse();
			for(int w = 0; w< grid.Width; w++){
			for(int h = 0; h< grid.Height; h++)
			{
				Cell cell = cells[w, h];
				if(cell.GetVariants().Length <= 0 )
				{
					error.transform.position = new Vector3(1+cell.cordinats.x*2, 0, 1+cell.cordinats.y*2);
					error.name = $"Error {1 + cell.cordinats.x * 2}, {1 + cell.cordinats.y * 2}";
					Instantiate(error);
					return;
				}
				Tile tile = cell.GetVariants()[0];
				tile.transform.position = new Vector3(1+cell.cordinats.x*2, 0, 1+cell.cordinats.y*2);
				tile.name = $"Tile {cell.cordinats.x }, {cell.cordinats.y}";
				Instantiate(tile);
				All.interactable = false;
			}
			}
		 });
	}
}
