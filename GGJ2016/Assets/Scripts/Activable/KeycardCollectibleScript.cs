﻿using UnityEngine;
using System.Collections;

public class KeycardCollectibleScript : CollectibleScript
{
    public GameStateManager.KeyCards m_keyType;

    [HideInInspector]
    public Material redMat;
    [HideInInspector]
    public Material greenMat;
    [HideInInspector]
    public Material cyanMat;
    [HideInInspector]
    public Material blueMat;
    [HideInInspector]
    public Material yellowMat;
    [HideInInspector]
    public Material magentaMat;

    public Renderer m_meshRenderer;

    public override void Start()
    {
        switch(m_keyType)
        {
            case GameStateManager.KeyCards.Red:
                m_meshRenderer.sharedMaterial = redMat;
                break;
            case GameStateManager.KeyCards.Green:
                m_meshRenderer.sharedMaterial = greenMat;
                break;
            case GameStateManager.KeyCards.Cyan:
                m_meshRenderer.sharedMaterial = cyanMat;
                break;
            case GameStateManager.KeyCards.Blue:
                m_meshRenderer.sharedMaterial = blueMat;
                break;
            case GameStateManager.KeyCards.Yellow:
                m_meshRenderer.sharedMaterial = yellowMat;
                break;
            case GameStateManager.KeyCards.Magenta:
                m_meshRenderer.sharedMaterial = magentaMat;
                break;
        }

        base.Start();
    }

    public override void Pickup()
    {
        GameStateManager.Instance.CollectKeyCard(m_keyType);
        base.Pickup();
    }
}
