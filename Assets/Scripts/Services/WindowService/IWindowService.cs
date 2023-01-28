using UI.Windows;

namespace Services.WindowService
{
  public interface IWindowService : IService
  {
    void Open(WindowId windowId);
    void CloseWindow(WindowId windowId);
  }
}