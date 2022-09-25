using static System.Console;
namespace Mozgovoi_M.B_
{
    class Program
    {
        private const int MapWidth = 60;
        private const int MapHeight = 30;

        static void Main(string[] args)
        {
            SetWindowSize(MapWidth, MapHeight);
            SetBufferSize(MapWidth, MapHeight);
            CursorVisible = false;

            //Chapter1_GasMolecule gm = new Chapter1_GasMolecule();
            //gm.Start();

            //Chapter1_Equilibrium gm = new Chapter1_Equilibrium();
            //gm.Start();

            //Chapter1_FallingBall fb = new Chapter1_FallingBall();
            //fb.Start();

            //Chapter1_SolarSystem ss = new Chapter1_SolarSystem();
            //ss.Start();

            Chapter1_DefinitionOfPI pi = new Chapter1_DefinitionOfPI();
            pi.Start();
        }
    }
}