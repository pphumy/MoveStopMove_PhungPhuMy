using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : Singleton<CinemachineManager>
{
    public CinemachineVirtualCamera playVirtualCam;
    public CinemachineVirtualCamera skinShopVirtualCam;
    public CinemachineVirtualCamera startGameVirtualCam;

    private List<CinemachineVirtualCamera> cams = new List<CinemachineVirtualCamera>();

    private void Start()
    {
        cams.Add(playVirtualCam);
        cams.Add(skinShopVirtualCam);
        cams.Add(startGameVirtualCam);

        SwitchToStartGameCam();
    }

    public void SwitchToPlayCam()
    {
        playVirtualCam.Priority = 10;

        foreach(CinemachineVirtualCamera cam in cams)
        {
            if (cam != playVirtualCam)
            {
                cam.Priority = 0;
            }
        }
    }

    public void SwitchToSkinShopCam()
    {
        skinShopVirtualCam.Priority = 10;

        foreach (CinemachineVirtualCamera cam in cams)
        {
            if (cam != skinShopVirtualCam)
            {
                cam.Priority = 0;
            }
        }
    }

    public void SwitchToStartGameCam()
    {
        startGameVirtualCam.Priority = 10;

        foreach (CinemachineVirtualCamera cam in cams)
        {
            if (cam != startGameVirtualCam)
            {
                cam.Priority = 0;
            }
        }
    }
}
