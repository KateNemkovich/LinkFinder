using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
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
        var documentLink = Application.Documents.Cast<Document>().Where(document => document.IsLinked);
        var count=0;
        foreach (var document in documentLink)
        {
            var doors = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Doors)
                .WherePasses( new SelectableInViewFilter(Document,Document.ActiveView.Id))
                .ToElements();
            count =count+doors.Count;
        }
       
    }
   
}
    
      
