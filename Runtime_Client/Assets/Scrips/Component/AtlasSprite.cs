using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AtlasSprite : MonoBehaviour
{
    public SpriteAssest atlas;
    public Image atlasSourceImage;
    private string cache_name= string.Empty;
    private bool cache_makePrefect = false;

    public string Sprite
    {
        get 
        {
            if (atlasSourceImage != null)
                return atlasSourceImage.sprite.name;
            return string.Empty;

        }

        set
        {
            if (atlasSourceImage == null)
                atlasSourceImage = GetComponent<Image>();

            if (atlasSourceImage != null && atlas != null)
                atlasSourceImage.sprite = atlas.GetSpriteWithName(value);
            else
                cache_name = value;
        }
    }

    public void MakePixelPrefect()
    {
        if (atlasSourceImage != null)
            atlasSourceImage.SetNativeSize();
        else
            cache_makePrefect = true;    
    }

    // Start is called before the first frame update
    void Start()
    {
        if(atlasSourceImage == null)
            atlasSourceImage = GetComponent<Image>();

        if (!string.IsNullOrEmpty(cache_name))
        {
            Sprite = cache_name;
            cache_name = string.Empty;
        }

        if (cache_makePrefect)
        {
            MakePixelPrefect();
            cache_makePrefect = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
