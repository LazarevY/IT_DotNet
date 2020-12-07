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
            int width = 50;
            int height = 50;
            context.Rectangle(loc.X - width, loc.Y - height, width * 2, height  * 2);
            context.SetSourceRGB(0, 1, 0);
            context.Fill();
            int available = (int) ((double)model.Storage.Available / model.Storage.Capacity * height);
            

            context.SetSourceRGB(1,1,1);
            context.Rectangle(loc.X - width, loc.Y - height + available, width * 2, (height - available)  * 2);
            context.Fill();
        }
    }
}