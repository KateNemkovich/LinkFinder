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

        var boundingBox = ActiveView.get_BoundingBox(null);
        var viewPlan = ((ViewPlan) ActiveView);
        var viewRange = viewPlan.GetViewRange();
        var minOffset = viewRange.GetOffset(PlanViewPlane.BottomClipPlane);
        var minlevelId = viewRange.GetLevelId(PlanViewPlane.BottomClipPlane);
        var maxOffset = viewRange.GetOffset(PlanViewPlane.CutPlane);
        var maxLevelId = viewRange.GetLevelId(PlanViewPlane.CutPlane);
        var maxLevel = ((Level) maxLevelId.ToElement(Document)).ProjectElevation;
        var maxLevelZ = maxLevel+maxOffset;
        
        double minLevelZ;
        if (minlevelId == ElementId.InvalidElementId)
        {
            minLevelZ= double.MinValue;
        }
        else
        {
           var level = ((Level) minlevelId.ToElement(Document)).ProjectElevation;
           minLevelZ = level + minOffset;
        }
        
        new XYZ(boundingBox.Max.X, boundingBox.Max.Y, minLevelZ);
        new XYZ(boundingBox.Min.X, boundingBox.Min.Y, maxLevelZ);
          
        foreach (var document in documentLink)
        {
            var doors = new FilteredElementCollector(document)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Doors)
                .WherePasses( new BoundingBoxIntersectsFilter(new Outline()))
                .ToElements();
            count =count+doors.Count;
        }
       
    }
   
}
    
      
