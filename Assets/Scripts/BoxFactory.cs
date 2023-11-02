
using UnityEngine;
using UnityEngine.UI;

public class BoxFactory : MonoBehaviour
{
	[SerializeField]
	private Button _btn;
	[SerializeField]
	private Tile tile;
	void Start()
	{
		_btn.onClick.AddListener(() =>
		{	
			Tile newTile = tile.Render(1);
		});
	}

}
