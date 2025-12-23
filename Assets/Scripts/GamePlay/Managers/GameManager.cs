using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Transform _collectPos; public Transform collectPos() { return _collectPos; }
    private void Awake()
    {
            instance = this;    
    }
}
