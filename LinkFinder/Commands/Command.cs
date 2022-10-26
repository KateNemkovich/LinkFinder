using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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
       //Получаем список документов и из них выбираем один который связанный
        var documentLink = Application.Documents.Cast<Document>().First(document => document.IsLinked);
        var doors = new FilteredElementCollector(documentLink,ActiveView.Id)
            .WhereElementIsNotElementType()
            .OfCategory(BuiltInCategory.OST_Doors)
            .ToElements();
    }
}
    
      
