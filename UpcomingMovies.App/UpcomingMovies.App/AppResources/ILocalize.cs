using System.Globalization;

namespace UpcomingMovies.App.AppResources
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }
}
