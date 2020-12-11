using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class LoaderDraw: ModelDraw<LoaderObject>
    {
        private ImageSurface _loader = new ImageSurface("/home/lazarev/RiderProjects/IT_Tasks/IT_DotNet/Task08Sln/Pictures/loader.png");
        public override void Draw(LoaderObject model, Context context)
        {
            context.LineWidth = 2;
            var loc = model.Location;
            int width = 45;
            int height = 30;
            
            double wf = (double) width * 2 / _loader.Width;
            double hf = (double) height * 2 / _loader.Height;
            
            context.Scale(wf, hf);
            context.SetSourceSurface(_loader, (int) ((loc.X - width) / wf), (int) ((loc.Y - height) / hf));
            context.Paint();
            context.Scale(1 / wf, 1 / hf);
        }
    }
}