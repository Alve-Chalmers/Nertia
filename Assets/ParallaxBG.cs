using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [Tooltip("Per-layer parallax: layer 0 = 0, layer 1 = this, layer 2 = 2×, etc. Higher index = moves more with camera (X).")]
    [SerializeField] [Range(0f, 1f)] private float perLayerSpeedOffset = 0.2f;

    private Vector2 _cameraStart;
    private Vector2 _bgStart;
    private float[] _layerStartX;
    private float[] _layerStartY;
    private float[] _layerWidth;

    void Start()
    {
        if (cameraTransform == null) return;
        _cameraStart = cameraTransform.position;
        _bgStart = transform.position;

        int n = transform.childCount;
        _layerStartX = new float[n];
        _layerStartY = new float[n];
        _layerWidth = new float[n];

        for (int i = 0; i < n; i++)
        {
            Transform layer = transform.GetChild(i);
            // Remove existing copies so we don't duplicate when Start runs again (ExecuteInEditMode / domain reload)
            for (int c = layer.childCount - 1; c >= 0; c--)
            {
                if (Application.isPlaying)
                    Destroy(layer.GetChild(c).gameObject);
                else
                    DestroyImmediate(layer.GetChild(c).gameObject);
            }
            SpriteRenderer sr = layer.GetComponent<SpriteRenderer>();

            float wRaw = sr != null ? sr.bounds.size.x : 10f;
            _layerWidth[i] = Mathf.Max(0.001f, wRaw);
            _layerStartX[i] = layer.position.x;
            _layerStartY[i] = layer.position.y;

            if (sr != null)
            {
                float w = _layerWidth[i];
                CreateCopy(sr, layer, -w, 0f);
                CreateCopy(sr, layer, w, 0f);
                CreateCopy(sr, layer, -w * 2, 0f);
                CreateCopy(sr, layer, w * 2, 0f);
            }
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
        int n = transform.childCount;
        if (_layerStartX == null || _layerStartY == null || _layerStartX.Length != n) return;

        float cameraX = cameraTransform.position.x;
        float cameraY = cameraTransform.position.y;
        float dx = cameraX - _cameraStart.x;
        Vector2 bgNow = transform.position;
        // Anchor to current BG position so moving the empty moves everything
        float closestY = n > 0 ? bgNow.y + (_layerStartY[n - 1] - _bgStart.y) : bgNow.y;
        float yCameraTarget = cameraY + (bgNow.y - _cameraStart.y);

        for (int i = 0; i < n; i++)
        {
            // X: relative to current BG position
            float parallax = i * perLayerSpeedOffset;
            float x = bgNow.x + (_layerStartX[i] - _bgStart.x) + dx * parallax;
            x = WrapHorizontal(x, cameraX, _layerWidth[i]);

            // Y: player counts as first (closest) layer at t=0; visible layers start at t=1/n so closest visible is offset
            float t = n <= 0 ? 0f : (float)(i + 1) / (n + 1);
            float y = Mathf.Lerp(closestY, yCameraTarget, t);

            Transform child = transform.GetChild(i);
            child.position = new Vector3(x, y, child.position.z);
        }
    }

    static float WrapHorizontal(float x, float cameraX, float layerWidth)
    {
        if (layerWidth <= 0.001f) return x; // avoid infinite loop from zero/negative width
        while (x < cameraX - layerWidth) x += layerWidth;
        while (x > cameraX + layerWidth) x -= layerWidth;
        return x;
    }
}
