using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class StorageDraw : ModelDraw<StorageObject>
    {
        private ImageSurface _storage = new ImageSurface("/home/lazarev/RiderProjects/IT_Tasks/IT_DotNet/Task08Sln/Pictures/storage.png");
        public override void Draw(StorageObject model, Context context)
        {
            context.LineWidth = 2;
            var loc = model.Location;
            int width = 60;
            int height = 60;
            double wf = (double) width * 2 / _storage.Width;
            double hf = (double) height * 2 / _storage.Height;

            context.Scale(wf, hf);
            context.SetSourceSurface(_storage, (int) ((loc.X - width) / wf), (int) ((loc.Y - height) / hf));
            context.Paint();
            context.Scale(1 / wf, 1 / hf);
            context.SetSourceRGB(1, 1, 1);
            context.SetFontSize(16);
            context.MoveTo(loc.X - width, loc.Y + height + 15);
            context.ShowText($"Storage fill: {model.Storage.Filled} / {model.Storage.Capacity}");
        }
    }
}