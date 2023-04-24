using Microsoft.AspNetCore.Hosting;

public class Program
{
    /// <summary>
    ///  ����� ����� - ����� Main
    /// </summary>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// ����������� �����, ��������� � ������������� IHostBuilder -
    /// ������, ������� � ���� ������� ������� ���� ��� ������������� ������ ����������
    /// </summary>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}
