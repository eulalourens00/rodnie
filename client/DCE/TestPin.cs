using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Graphics;

namespace rodnie.DCE;

internal class TestPin : Pin {
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(TestPin));

    public static readonly BindableProperty CircleColorProperty =
        BindableProperty.Create(nameof(CircleColor), typeof(Color), typeof(TestPin), Colors.Cyan);

    // Новые свойства: ширина и высота пина (в пикселях, для scaling Bitmap)
    public static readonly BindableProperty PinWidthProperty =
        BindableProperty.Create(nameof(PinWidth), typeof(float), typeof(TestPin), 150f);

    public static readonly BindableProperty PinHeightProperty =
        BindableProperty.Create(nameof(PinHeight), typeof(float), typeof(TestPin), 150f);

    public static readonly BindableProperty AspectProperty =
        BindableProperty.Create(nameof(Aspect), typeof(PinAspectMode), typeof(TestPin), PinAspectMode.Fit);

    public ImageSource? ImageSource {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
    public Color CircleColor {
        get => (Color)GetValue(CircleColorProperty);
        set => SetValue(CircleColorProperty, value);
    }
    public float PinWidth {
        get => (float)GetValue(PinWidthProperty);
        set => SetValue(PinWidthProperty, value);
    }
    public float PinHeight {
        get => (float)GetValue(PinHeightProperty);
        set => SetValue(PinHeightProperty, value);
    }
    public PinAspectMode Aspect {
        get => (PinAspectMode)GetValue(AspectProperty);
        set => SetValue(AspectProperty, value);
    }
}

public enum PinAspectMode {
    Stretch,  // Точное растяжение (может искажать)
    Fit,      // Вписать целиком (с пропорциями, возможны прозрачные края)
    Fill      // Заполнить целиком (с пропорциями, обрезка краёв)
}

