using UnityEngine;

/// <summary>
/// Sprite工具类
/// </summary>
public static class SpriteUtil
{
    /// <summary>
    /// 根据Texture2D创建Sprite
    /// </summary>
    /// <param name="texture2D"></param>
    /// <returns></returns>
    public static Sprite CreateSprite(Texture2D texture2D)
    {
        return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.one * 0.5f, 100, 0, SpriteMeshType.FullRect);
    }
}