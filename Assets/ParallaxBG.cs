using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [Tooltip("Per-layer parallax: layer 0 = 0, layer 1 = this, layer 2 = 2×, etc. Higher index = moves more with camera (X).")]
    [SerializeField][Range(0f, 1f)] private float perLayerSpeedOffset = 0.2f;

    [Tooltip("1 = layers follow camera Y; 0 = layers fixed in world (ignore camera Y).")]
    [SerializeField][Range(0f, 1f)] private float verticalSpeedOffset = 1f;

    private Vector2 _cameraStart;
    private float[] _layerOffsetX;
    private float[] _layerOffsetY;
    private float[] _layerWidth;

    void Start()
    {
        if (cameraTransform == null) return;

        _cameraStart = cameraTransform.position;
        Vector2 bgStart = transform.position;
        int n = transform.childCount;
        _layerOffsetX = new float[n];
        _layerOffsetY = new float[n];
        _layerWidth = new float[n];

        for (int i = 0; i < n; i++)
        {
            Transform layer = transform.GetChild(i);
            ClearLayerCopies(layer);

            SpriteRenderer sr = layer.GetComponent<SpriteRenderer>();
            float wRaw = sr != null ? sr.bounds.size.x : 10f;
            _layerWidth[i] = Mathf.Max(0.001f, wRaw);
            _layerOffsetX[i] = layer.position.x - bgStart.x;
            _layerOffsetY[i] = layer.position.y - bgStart.y;

            if (sr != null)
            {
                float w = _layerWidth[i];
                CreateCopy(sr, layer, -w, 0f);
                CreateCopy(sr, layer, w, 0f);
                CreateCopy(sr, layer, -w * 2f, 0f);
                CreateCopy(sr, layer, w * 2f, 0f);
                CreateCopy(sr, layer, -w * 3f, 0f);
                CreateCopy(sr, layer, w * 3f, 0f);
                CreateCopy(sr, layer, -w * 4f, 0f);
                CreateCopy(sr, layer, w * 4f, 0f);
            }
        }
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
        int n = _layerOffsetX?.Length ?? 0;
        if (n == 0) return;

        float cameraX = cameraTransform.position.x;
        float cameraY = cameraTransform.position.y;
        float dx = cameraX - _cameraStart.x;
        Vector2 bgNow = transform.position;
        float closestY = n > 0 ? bgNow.y + _layerOffsetY[n - 1] : bgNow.y;
        float yCameraTarget = cameraY + (bgNow.y - _cameraStart.y);

        for (int i = 0; i < n; i++)
        {
            float parallax = i * perLayerSpeedOffset;
            float x = bgNow.x + _layerOffsetX[i] + dx * parallax;
            x = WrapHorizontal(x, cameraX, _layerWidth[i]);

            float t = (float)(i + 1) / (n + 1);
            float yFixed = bgNow.y + _layerOffsetY[i];
            float yCameraFollowing = Mathf.Lerp(closestY, yCameraTarget, t);
            float y = Mathf.Lerp(yFixed, yCameraFollowing, verticalSpeedOffset);

            Transform layer = transform.GetChild(i);
            layer.position = new Vector3(x, y, layer.position.z);
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
