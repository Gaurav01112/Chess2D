using UnityEngine;

/// <summary>
/// ChessCameraManager
/// Fits an orthographic 2D camera to a square chess board so the whole board
/// is always visible, centered, and padded correctly — across phones, tablets,
/// and orientation changes.
///
/// Attach this to your Main Camera (must be Orthographic).
/// </summary>
[RequireComponent(typeof(Camera))]
public class ChessCameraManager : MonoBehaviour
{
    public enum DeviceCategory { Phone, Tablet, Unknown }

    [Header("Board Setup")]
    [Tooltip("World-space size of one square edge of the board (assumes a square NxN board).")]
    [SerializeField] private float boardWorldSize = 8f;
    [Tooltip("World-space center of the board, e.g. (0,0) if the board is centered at the origin.")]
    [SerializeField] private Vector2 boardCenter = Vector2.zero;

    [Header("Padding (world units added around the board)")]
    [SerializeField] private float paddingPhone = 0.6f;
    [SerializeField] private float paddingTablet = 1.2f;
    [Tooltip("Extra padding reserved at the top of the screen for UI (turn indicator, timers, etc).")]
    [SerializeField] private float topUIPaddingPhone = 1.2f;
    [SerializeField] private float topUIPaddingTablet = 1.6f;
    [Tooltip("Extra padding reserved at the bottom of the screen for UI (captured pieces, buttons, etc).")]
    [SerializeField] private float bottomUIPaddingPhone = 1.2f;
    [SerializeField] private float bottomUIPaddingTablet = 1.6f;

    [Header("Device Detection")]
    [Tooltip("Screens with diagonal size (inches) at or above this are treated as tablets.")]
    [SerializeField] private float tabletDiagonalInchThreshold = 6.5f;
    [SerializeField] private bool overrideDeviceCategory = false;
    [SerializeField] private DeviceCategory forcedCategory = DeviceCategory.Phone;

    [Header("Safe Area")]
    [Tooltip("Respect notches / rounded corners / system bars.")]
    [SerializeField] private bool applySafeArea = true;

    private Camera _camera;
    private Rect _lastSafeArea = new Rect(0, 0, 0, 0);
    private ScreenOrientation _lastOrientation;
    private int _lastScreenWidth;
    private int _lastScreenHeight;

    public DeviceCategory CurrentCategory { get; private set; } = DeviceCategory.Unknown;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        if (!_camera.orthographic)
        {
            Debug.LogWarning("[ChessCameraManager] Camera should be Orthographic for a 2D chess board. Forcing it.");
            _camera.orthographic = true;
        }
    }

    private void Start()
    {
        CurrentCategory = DetectDeviceCategory();
        Refresh();
    }

    private void Update()
    {
        bool screenChanged = Screen.width != _lastScreenWidth || Screen.height != _lastScreenHeight;
        bool safeAreaChanged = applySafeArea && (Screen.safeArea != _lastSafeArea || Screen.orientation != _lastOrientation);

        if (screenChanged || safeAreaChanged)
        {
            Refresh();
        }
    }

    /// <summary>
    /// Re-detects device category and re-fits the camera to the board.
    /// Call this manually after resolution changes you control (e.g. windowed resize in editor tests).
    /// </summary>
    public void Refresh()
    {
        CurrentCategory = overrideDeviceCategory ? forcedCategory : DetectDeviceCategory();
        ApplySafeAreaViewport();
        FitCameraToBoard();

        _lastScreenWidth = Screen.width;
        _lastScreenHeight = Screen.height;
        _lastSafeArea = Screen.safeArea;
        _lastOrientation = Screen.orientation;
    }

    private DeviceCategory DetectDeviceCategory()
    {
        float dpi = Screen.dpi;
        if (dpi <= 0f)
        {
            Debug.LogWarning("[ChessCameraManager] Screen.dpi unavailable, defaulting to Phone category.");
            return DeviceCategory.Phone;
        }

        float widthInches = Screen.width / dpi;
        float heightInches = Screen.height / dpi;
        float diagonalInches = Mathf.Sqrt(widthInches * widthInches + heightInches * heightInches);

        return diagonalInches >= tabletDiagonalInchThreshold ? DeviceCategory.Tablet : DeviceCategory.Phone;
    }

    /// <summary>
    /// Sets orthographic size and position so the full board fits on screen
    /// (accounting for the camera's *effective* viewport after safe-area cropping),
    /// with device-appropriate padding and space reserved for top/bottom UI.
    /// </summary>
    private void FitCameraToBoard()
    {
        bool isTablet = CurrentCategory == DeviceCategory.Tablet;

        float padding = isTablet ? paddingTablet : paddingPhone;
        float topUI = isTablet ? topUIPaddingTablet : topUIPaddingPhone;
        float bottomUI = isTablet ? bottomUIPaddingTablet : bottomUIPaddingPhone;

        // Effective aspect ratio is based on the camera's rect (post safe-area), not the raw screen.
        float effectivePixelWidth = Screen.width * _camera.rect.width;
        float effectivePixelHeight = Screen.height * _camera.rect.height;
        float aspect = effectivePixelWidth / effectivePixelHeight;

        // Total world-space height needed: board + side padding (top/bottom) + UI reservations.
        float requiredHeight = boardWorldSize + (padding * 2f) + topUI + bottomUI;
        float requiredWidth = boardWorldSize + (padding * 2f);

        // Orthographic size is half the *vertical* world size the camera shows.
        float sizeFromHeight = requiredHeight / 2f;
        float sizeFromWidth = (requiredWidth / aspect) / 2f;

        _camera.orthographicSize = Mathf.Max(sizeFromHeight, sizeFromWidth);

        // Shift the camera center up/down slightly so extra bottom UI space doesn't
        // push the board off-center visually (board sits centered between top/bottom UI bands).
        float verticalShift = (bottomUI - topUI) / 2f;
        transform.position = new Vector3(boardCenter.x, boardCenter.y - verticalShift, transform.position.z);
    }

    /// <summary>
    /// Crops the camera viewport to Unity's safe area so the board and UI
    /// never sit under a notch, rounded corner, or system bar.
    /// </summary>
    private void ApplySafeAreaViewport()
    {
        if (!applySafeArea)
        {
            _camera.rect = new Rect(0f, 0f, 1f, 1f);
            return;
        }

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        _camera.rect = new Rect(anchorMin.x, anchorMin.y, anchorMax.x - anchorMin.x, anchorMax.y - anchorMin.y);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        boardWorldSize = Mathf.Max(0.01f, boardWorldSize);
        paddingPhone = Mathf.Max(0f, paddingPhone);
        paddingTablet = Mathf.Max(0f, paddingTablet);
        topUIPaddingPhone = Mathf.Max(0f, topUIPaddingPhone);
        topUIPaddingTablet = Mathf.Max(0f, topUIPaddingTablet);
        bottomUIPaddingPhone = Mathf.Max(0f, bottomUIPaddingPhone);
        bottomUIPaddingTablet = Mathf.Max(0f, bottomUIPaddingTablet);
        tabletDiagonalInchThreshold = Mathf.Max(1f, tabletDiagonalInchThreshold);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the board bounds in the Scene view for easy alignment.
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3(boardCenter.x, boardCenter.y, 0f);
        Gizmos.DrawWireCube(center, new Vector3(boardWorldSize, boardWorldSize, 0f));
    }
#endif
}