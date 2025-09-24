using Android.Gms.Maps.Model;
using Android.Gms.Maps;

namespace rodnie.Platforms.Android.Handlers;

internal class CustomMarkerClickListener(CustomMapHandler mapHandler) : Java.Lang.Object, GoogleMap.IOnMarkerClickListener {
    public bool OnMarkerClick(Marker marker) {
        var pin = mapHandler.Markers.FirstOrDefault(x => x.marker.Id == marker.Id);
        pin.pin?.SendMarkerClick();
        marker.ShowInfoWindow();
        return true;
    }
}
