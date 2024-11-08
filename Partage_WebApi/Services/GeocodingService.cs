using Geocoding;
using Geocoding.Google;
using Microsoft.Extensions.Options;
using Partage.Models;

namespace Partage.Services

{

    public class GeocodingService
    {
        private readonly IGeocoder _geocoder;


        public GeocodingService(IOptions<GeocodingOptions> options)

        {
            _geocoder = new GoogleGeocoder(options.Value.ApiKey);
        }


        public (double Latitude, double Longitude) GetCoordinates(string adresse)

        {
            var result = _geocoder.Geocode(adresse).FirstOrDefault();

            if (result != null){

                return (result.Coordinates.Latitude, result.Coordinates.Longitude);

            }

            throw new Exception("Adresse non trouvée.");

        }


        public async Task UpdateAdresseWithCoordinates(Adresse adresse)

        {

            var (latitude, longitude) = GetCoordinates($"{adresse.NumCivique} {adresse.NomRue}, {adresse.Ville}, {adresse.Province}");

            adresse.Latitude = latitude;
            adresse.Longitude = longitude;

        }

    }

}