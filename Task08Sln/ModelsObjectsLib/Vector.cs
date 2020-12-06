using System;

namespace ModelsObjectsLib
{
    public class Vector
    {

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        private Vector()
        {
            
        }

        public double X { get; set; }
        public double Y { get; set; }

        public Vector Add(Vector vector)
        {
            return new Vector(X + vector.X,Y + vector.Y);
        }

        public Vector Subtract(Vector vector)
        {
            return new Vector (X - vector.X, Y - vector.Y);
        }

        public double Norm()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }

        public Vector Normalized()
        {
            double norm = Norm();
            if (Math.Abs(norm) < 1e-5)
                throw new Exception("Zero length of vector!");

            return new Vector (X / norm,Y / norm);
        }

        public Vector Multiply(double value)
        {
            return new Vector (X * value, Y * value);
        }

        public override string ToString()
        {
            return $"Vector [{X}, {Y}]";
        }
    }
}