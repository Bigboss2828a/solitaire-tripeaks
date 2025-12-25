using System;
using UnityEngine;

public class ActionManagerMenu : MonoBehaviour
{
    public static ActionManagerMenu Instance;

    public Action<MapLevelItem> OnLevelClicked;
    private void Awake()
    {
            Instance = this;    
    }
}
