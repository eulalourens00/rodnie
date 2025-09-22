using rodnie.DCE;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Maps.Handlers;
using Microsoft.Maui.Platform;

using MGraphics = Microsoft.Maui.Graphics;
using AGraphics = Android.Graphics;
using IMap = Microsoft.Maui.Maps.IMap;
using Android.Hardware.Lights;
using static Android.Icu.Text.ListFormatter;

namespace rodnie.Platforms.Android.Handlers;

public class CustomMapHandler : MapHandler {
    public static readonly IPropertyMapper<IMap, IMapHandler> CustomMapper =
        new PropertyMapper<IMap, IMapHandler>(Mapper) {
            [nameof(IMap.Pins)] = MapPins
        };

    public GoogleMap? Map { get; set; }  // Хранение нативной GoogleMap

    public CustomMapHandler() : base(CustomMapper, CommandMapper) {
    }

    public CustomMapHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null) : base(
        mapper ?? CustomMapper, commandMapper ?? CommandMapper) {
    }

    // Список подконтрольных нам маркеров
    public List<(IMapPin pin, Marker marker)> Markers { get; } = new();

    protected override void ConnectHandler(MapView platformView) {
        base.ConnectHandler(platformView);
        var mapReady = new MapCallbackHandler(this);  // Callback для OnMapReady
        PlatformView.GetMapAsync(mapReady);
    }

    // Внутренний класс для обработки готовности карты
    private class MapCallbackHandler : Java.Lang.Object, IOnMapReadyCallback {
        private readonly CustomMapHandler _handler;

        public MapCallbackHandler(CustomMapHandler handler) {
            _handler = handler;
        }

        public void OnMapReady(GoogleMap googleMap) {
            _handler.Map = googleMap;

            // Подписываемся на клики маркеров (используя ваш CustomMarkerClickListener)
            googleMap.SetOnMarkerClickListener(new CustomMarkerClickListener(_handler));

            // Добавляем существующие Pins после готовности карты
            if (_handler.VirtualView is IMap map) {
                MapPins(_handler, map);
            }
        }
    }

    // Основной метод, вызывается каждое обновление map.Pins
    private static new void MapPins(IMapHandler handler, IMap map) {
        if (handler is CustomMapHandler mapHandler) {
            var pinsToAdd = map.Pins.Where(x => x.MarkerId == null).ToList();
            var pinsToRemove = mapHandler.Markers.Where(x => !map.Pins.Contains(x.pin)).ToList();
            foreach (var marker in pinsToRemove) {
                marker.marker.Remove();
                mapHandler.Markers.Remove(marker);
            }

            mapHandler.AddPins(pinsToAdd);
        }
    }

    // Получаем все пины, функция которая будет их рисовать на карте
    private void AddPins(IEnumerable<IMapPin> mapPins) {
        if (Map is null || MauiContext is null) {
            return;
        }

        foreach (var pin in mapPins) {
            var pinHandler = pin.ToHandler(MauiContext);
            if (pinHandler is IMapPinHandler mapPinHandler) {
                var markerOption = mapPinHandler.PlatformView;

                if (pin is TestPin p) {
                    // Генерируем кастомный Bitmap с кругом и текстом (пока игнорируем ImageSource)
                    var customBitmap = GenerateCustomBitmap(p);

                    // Динамические размеры: берём из свойств PinWidth и PinHeight (fallback на 150f)
                    float width = Math.Max(p.PinWidth, 1f);  // Минимум 1px
                    float height = Math.Max(p.PinHeight, 1f);  // Минимум 1px

                    AGraphics.Bitmap scaledBitmap;
                    switch (p.Aspect) {
                        case PinAspectMode.Fit:
                            scaledBitmap = GetMaximumBitmapAspectFit(customBitmap, width, height);
                            break;
                        case PinAspectMode.Fill:
                            scaledBitmap = GetMaximumBitmapAspectFill(customBitmap, width, height);
                            break;
                        case PinAspectMode.Stretch:
                        default:
                            scaledBitmap = GetMaximumBitmap(customBitmap, width, height);  // Оригинальный stretch
                            break;
                    }

                    markerOption.SetIcon(BitmapDescriptorFactory.FromBitmap(scaledBitmap));

                    // Позиционирование: якорь в центре верха иконки (пин "висит" сверху от Location)
                    markerOption.Anchor(0.5f, 1.0f);  // 0.5f = центр по X, 0.0f = верх по Y

                    // Если в будущем ImageSource не null, можно загрузить и наложить:
                    // cp.ImageSource?.LoadImage(MauiContext, result => { /* наложить на bitmap */ });
                } else {
                    // Для стандартных Pins — дефолтная иконка
                }

                AddMarker(Map, pin, markerOption);
            }
        }
    }

    private void AddMarker(GoogleMap map, IMapPin pin, MarkerOptions markerOption) {
        var marker = map.AddMarker(markerOption);
        pin.MarkerId = marker.Id;
        Markers.Add((pin, marker));
    }

    // Новый метод: генерация Bitmap с кругом и текстом снизу
    private AGraphics.Bitmap GenerateCustomBitmap(TestPin pin) {
        const int size = 256;  // Размер Bitmap
        var bitmap = AGraphics.Bitmap.CreateBitmap(size, size, AGraphics.Bitmap.Config.Argb8888);
        // Очищаем bitmap для прозрачного фона
        bitmap.EraseColor(AGraphics.Color.Transparent);


        var canvas = new AGraphics.Canvas(bitmap);

        // Рисуем круг (сверху, полупрозрачный)
        var circlePaint = new AGraphics.Paint {
            AntiAlias = true,
            Color = ToAndroidColorWithAlpha(pin.CircleColor, 128)  // Конвертируем MAUI Color в Android Color
        };
        var circleRadius = size * 0.3f;  // Радиус ~25 пикселей
        var circleCenterX = size / 2f;
        var circleCenterY = size / 3f;  // Сверху (1/3 от высоты)
        canvas.DrawCircle(circleCenterX, circleCenterY, circleRadius, circlePaint);

        // Рисуем текст снизу (Label Pin'а)
        var text = string.IsNullOrEmpty(pin.Label) ? "No Label" : pin.Label;
        var textPaint = new AGraphics.Paint {
            Color = AGraphics.Color.Black,
            TextSize = 40f,  // Размер шрифта
            TextAlign = AGraphics.Paint.Align.Center,
            AntiAlias = true,
            FakeBoldText = true
        };

        var textBounds = new AGraphics.Rect();
        textPaint.GetTextBounds(text, 0, text.Length, textBounds);
        var textX = circleCenterX;  // float
        var textY = (size * 2f / 3f) + (textBounds.Height() / 2f);  // float, центрируем снизу
        canvas.DrawText(text, textX, textY, textPaint);

        // Освобождаем canvas
        canvas.Dispose();

        return bitmap;
    }

    // = = = = = = = =
    // ДАЛЬШЕ БОГА НЕТ
    // = = = = = = = =

    // Вспомогательный метод: конвертация MAUI Color в Android Color
    private AGraphics.Color ToAndroidColor(MGraphics.Color mauiColor) {
        return AGraphics.Color.Argb(
            255,  // Alpha: 0-1 -> 0-255
            (int)(mauiColor.Red * 255),    // Red: 0-1 -> 0-255
            (int)(mauiColor.Green * 255),  // Green: 0-1 -> 0-255
            (int)(mauiColor.Blue * 255)    // Blue: 0-1 -> 0-255
        );
    }

    // Новый метод: конвертация с заданным alpha
    private AGraphics.Color ToAndroidColorWithAlpha(MGraphics.Color mauiColor, int alpha) {
        return AGraphics.Color.Argb(
            alpha,  // Заданный alpha (0-255)
            (int)(mauiColor.Red * 255),
            (int)(mauiColor.Green * 255),
            (int)(mauiColor.Blue * 255)
        );
    }

    // ОРИГИНАЛЬНЫЙ: Точное масштабирование (Stretch: может искажать пропорции)
    private static AGraphics.Bitmap GetMaximumBitmap(AGraphics.Bitmap sourceImage, float maxWidth, float maxHeight) {
        float sourceWidth = sourceImage.Width;
        float sourceHeight = sourceImage.Height;

        // Точное масштабирование до maxWidth/maxHeight
        float resizeFactorX = maxWidth / sourceWidth;
        float resizeFactorY = maxHeight / sourceHeight;
        float width = sourceWidth * resizeFactorX;
        float height = sourceHeight * resizeFactorY;

        // Ограничиваем минимальный размер
        width = Math.Max(width, 1f);
        height = Math.Max(height, 1f);
        var scaled = AGraphics.Bitmap.CreateScaledBitmap(sourceImage, (int)width, (int)height, true);  // true = фильтрация для качества
        if (scaled == null) {
            throw new InvalidOperationException("Failed to scale Bitmap");
        }
        return scaled;
    }

    // НОВЫЙ: AspectFit — вписать целиком (с пропорциями, центрирование с прозрачными краями)
    private static AGraphics.Bitmap GetMaximumBitmapAspectFit(AGraphics.Bitmap sourceImage, float maxWidth, float maxHeight) {
        float sourceWidth = sourceImage.Width;
        float sourceHeight = sourceImage.Height;

        // Пропорциональное масштабирование: используем Min, чтобы вписаться целиком
        float resizeFactor = Math.Min(maxWidth / sourceWidth, maxHeight / sourceHeight);
        float scaledWidth = sourceWidth * resizeFactor;
        float scaledHeight = sourceHeight * resizeFactor;

        // Создаём новый Bitmap целевого размера (с прозрачным фоном)
        var targetBitmap = AGraphics.Bitmap.CreateBitmap((int)maxWidth, (int)maxHeight, AGraphics.Bitmap.Config.Argb8888);
        targetBitmap.EraseColor(AGraphics.Color.Transparent);

        // Масштабируем source и рисуем на target (центрируем)
        var scaledSource = AGraphics.Bitmap.CreateScaledBitmap(sourceImage, (int)scaledWidth, (int)scaledHeight, true);
        var canvas = new AGraphics.Canvas(targetBitmap);

        // Центрирование: offsetX/Y для размещения в центре target
        float offsetX = (maxWidth - scaledWidth) / 2f;
        float offsetY = (maxHeight - scaledHeight) / 2f;
        var srcRect = new AGraphics.Rect(0, 0, (int)scaledWidth, (int)scaledHeight);
        var dstRect = new AGraphics.Rect((int)offsetX, (int)offsetY, (int)offsetX + (int)scaledWidth, (int)offsetY + (int)scaledHeight);
        var paint = new AGraphics.Paint { FilterBitmap = true };  // Качество
        canvas.DrawBitmap(scaledSource, srcRect, dstRect, paint);

        // Освобождаем ресурсы
        scaledSource?.Recycle();
        canvas.Dispose();
        paint?.Dispose();
        if (targetBitmap == null) {
            throw new InvalidOperationException("Failed to create AspectFit Bitmap");
        }
        return targetBitmap;
    }

    // AspectFill — заполнить целиком (с пропорциями, обрезка краёв)
    private static AGraphics.Bitmap GetMaximumBitmapAspectFill(AGraphics.Bitmap sourceImage, float maxWidth, float maxHeight) {
        float sourceWidth = sourceImage.Width;
        float sourceHeight = sourceImage.Height;

        // Пропорциональное масштабирование: используем Max, чтобы заполнить (может выйти за границы)
        float resizeFactor = Math.Max(maxWidth / sourceWidth, maxHeight / sourceHeight);
        float scaledWidth = sourceWidth * resizeFactor;
        float scaledHeight = sourceHeight * resizeFactor;

        // Масштабируем source
        var scaledSource = AGraphics.Bitmap.CreateScaledBitmap(sourceImage, (int)scaledWidth, (int)scaledHeight, true);
        if (scaledSource == null) {
            throw new InvalidOperationException("Failed to scale source for AspectFill");
        }
        // Создаём target Bitmap целевого размера (с прозрачным фоном)
        var targetBitmap = AGraphics.Bitmap.CreateBitmap((int)maxWidth, (int)maxHeight, AGraphics.Bitmap.Config.Argb8888);
        targetBitmap.EraseColor(AGraphics.Color.Transparent);
        var canvas = new AGraphics.Canvas(targetBitmap);

        // Обрезка по центру: вычисляем offset в scaledSource (центральная часть, которая вписывается в target)
        float offsetX = (scaledWidth - maxWidth) / 2f;
        float offsetY = (scaledHeight - maxHeight) / 2f;
        var srcRect = new AGraphics.Rect((int)offsetX, (int)offsetY, (int)offsetX + (int)maxWidth, (int)offsetY + (int)maxHeight);  // Обрезаем scaledSource
        var dstRect = new AGraphics.Rect(0, 0, (int)maxWidth, (int)maxHeight);  // Рисуем на весь target
        var paint = new AGraphics.Paint { FilterBitmap = true };  // Качество
        canvas.DrawBitmap(scaledSource, srcRect, dstRect, paint);

        // Освобождаем ресурсы
        scaledSource.Recycle();
        canvas.Dispose();
        paint.Dispose();
        if (targetBitmap == null) {
            throw new InvalidOperationException("Failed to create AspectFill Bitmap");
        }
        return targetBitmap;
    }
}