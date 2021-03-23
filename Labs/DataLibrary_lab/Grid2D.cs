using System;

namespace Lab
{
    [Serializable]
    public struct Grid2D
    {
        public float Ox { get; set; }
        public float Oy { get; set; }
        public int Nx { get; set; }
        public int Ny { get; set; }

        public Grid2D(float ox, float oy, int nx, int ny)
        {
            Ox = ox;
            Oy = oy;
            Nx = nx;
            Ny = ny;
        }

        public string ToString(string format)
        {
            return $"Ox step: {Ox.ToString(format)}\tNumber of steps: {Nx}\tOy step: {Oy.ToString(format)}\tNumber of steps: {Ny}";
        }

        public override string ToString()
        {
            return ToString(null);
        }
    }
}