using Nice3point.Revit.Toolkit.External;
using LinkFinder.Commands;

namespace LinkFinder;

[UsedImplicitly]
public class Application : ExternalApplication
{
    public override void OnStartup()
    {
        CreateRibbon();
    }

    private void CreateRibbon()
    {
        var panel = Application.CreatePanel("Panel name", "LinkFinder");

        var showButton = panel.AddPushButton<Command>("Button text");
        showButton.SetImage("/LinkFinder;component/Resources/Icons/RibbonIcon16.png");
        showButton.SetLargeImage("/LinkFinder;component/Resources/Icons/RibbonIcon32.png");
    }
}