using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class LoaderDraw: ModelDraw<LoaderObject>
    {
        public override void Draw(LoaderObject model, Context context)
        {
            context.LineWidth = 2;
            context.SetSourceRGB(1, 0.2, 0);
            var loc = model.Location;
            int width = 20;
            int height = 30;
            context.Rectangle(loc.X - width, loc.Y - height, width * 2, height  * 2);
            context.Stroke();
            context.GetTarget().Dispose();
        }
    }
}