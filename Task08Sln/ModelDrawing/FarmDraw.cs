using System;
using Cairo;
using ModelsObjectsLib;

namespace ModelDrawing
{
    public class FarmDraw: ModelDraw<FarmObject>
    {
        public override void Draw(FarmObject model, Context context)
        {
            context.LineWidth = 2;
            context.SetSourceRGB(1 * (double)model.Farm.Equipment.Strength / 100, 0.2, 0);
            var loc = model.Location;
            int width = 50;
            int height = 70;
            context.Rectangle(loc.X - width, loc.Y - height, width * 2, height  * 2);
            context.Fill();
            context.MoveTo(loc.X , loc.Y);
            context.SetSourceRGB(1, 1, 1);
            context.ShowText($"{model.Farm.Equipment.Strength}/100");
        }
    }
}