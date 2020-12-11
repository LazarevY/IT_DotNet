using System;
using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class FarmDraw: ModelDraw<FarmObject>
    {
        private ImageSurface _farm = new ImageSurface("/home/lazarev/RiderProjects/IT_Tasks/IT_DotNet/Task08Sln/Pictures/farm.png");
        public override void Draw(FarmObject model, Context context)
        {
            context.LineWidth = 2;
            context.SetSourceRGB(1 * (double)model.Farm.Equipment.Strength / 100, 0.2, 0);
            var loc = model.Location;
            int width = 80;
            int height = 70;

            double wf = (double) width * 2 / _farm.Width;
            double hf = (double) height * 2 / _farm.Height;
            
            context.Scale(wf, hf);
            context.SetSourceSurface(_farm, (int) ((loc.X - width) / wf), (int) ((loc.Y - height) / hf));
            context.Paint();
            context.Scale(1 / wf, 1 / hf);
            context.SetSourceRGB(1,1,1);
            context.MoveTo(loc.X - width, loc.Y - height - 15);
            context.ShowText($"Equip: {model.Farm.Equipment.Strength} / 100");
        }
    }
}