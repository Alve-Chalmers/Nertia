using UnityEngine;
using System;

[ExecuteInEditMode]
public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [Tooltip("Per-layer parallax: layer 0 = 0, layer 1 = this, layer 2 = 2×, etc. Higher index = moves more with camera (X).")]
    [SerializeField][Range(0f, 1f)] private float perLayerSpeedOffset = 0.2f;

    [Tooltip("1 = layers follow camera Y; 0 = layers fixed in world (ignore camera Y).")]
    [SerializeField][Range(0f, 1f)] private float verticalSpeedOffset = 1f;

    private Vector2 _cameraStart;
    private Layer[] _layers;

    private struct Layer
    {
        public Transform Transform;
        public int SortingOrder;
        public int SiblingIndex;
        public float OffsetX;
        public float OffsetY;
        public float Width;
    }

    void Start()
    {
        RebuildLayerCache();
    }

    void OnEnable()
    {
        RebuildLayerCache();
    }

    void OnTransformChildrenChanged()
    {
        RebuildLayerCache();
    }

    void RebuildLayerCache()
    {
        if (cameraTransform == null) return;

        _cameraStart = cameraTransform.position;
        Vector2 bgStart = transform.position;
        int n = transform.childCount;
        _layers = new Layer[n];

        for (int i = 0; i < n; i++)
        {
            Transform layerTransform = transform.GetChild(i);
            ClearLayerCopies(layerTransform);

            SpriteRenderer sr = layerTransform.GetComponent<SpriteRenderer>();
            float wRaw = sr != null ? sr.bounds.size.x : 10f;
            float width = Mathf.Max(0.001f, wRaw);
            _layers[i] = new Layer
            {
                Transform = layerTransform,
                SortingOrder = sr != null ? sr.sortingOrder : 0,
                SiblingIndex = i,
                OffsetX = layerTransform.position.x - bgStart.x,
                OffsetY = layerTransform.position.y - bgStart.y,
                Width = width
            };

            if (sr != null)
            {
                float w = width;
                CreateCopy(sr, layerTransform, -w, 0f);
                CreateCopy(sr, layerTransform, w, 0f);
                CreateCopy(sr, layerTransform, -w * 2f, 0f);
                CreateCopy(sr, layerTransform, w * 2f, 0f);
                CreateCopy(sr, layerTransform, -w * 3f, 0f);
                CreateCopy(sr, layerTransform, w * 3f, 0f);
                CreateCopy(sr, layerTransform, -w * 4f, 0f);
                CreateCopy(sr, layerTransform, w * 4f, 0f);
            }
        }

        Array.Sort(_layers, CompareLayers);
    }

    static int CompareLayers(Layer a, Layer b)
    {
        int sortingComparison = b.SortingOrder.CompareTo(a.SortingOrder);
        if (sortingComparison != 0) return sortingComparison;
        return a.SiblingIndex.CompareTo(b.SiblingIndex);
    }

    static void ClearLayerCopies(Transform layer)
    {
        for (int c = layer.childCount - 1; c >= 0; c--)
        {
            GameObject go = layer.GetChild(c).gameObject;

            if (Application.isPlaying)
                Destroy(go);
            else
                DestroyImmediate(go);
        }
    }

    static void CreateCopy(SpriteRenderer source, Transform parent, float localX, float localY)
    {
        var go = new GameObject(source.name + "_copy");
        go.transform.SetParent(parent, false);
        go.transform.localPosition = new Vector3(localX, localY, 0f);
        var copy = go.AddComponent<SpriteRenderer>();
        copy.sprite = source.sprite;
        copy.color = source.color;
        copy.sortingLayerID = source.sortingLayerID;
        copy.sortingOrder = source.sortingOrder;
        copy.flipX = source.flipX;
        copy.flipY = source.flipY;
    }

    void Update()
    {
        if (cameraTransform == null) return;
        int n = _layers?.Length ?? 0;
        if (n == 0) return;

        float cameraX = cameraTransform.position.x;
        float cameraY = cameraTransform.position.y;
        float dx = cameraX - _cameraStart.x;
        Vector2 bgNow = transform.position;
        float closestY = bgNow.y + _layers[n - 1].OffsetY;
        float yCameraTarget = cameraY + (bgNow.y - _cameraStart.y);

        for (int i = 0; i < n; i++)
        {
            Layer layer = _layers[i];
            float parallax = (i+1) * perLayerSpeedOffset;

            float x = bgNow.x + layer.OffsetX + dx * parallax;
            x = WrapHorizontal(x, cameraX, layer.Width);

            float t = (float)(i + 1) / (n + 1);
            float yFixed = bgNow.y + layer.OffsetY;
            float yCameraFollowing = Mathf.Lerp(closestY, yCameraTarget, t);
            float y = Mathf.Lerp(yFixed, yCameraFollowing, verticalSpeedOffset);

            layer.Transform.position = new Vector3(x, y, layer.Transform.position.z);
        }
    }

    /// <summary>Brings x into [cameraX - layerWidth, cameraX + layerWidth] using modulo.</summary>
    static float WrapHorizontal(float x, float cameraX, float layerWidth)
    {
        if (layerWidth <= 0.001f) return x;
        float offset = x - cameraX;
        float wrapped = Mod(offset + layerWidth, 2f * layerWidth) - layerWidth;
        return cameraX + wrapped;
    }

    static float Mod(float a, float b)
    {
        float r = a % b;
        return r < 0f ? r + b : r;
    }
}
