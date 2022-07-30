using Enforsement.Core.Servise;

namespace Enforsement;

public partial class App : Application
{
    static string generalPath = FileSystem.Current.AppDataDirectory;
    static DataBaseServise dataBase;
    public static DataBaseServise DataBase
    {
        get
        {
            if (dataBase == null)
            {
                dataBase = new DataBaseServise(Path.Combine(generalPath, "EnforsementDataBase.db3"));
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
