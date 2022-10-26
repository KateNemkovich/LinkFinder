using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using LinkFinder.ViewModels;
using LinkFinder.Views;

namespace LinkFinder.Commands;

[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class Command : ExternalCommand
{
    public override void Execute()
    {
        var viewModel = new LinkFinderViewModel();
        var view = new LinkFinderView(viewModel);
        view.ShowDialog();
    }
}