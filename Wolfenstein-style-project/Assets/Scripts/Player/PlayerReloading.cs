using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerReloading : MonoBehaviour
{
    public static event Action OnReload;
    PlayerStateManager playerStateManager;

    bool isReloading;
    // Start is called before the first frame update
    void Start()
    {
        playerStateManager = GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        isReloading = playerStateManager.playerActionInputHandler.isReloading;
        if (isReloading)
        {
            playerStateManager.playerActionInputHandler.isReloading = false;
            OnReload?.Invoke();
        }
    }
}
