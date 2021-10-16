using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PickUpUiMove : MonoBehaviour
{
	[Range(-1000f, 1000f)]
	public float TargetPositionLeft = -450f;
	[Range(-1000f, 1000f)]
	public float TargetPositionTop = 160f;
	[Range(0.001f, 1000f)]
	public float Speed = 1.0f;
	public float closeEnough;

	private Vector2 targetPosition;
	private Vector2 currentPosition;
	private RectTransform objTrans;
	private float distanceToTarget;

	public GameObject ResourceImage;
	public GameObject ResourceText;
	public string ResourceTypeName;
	public int textAmount;
	public string getText;

	// Start is called before the first frame update
	void Start()
    {
		//Get a reference to the UI object
		objTrans = GetComponent<RectTransform>();

		//Get its current position
		currentPosition = objTrans.anchoredPosition;

		//Get a reference to where we want it to go
		targetPosition = new Vector2(TargetPositionLeft, TargetPositionTop);

		//Run the UpdateTotal, which updates the UI Text for the number of this resource
		//UpdateTotal();
	}


	//This Sets the Resource Type
	public void SetResourceType(string resourceType)
	{
		ResourceTypeName = resourceType;

		switch (ResourceTypeName)
		{
			case "Coin":
				ResourceImage = GameObject.FindGameObjectWithTag("Coin_Icon");
				ResourceText = GameObject.FindGameObjectWithTag("Coin_Text");
				//getText = ResourceText.GetComponent<Text>().text;
				break;
			case "Heart":
				ResourceImage = GameObject.FindGameObjectWithTag("Heart_Icon");
				//ResourceText = GameObject.FindGameObjectWithTag("Heart_Text");
				//getText = ResourceText.GetComponent<TextMeshProUGUI>().text;
				break;
			/**case "Metal":
				ResourceImage = GameObject.FindGameObjectWithTag("Metal_Icon");
				ResourceText = GameObject.FindGameObjectWithTag("Metal_Text");
				getText = ResourceText.GetComponent<TextMeshProUGUI>().text;
				break;**/
		}
	}

	// Update is called once per frame
	void Update()
    {
		
		//Get a position that is a little bit closer to our goal position
		objTrans.anchoredPosition = Vector2.Lerp(currentPosition, targetPosition, Speed * Time.deltaTime);

		//Set our object to that new position
		currentPosition = objTrans.anchoredPosition;

		//How far are we from our goal?
		distanceToTarget = Vector2.Distance(currentPosition, targetPosition);

	}
	void LateUpdate()
	{
		//If we are close enough and we want the icon to disappear...
		if (distanceToTarget < closeEnough)
		{
			//Bonus: Make the default icon animate as the new resource is brought in
			//Swell the resource icon

			//Destroy the object, we are done with it!
			Destroy(gameObject);
		}
	}


	/**void UpdateTotal()
	{
		//Increase the resource count

		//Turn the displayed text into an integer (int) (a number)
		int.TryParse(getText, out textAmount);

		//Increase this number by 1
		textAmount++;

		//Turn this number back into text
		getText = textAmount.ToString();

		//Update the text object's display
		ResourceText.GetComponent<Text>().text = getText;

		//Force a refresh of the mesh display
		ResourceText.GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
	}**/
}
