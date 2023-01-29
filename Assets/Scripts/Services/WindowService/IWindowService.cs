using UI.Windows;

namespace Services.WindowService
{
  public interface IWindowService : IService
  {
    public void Open(WindowId windowId);
    public void CloseWindow(WindowId windowId);
  }
}