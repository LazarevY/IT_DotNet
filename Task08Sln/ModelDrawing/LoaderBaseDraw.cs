using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class LoaderBaseDraw: ModelDraw<LoaderBaseObject>
    {
        public override void Draw(LoaderBaseObject model, Context context)
        {
            context.LineWidth = 2;
            var loc = model.Location;
            int width = 60;
            int height = 45;
            context.Rectangle(loc.X - width, loc.Y - height, width * 2, height  * 2);
            context.SetSourceRGB(0, 1, 1);
            context.Stroke();
        }
    }
}