using UnityEngine;

public class GameSlotManager : MonoBehaviour
{
    public static GameSlotManager Instance;

    private int _currentGameSlot = 0;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public int GetCurrentSlot()
    {
        return _currentGameSlot;
    }

    public void SetCurrentSlot(int gameSlot)
    {
        _currentGameSlot = gameSlot;
    }
}
