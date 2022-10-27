using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{


    public AudioSource MainTheme;

    private void Start()
    {
        MainTheme.volume = 0.15f;
    }

}
