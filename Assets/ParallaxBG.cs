using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] [Range(0f, 1f)] private float[] layerSpeeds = { 0.2f, 0.4f, 0.6f };

    private float _startCameraX;
    private float[] _startLayerX;
    private float[] _layerWidth;

    void Start()
    {
        if (cameraTransform == null) return;
        _startCameraX = cameraTransform.position.x;

        int n = transform.childCount;
        _startLayerX = new float[n];
        _layerWidth = new float[n];

        for (int i = 0; i < n; i++)
        {
            Transform layer = transform.GetChild(i);
            var sr = layer.GetComponent<SpriteRenderer>();
            _startLayerX[i] = layer.position.x;
            if (sr == null)
            {
                _layerWidth[i] = 10f;
                continue;
            }

            float w = sr.bounds.size.x;
            _layerWidth[i] = w;

            // Duplicate sprite left and right for seamless looping
            CreateCopy(sr, layer, -w);
            CreateCopy(sr, layer, w);
        }
    }

    static void CreateCopy(SpriteRenderer source, Transform parent, float localX)
    {
        var go = new GameObject(source.name + "_copy");
        go.transform.SetParent(parent, false);
        go.transform.localPosition = new Vector3(localX, 0f, 0f);
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
        if (cameraTransform == null || _startLayerX == null) return;

        float cameraX = cameraTransform.position.x;
        float dx = cameraX - _startCameraX;

        for (int i = 0; i < transform.childCount; i++)
        {
            float speed = i < layerSpeeds.Length ? layerSpeeds[i] : 0.5f;
            float x = _startLayerX[i] + dx * speed;

            float w = _layerWidth[i];
            while (x < cameraX - w) x += w;
            while (x > cameraX + w) x -= w;

            Transform child = transform.GetChild(i);
            child.position = new Vector3(x, child.position.y, child.position.z);
        }
    }
}
