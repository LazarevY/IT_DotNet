using System;
using Cairo;

namespace ModelDrawing
{
    public interface IModelDrawBase
    {
        void Draw(object model, Context context);
        Type TypeOfObject();
    }
}