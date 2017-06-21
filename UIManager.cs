/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
   CMDG LISA ENGELEN
	PORTFOLIO: https://lisaengelen-portfolio.tumblr.com/ */
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {

	#region Variables
	public RectTransform btnPrefab;

	private FeatureManager agr;
	private Text descText;
	private List<Button> buttons;
	#endregion

	#region Unity Methods
	void Start () 
	{
		agr = FindObjectOfType<FeatureManager>();
		descText = transform.Find("Customization").Find("Description").GetComponent<Text>();
		transform.Find("Customization").Find("Next").GetComponent<Button>().onClick.AddListener(() => agr.NextChoice());
		transform.Find("Customization").Find("Previous").GetComponent<Button>().onClick.AddListener(() => agr.PreviousChoice());
		transform.Find("Customization").Find("Save").GetComponent<Button>().onClick.AddListener(() => agr.SaveFeature());
		InitializeFeatureButtons();
	}

	private void InitializeFeatureButtons()
	{
		buttons = new List<Button>();

		float height = btnPrefab.rect.height;
		float width = btnPrefab.rect.width;
		for (int i = 0; i < agr.features.Count; i++)
		{
			RectTransform temp = Instantiate<RectTransform>(btnPrefab);
			temp.name = i.ToString();
			temp.SetParent(transform.Find("Features").GetComponent<RectTransform>());
			temp.localScale = Vector3.one;
			temp.localPosition = Vector3.zero;
			temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, width);
			temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * height, height);

			Button b = temp.GetComponent<Button>();
			b.onClick.AddListener(() => agr.SetCurrent(int.Parse(temp.name)));
			buttons.Add(b);
		}
	}

	private void UpdateFeatureButtons()
	{
		for (int i = 0; i < agr.features.Count; i++)
		{
			//buttons[i].transform.Find("FeatureImg").GetComponent<Image>().sprite = agr.features[i].renderer.sprite;
		}
	}

	void Update () 
	{
		UpdateFeatureButtons();
		EventSystem.current.SetSelectedGameObject(buttons[agr.curFeature].gameObject);
		descText.text = agr.features[agr.curFeature].ID + " #" + agr.features[agr.curFeature].curIndex;
	}
	#endregion
}
