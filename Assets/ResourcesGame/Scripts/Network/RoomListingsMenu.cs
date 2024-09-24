using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingsMenu : MonoBehaviour
{
    [SerializeField]
    private RoomListing _roomListing;
    private List<RoomListing> _listings = new List<RoomListing>();

    public void SetListRoom(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListing listing = Instantiate(_roomListing, transform);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);
                        _listings.Add(listing);
                    }
                }

            }
        }
    }
   
    public void DestroyListRoom()
    {
        foreach (var item in _listings)
        {
            Destroy(item.gameObject);
        }

        _listings.Clear(); ;
    }
        
    

}
