namespace Bai4_PhongKhamMDI;
internal static class Program
{
    [STAThread]
    private static void Main() { ApplicationConfiguration.Initialize(); Application.Run(new Form1()); }
}
