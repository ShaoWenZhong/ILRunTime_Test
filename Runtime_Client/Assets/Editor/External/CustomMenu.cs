using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;

public static class CustomMenu
{
    private static Vector2 defaultImageSize = new Vector2();

    [MenuItem("Tool/¼ì²â×ÊÔ´")]
    public static void CheckBundle(MenuCommand menuCommand)
    {
        var paths = AssetDatabase.GetAllAssetPaths();
        for (int i = 0; i < paths.Length; i++)
        {
            Debug.Log(paths[i]);
        }
    }


    [MenuItem("GameObject/UI/AtlasSprite")]
    public static void CreatSpriteAsset(MenuCommand menuCommand)
    {
        GameObject gameObject = CreateAtlaSprite();
        PlaceUIElement(gameObject,menuCommand);
        gameObject.GetComponent<Image>().raycastTarget = false;
    }


    private static void PlaceUIElement(GameObject element, MenuCommand menuCommand)
    {
        GameObject parent = menuCommand.context as GameObject;
        if (parent == null || parent.GetComponentInParent<Canvas>() == null)
            parent = GetOrCreateCanvasGO();

        string uniqeName = GameObjectUtility.GetUniqueNameForSibling(parent.transform, element.name);
        element.name = uniqeName;
        Undo.RegisterCreatedObjectUndo(element,"Create" + element.name);
        Undo.SetTransformParent(element.transform, parent.transform, "Parent" + element.name);
        GameObjectUtility.SetParentAndAlign(element,parent);
        Selection.activeGameObject = element;
    }

    public static GameObject GetOrCreateCanvasGO()
    {
        GameObject selectGo = Selection.activeGameObject;
        Canvas canvas = (selectGo != null) ? selectGo.GetComponentInParent<Canvas>() : null;
        if (canvas != null && canvas.gameObject.activeInHierarchy)
            return canvas.gameObject;

        canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
        if (canvas != null & canvas.gameObject.activeInHierarchy)
            return canvas.gameObject;

        return CreateNewUI();
    }

    public static GameObject CreateNewUI()
    {
        var root = new GameObject("Canvas");
        root.layer = LayerMask.NameToLayer("UI");
        Canvas canvas = root.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        root.AddComponent<CanvasScaler>();
        root.AddComponent<GraphicRaycaster>();

        CreateEventSystem(false,null);
        return root;
    }

    private static void CreateEventSystem(bool select, GameObject parent)
    {
        var esys = Object.FindObjectOfType<EventSystem>();
        if (esys == null)
        {
            var eventSystem = new GameObject("EventSystem");
            GameObjectUtility.SetParentAndAlign(eventSystem,parent);
            esys = eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();

            Undo.RegisterCreatedObjectUndo(eventSystem,"Create" + eventSystem.name);
        }

        if (select && esys != null)
            Selection.activeGameObject = esys.gameObject;
    }


    public static GameObject CreateAtlaSprite()
    {
        GameObject gameObject = CreateUIElementRoot("Image",defaultImageSize);
        gameObject.AddComponent<AtlasSprite>();
        return gameObject;
    }

    private static GameObject CreateUIElementRoot(string name, Vector2 vector2)
    {
        GameObject gameObject = new GameObject(name);
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        return gameObject;
    }
}
