//Copy from Ordersystem.Droid (Test considered different platform by Xamarin.Forms.DependecyService)

using Ordersystem.Utilities;
using AndroidEvironmentTests.Utilities;

[assembly: Xamarin.Forms.Dependency(typeof(DataPath_Android))]
namespace AndroidEvironmentTests.Utilities
{
    public class DataPath_Android : IDataPath
    {
        public DataPath_Android() {}

        public string GetPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }
    }
}