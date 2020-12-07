using System;
using Cairo;

namespace ModelDrawing

{
    public abstract class ModelDraw<T>: IModelDrawBase
    {
        public abstract void Draw(T model, Context context);
        public void Draw(object model, Context context)
        {
            Draw((T)model, context);
        }

        public Type TypeOfObject()
        {
            return typeof(T);
        }
    }
}