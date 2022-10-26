using LinkFinder.ViewModels;

namespace LinkFinder.Views;

public partial class LinkFinderView
{
    public LinkFinderView(LinkFinderViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}