using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace rodnie;
    
public partial class MainPage : ContentPage {
    public MainPage() {
        InitializeComponent();

        var collegePos = new Location(55.679154, 37.657988);
        map.MoveToRegion(MapSpan.FromCenterAndRadius(collegePos, Distance.FromMeters(500)));
        
        var collegePin = new Pin {
            Label = "Пользователь",
            Location = collegePos,
            Type = PinType.Place
        };
        map.Pins.Add(collegePin);

        Location shopPos = new Location(55.68264102278141, 37.66232979064646);
        Pin shopPin = new Pin {
            Type = PinType.SearchResult,
            Label = "Магаз хз",
            Location = shopPos
        };
        map.Pins.Add(shopPin);

        map.MapElements.Add(new Circle {
            Center = new Location(55.679154, 37.657988),
            Radius = new Distance(50),
            FillColor = Color.FromRgba(0, 255, 255, 0.5),
            StrokeColor = Colors.Transparent
        });
    }
}

