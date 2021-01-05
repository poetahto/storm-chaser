using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressBar : MonoBehaviour
{
    private LevelGameplayController _controller;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void Bind(LevelGameplayController controller)
    {
        _controller = controller;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        _slider.value = _controller.PercentComplete;
    }
}