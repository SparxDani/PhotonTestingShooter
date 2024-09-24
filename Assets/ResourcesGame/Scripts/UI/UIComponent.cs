using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ComponentUI { CreateRoom, ListRoom,MenuLobby,CurrentRoom, CreatePlayer }
public class UIComponent : MonoBehaviour
{
    public ComponentUI type;
}
