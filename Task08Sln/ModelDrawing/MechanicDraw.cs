using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class MechanicDraw: ModelDraw<MechanicObject>
    {
        public override void Draw(MechanicObject model, Context context)
        {
            context.LineWidth = 2;
            context.SetSourceRGB(0, 0.8, 0);
            var loc = model.Location;
            int width = 10;
            int height = 10;
            context.Rectangle(loc.X - width, loc.Y - height, width * 2, height  * 2);
            context.FillPreserve();
            context.GetTarget().Dispose();
        }
    }
}