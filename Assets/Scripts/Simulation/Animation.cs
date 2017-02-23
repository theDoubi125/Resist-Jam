using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anim", menuName = "Anim", order = 1)]
public class Animation : ScriptableObject
{
    [SerializeField]
    private Sprite[] m_sprites = null;

    public int SpriteCount { get { return m_sprites.Length; } }

    public Sprite GetSprite(int id)
    {
        return m_sprites[id];
    }
}
