using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace rodnie;
    
public partial class MainPage : ContentPage {
    public MainPage() {
        InitializeComponent();

        var moscowPos = new Location(55.7558, 37.6173);

        map.MoveToRegion(MapSpan.FromCenterAndRadius(moscowPos, Distance.FromKilometers(10)));

        var userPin = new Pin {
            Label = "Пользователь",
            Location = moscowPos,
            Type = PinType.Place
        };
        map.Pins.Add(userPin);
    }
}

