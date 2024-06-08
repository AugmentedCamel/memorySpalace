using UnityEngine;

public class SoundEffectTrigger : MonoBehaviour
{
    public string soundName;

    public void PlaySound()
    {
        if (SoundEffectManager.instance != null)
        {
            SoundEffectManager.instance.PlaySound(soundName, transform.position);
        }
    }
}
