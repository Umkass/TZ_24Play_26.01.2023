using Services.WindowService;
using UnityEngine;

namespace UI.Windows
{
  public class WindowBase : MonoBehaviour
  {
    protected IWindowService WindowService;
    public WindowId Id;

    public void Construct(IWindowService windowService) =>
      WindowService = windowService;
  }
}