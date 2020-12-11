using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class MechanicDraw: ModelDraw<MechanicObject>
    {
        private ImageSurface _mechanic = new ImageSurface("/home/lazarev/RiderProjects/IT_Tasks/IT_DotNet/Task08Sln/Pictures/mechanic.png");
        public override void Draw(MechanicObject model, Context context)
        {
            context.LineWidth = 2;
            context.SetSourceRGB(0, 0.8, 0);
            var loc = model.Location;
            int width = 50;
            int height = 60;
            
            double wf = (double) width * 2 / _mechanic.Width;
            double hf = (double) height * 2 / _mechanic.Height;
            
            context.Scale(wf, hf);
            context.SetSourceSurface(_mechanic, (int) ((loc.X - width) / wf), (int) ((loc.Y - height) / hf));
            context.Paint();
            context.Scale(1 / wf, 1 / hf);
        }
    }
}