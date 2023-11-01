
using UnityEngine;
using UnityEngine.UI;

public class WFCController : MonoBehaviour
{
	[SerializeField] private Button GenerateNextBtn;
	[SerializeField] private Button GenerateAllBtn;
	[SerializeField] private Button ResetBtn;
	[SerializeField] private WFC _map;
	// Start is called before the first frame update
	void Start()
	{
		ResetBtn.interactable = !_map.isGenerated;
		GenerateNextBtn.onClick.AddListener(()=> { _map.GenerateNext(); GenerateNextBtn.interactable = _map.isGenerated; GenerateAllBtn.interactable = _map.isGenerated; });
		GenerateAllBtn.onClick.AddListener(()=>{ _map.GenerateAll(); GenerateNextBtn.interactable = _map.isGenerated; GenerateAllBtn.interactable = _map.isGenerated;});
		ResetBtn.onClick.AddListener(()=> { _map.Reset();  ResetBtn.interactable = !_map.isGenerated; GenerateNextBtn.interactable = !_map.isGenerated; GenerateAllBtn.interactable = !_map.isGenerated;});	
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
