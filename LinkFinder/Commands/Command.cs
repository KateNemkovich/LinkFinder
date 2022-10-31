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
             var viewPlan = ((ViewPlan) ActiveView);
            var viewRange = viewPlan.GetViewRange();
            
            var minOffset = viewRange.GetOffset(PlanViewPlane.BottomClipPlane);
            var maxOffset = viewRange.GetOffset(PlanViewPlane.CutPlane);
            
            var minLevelId = viewRange.GetLevelId(PlanViewPlane.BottomClipPlane);
            var maxLevelId = viewRange.GetLevelId(PlanViewPlane.CutPlane);
            
            var maxLevel = ((Level) maxLevelId.ToElement(Document))!.ProjectElevation;
            var maxLevelZ = maxLevel+maxOffset;
            var minLevelZ= minLevelId == ElementId.InvalidElementId
                ? double.MinValue
                :((Level) minLevelId.ToElement(Document))!.ProjectElevation + minOffset;
        
            var boundingBox = ActiveView.get_BoundingBox(null);
            var maximumPoint = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, maxLevelZ);
            var minimumPoint = new XYZ(boundingBox.Min.X, boundingBox.Min.Y, minLevelZ);
            
            var outline = new Outline(minimumPoint, maximumPoint);
            var boundingBoxIntersectsFilter = new BoundingBoxIntersectsFilter(outline); 
            
            //Получаем список документов и из них выбираем один который связанный
            var documentLink = Application.Documents.Cast<Document>().Where(document => document.IsLinked);
            var count=0;
            
            foreach (var document in documentLink)
            {
                var doors = new FilteredElementCollector(document)
                    .WhereElementIsNotElementType()
                    .OfCategory(BuiltInCategory.OST_Doors)
                    .WherePasses(boundingBoxIntersectsFilter)
                    .ToElements();
                count+=doors.Count;
            }
    }
}

    
      
