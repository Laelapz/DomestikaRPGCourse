using ScriptableObjectArchitecture;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LoadingScreenUI : MonoBehaviour
{
    [Header("Broadcasting on channels")]
    public BoolGameEvent loadingScreenToggled;

    [Header("Private dependencies")]
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ToggleScreen(bool enable)
    {
        if (enable)
        {
            _animator.SetTrigger("Show");
        }
        else
        {
            _animator.SetTrigger("Hide");
        }
    }

    public void SendLoadingScreenShowEvent()
    {
        loadingScreenToggled.Raise(true);
    }

    public void SendLoadingScreenHideEvent()
    {
        loadingScreenToggled.Raise(false);
    }
}
