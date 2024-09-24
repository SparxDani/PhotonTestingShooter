using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPlayer : MonoBehaviour
{
    public List<GameObject> allCharacter = new List<GameObject>();
	public List<GameObject> RoomCharacter = new List<GameObject>();
	public int characterSelected;
    // Start is called before the first frame update
    void Start()
    {
		ActiveCharacter(characterSelected,0);
	}
	public void OnNextPlayerButtonClicked()
	{
		int oldindex = characterSelected;
		characterSelected++;
		characterSelected = characterSelected %allCharacter.Count;
		ActiveCharacter(characterSelected, oldindex);
	}
	public void OnBackPlayerButtonClicked()
	{
		int oldindex = characterSelected;
		characterSelected--;
		characterSelected = (characterSelected<0)?allCharacter.Count-1: characterSelected;
		ActiveCharacter(characterSelected, oldindex);
	}
	// Update is called once per frame
	private void ActiveCharacter(int index,int oldindex)
	{
		
		for (int i = 0; i < allCharacter.Count; i++)
		{
			if (i == index)
			{
				UICharacter charact = allCharacter[index].GetComponent<UICharacter>();
				if(charact!=null)
				{
					allCharacter[index].SetActive(true);
					allCharacter[index].transform.rotation = allCharacter[oldindex].transform.rotation;
					PlayerInfo.instance.Selectedcharacter = charact.prefabnameSand;
					RoomCharacter[index].SetActive(true);
				}
			}
			else
			{
				allCharacter[i].SetActive(false);
				RoomCharacter[i].SetActive(false);
			}
		}
		

	}
}
