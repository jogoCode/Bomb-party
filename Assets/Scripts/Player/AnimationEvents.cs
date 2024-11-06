using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void PlayAudio(string SoundName)
    {
        SoundManager soundManager = SoundManager.Instance;
        soundManager.PlaySFX(SoundName);
    }
}
