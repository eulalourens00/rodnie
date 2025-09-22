using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using rodnie.DCE;

namespace rodnie;
    
public partial class MainPage : ContentPage {
    public MainPage() {
        InitializeComponent();

        var collegePos = new Location(55.679154, 37.657988);
        map.MoveToRegion(MapSpan.FromCenterAndRadius(collegePos, Distance.FromMeters(500)));

        Location shopPos = new Location(55.68264102278141, 37.66232979064646);
        Pin shopPin = new Pin {
            Type = PinType.SearchResult,
            Label = "Магаз хз",
            Location = shopPos
        };
        map.Pins.Add(shopPin);

        //var collegePinDefault = new Pin {
        //    Label = "ВАЙБ КОДИМ ЖЕСКА",
        //    Location = collegePos
        //};
        //map.Pins.Add(collegePinDefault);

        var collegePin = new TestPin {
            Label = "ВАЙБ КОДИМ",
            Location = collegePos,
            CircleColor = Colors.Cyan,
            Aspect = PinAspectMode.Fit,
            PinWidth = 300
        };
        map.Pins.Add(collegePin);


        //map.MapElements.Add(new Circle {
        //    Center = new Location(55.679154, 37.657988),
        //    Radius = new Distance(50),
        //    FillColor = Color.FromRgba(0, 255, 255, 0.5),
        //    StrokeColor = Colors.Transparent
        //});
        
    }
}

