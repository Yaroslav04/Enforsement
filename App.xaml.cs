using Enforsement.Core.Servise;

namespace Enforsement;

public partial class App : Application
{
    public static string generalPath = @"Q:\Enforsement\";
    static DataBaseServise dataBase;
    public static DataBaseServise DataBase
    {
        get
        {
            if (dataBase == null)
            {
                dataBase = new DataBaseServise(Path.Combine(generalPath, @"Data\EnforsementDataBase.db3"));
            }
            return dataBase;
        }
    }
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
