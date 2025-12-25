using UnityEngine;

public class ControllerUpdater : MonoBehaviour
{
    private CompositeController _compositeController;

    public void Initialize(params Controller[] controllers)
    {
        _compositeController = new(controllers);
        _compositeController.Enable();
    }

    private void Update()
    {
        _compositeController.Update(Time.deltaTime);
    }
}
