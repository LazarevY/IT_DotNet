using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class LoaderDraw: ModelDraw<LoaderObject>
    {
        public override void Draw(LoaderObject model, Context context)
        {
            context.LineWidth = 2;
            var loc = model.Location;
            int width = 30;
            int height = 35;
            context.SetSourceRGB(0.4, 0.2, 0.1);
            context.Rectangle(loc.X - width, loc.Y - height, width * 2, height  * 2);
            context.Fill();
        }
    }
}