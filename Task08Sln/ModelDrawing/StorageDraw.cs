using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class StorageDraw: ModelDraw<StorageObject>
    {
        public override void Draw(StorageObject model, Context context)
        {
            context.LineWidth = 2;
            var loc = model.Location;
            int width = 30;
            int height = 30;
            context.Rectangle(loc.X - width, loc.Y - height, width * 2, height  * 2);
            context.SetSourceRGB(0, 1, 0);
            context.Stroke();
            context.GetTarget().Dispose();
        }
    }
}