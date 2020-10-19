namespace Lab
{
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

        public override string ToString()
        {
            return $"Ox step: {Ox}\tNumber of steps: {Nx}\tOy step: {Oy}\tNumber of steps: {Ny}";
        }
    }
}