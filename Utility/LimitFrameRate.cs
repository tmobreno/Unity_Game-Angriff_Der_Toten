using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFrameRate : MonoBehaviour
{
#if (UNITY_EDITOR)

    [SerializeField] private int frameRate;
    
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#endif
}
