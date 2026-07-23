using UnityEngine;

public class ShooterReticlePointer : MonoBehaviour
{
    [Range(-32767, 32767)]
    public int ReticleSortingOrder = 32767;

    public LayerMask ReticleInteractionLayerMask = 1 << _RETICLE_INTERACTION_DEFAULT_LAYER;

    [Header("Sprite Reticle")]
    [SerializeField] private SpriteRenderer reticleSprite;
    [SerializeField] private Animator reticleAnimator;
    [SerializeField] private float spriteDistance = 1f;

    [Header("Animations")]
    [SerializeField] private string idleAnimation = "Idle";
    [SerializeField] private string enterAnimation = "Enter";
    [SerializeField] private string exitAnimation = "Exit";
    [SerializeField] private string clickAnimation = "Click";

    private const int _RETICLE_INTERACTION_DEFAULT_LAYER = 8;
    private const float _RETICLE_MIN_INNER_ANGLE = 0f;
    private const float _RETICLE_MIN_OUTER_ANGLE = 0.5f;
    private const float _RETICLE_GROWTH_ANGLE = 1.5f;
    private const float _RETICLE_MIN_DISTANCE = 0.45f;
    private const float _RETICLE_MAX_DISTANCE = 20f;
    private const int _RETICLE_SEGMENTS = 20;
    private const float _RETICLE_GROWTH_SPEED = 8f;

    private GameObject _gazedAtObject;
    private Material _reticleMaterial;

    private float _reticleInnerAngle;
    private float _reticleOuterAngle;
    private float _reticleDistanceInMeters;
    private float _reticleInnerDiameter;
    private float _reticleOuterDiameter;

    private void Start()
    {
        if (reticleSprite != null)
        {
            reticleSprite.sortingOrder = ReticleSortingOrder;
            reticleSprite.transform.localPosition = Vector3.forward * spriteDistance;
        }

        PlayAnimation(idleAnimation);
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _RETICLE_MAX_DISTANCE))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (_gazedAtObject != hitObject)
            {
                if (IsInteractive(_gazedAtObject))
                {
                    _gazedAtObject.SendMessage("OnPointerExit");
                    PlayAnimation(exitAnimation);
                }

                _gazedAtObject = hitObject;

                if (IsInteractive(_gazedAtObject))
                {
                    _gazedAtObject.SendMessage("OnPointerEnter");
                    PlayAnimation(enterAnimation);
                }
            }

            SetParams(hit.distance, IsInteractive(_gazedAtObject));
        }
        else
        {
            if (IsInteractive(_gazedAtObject))
            {
                _gazedAtObject.SendMessage("OnPointerExit");
                PlayAnimation(exitAnimation);
            }

            _gazedAtObject = null;
            ResetParams();
        }

        if ((Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetMouseButtonDown(0)) && IsInteractive(_gazedAtObject))
        {
            _gazedAtObject.SendMessage("OnPointerClick");
            PlayAnimation(clickAnimation);
        }
    }

    private void PlayAnimation(string animationName)
    {
        if (reticleAnimator == null)
            return;

        reticleAnimator.Play(animationName, 0, 0f);
    }

    private void SetParams(float distance, bool interactive)
    {
        _reticleDistanceInMeters = Mathf.Clamp(
            distance,
            _RETICLE_MIN_DISTANCE,
            _RETICLE_MAX_DISTANCE);

        if (interactive)
        {
            _reticleInnerAngle =
                _RETICLE_MIN_INNER_ANGLE + _RETICLE_GROWTH_ANGLE;

            _reticleOuterAngle =
                _RETICLE_MIN_OUTER_ANGLE + _RETICLE_GROWTH_ANGLE;
        }
        else
        {
            _reticleInnerAngle = _RETICLE_MIN_INNER_ANGLE;
            _reticleOuterAngle = _RETICLE_MIN_OUTER_ANGLE;
        }
    }

    private void ResetParams()
    {
        _reticleDistanceInMeters = _RETICLE_MAX_DISTANCE;
        _reticleInnerAngle = _RETICLE_MIN_INNER_ANGLE;
        _reticleOuterAngle = _RETICLE_MIN_OUTER_ANGLE;
    }

    private bool IsInteractive(GameObject target)
    {
        return target != null &&
               (1 << target.layer & ReticleInteractionLayerMask) != 0;
    }
}