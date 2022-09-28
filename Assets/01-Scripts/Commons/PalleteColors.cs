using UnityEngine;
public static class PalleteColors
{
    private static Color main = new(r: 0.98039216f, g: 0.3019608f, b: 0.3254902f, a: 1f);
    private static readonly Color[] analogous =  new Color[]
    {
            new Color(r: 0.99215686f,g: 0.4627451f, b: 0.25882354f, a: 1f),
            new Color(r: 0.8901961f, g: 0.32156864f, b: 0.23137255f, a: 1f),
            new Color(r: 0.8901961f, g: 0.23137255f, b: 0.58431375f, a: 1f),
            new Color(r: 0.9764706f, g: 0.25882354f, b: 0.99215686f, a: 1f)
    };
    private static readonly Color[] complementary = new Color[]
    {
        new Color(r: 0.6784314f, g: 0.14117648f, b: 0.16078432f, a: 1f),
        new Color(r: 1f, g: 0.4117647f, b: 0.43137255f, a: 1f),
        new Color(r: 0.07450981f, g: 0.6784314f, b: 0.23529412f, a: 1f),
        new Color(r: 0.3019608f, g: 0.98039216f, b: 0.48235294f, a: 1f)
    };
    private static readonly Color[] compound = new Color[]
    {
        new Color(r: 0.78039217f, g: 0.3372549f, b: 0.16470589f, a: 1f),
        new Color(r: 0.5803922f, g: 0.45882353f, b: 0.4117647f, a: 1f),
        new Color(r: 0.56078434f, g: 0.9843137f, b: 0.54901963f, a: 1f),
        new Color(r: 0.39215687f, g: 0.78039217f, b: 0.16470589f, a: 1f)
    };
    private static readonly Color[] double_split_complementary = new Color[]
    {
        new Color(r: 0.98039216f, g: 0.5294118f, b: 0.3529412f, a: 1f),
        new Color(r: 0.6156863f, g: 0.98039216f, b: 0.4f, a: 1f),
        new Color(r: 0.20392157f, g: 0.98039216f, b: 0.84705883f, a: 1f),
        new Color(r: 0.96862745f, g: 0.25490198f, b: 0.98039216f, a: 1f)
    };
    private static readonly Color[] monochromatic = new Color[]
    {
        new Color(r: 0.47843137f, g: 0.14901961f, b: 0.16078432f, a: 1),
        new Color(r: 0.9843137f, g: 0.6f, b: 0.6117647f, a: 1f),
        new Color(r: 0.47843137f, g: 0.29411766f, b: 0.29803923f, a: 1f),
        new Color(r: 0.78039217f, g: 0.24313726f, b: 0.25882354f, a: 1f)
    };
    private static readonly Color[] shades = new Color[]
    {
        new Color(r: 0.7294118f, g: 0.22745098f, b: 0.24313726f, a: 1f),
        new Color(r: 0.47843137f, g: 0.14901961f, b: 0.16078432f, a: 1f),
        new Color(r: 0.23137255f, g: 0.07058824f, b: 0.078431375f, a: 1f),
        new Color(r: 0.8784314f, g: 0.27450982f, b: 0.29411766f, a: 1f)
    };
    private static readonly Color[] split_complementary = new Color[]
    {
        new Color(r: 0.6784314f, g: 0.14117648f, b: 0.16078432f, a: 1f),
        new Color(r: 1f, g: 0.4117647f, b: 0.43137255f, a: 1f),
        new Color(r: 0.07450981f, g: 0.6784314f, b: 0.23529412f, a: 1f),
        new Color(r: 0.3019608f, g: 0.98039216f, b: 0.48235294f, a: 1f)
    };
    private static readonly Color[] square = new Color[]
    {
        new Color(r: 0.98039216f, g: 0.20392157f, b: 0.23137255f, a: 1f),
        new Color(r: 0.98039216f, g: 0.8509804f, b: 0.4f, a: 1f),
        new Color(r: 0.20392157f, g: 0.98039216f, b: 0.4117647f, a: 1f),
        new Color(r: 0.25490198f, g: 0.32941177f, b: 0.98039216f, a: 1f)
    };
    private static readonly Color[] triad = new Color[]
    {
        new Color(r: 0.6784314f, g: 0.14117648f, b: 0.16078432f, a: 1f),
        new Color(r: 0.98039216f, g: 0.96862745f, b: 0.4f, a: 1f),
        new Color(r: 0.20392157f, g: 0.69411767f, b: 0.98039216f, a: 1f),
        new Color(r: 0.1764706f, g: 0.49411765f, b: 0.6784314f, a: 1f)
    };
    private static readonly Color[] gray_scale = new Color[]
    {
        new Color(r: 0.7921569f, g: 0.7921569f, b: 0.7921569f, a: 1f),
        new Color(r: 0.6392157f, g: 0.6392157f, b: 0.6392157f, a: 1f),
        new Color(r: 0.5254902f, g: 0.5254902f, b: 0.5254902f, a: 1f),
        new Color(r: 0.47843137f, g: 0.47843137f, b: 0.47843137f, a: 1f),
        new Color(r: 0.38431373f, g: 0.38431373f, b: 0.38431373f, a: 1f),
        new Color(r: 0.3137255f, g: 0.3137255f, b: 0.3137255f, a: 1f),
        new Color(r: 0.14901961f, g: 0.14901961f, b: 0.14901961f, a: 1f)
    };
    private static readonly Color[] blueGrey = new Color[]
    {
        new Color(r: 0.9254902f, g: 0.9372549f, b: 0.94509804f, a: 1f),
        new Color(r: 0.8117647f, g: 0.84705883f, b: 0.8627451f, a: 1f),
        new Color(r: 0.7294118f, g: 0.7411765f, b: 0.74509805f, a: 1f),
        new Color(r: 0.61960787f, g: 0.654902f, b: 0.6666667f, a: 1f),
        new Color(r: 0.30980393f, g: 0.35686275f, b: 0.38431373f, a: 1f),
        new Color(r: 0.14901961f, g: 0.19607843f, b: 0.21960784f, a: 1f),
        new Color(r: 0f, g: 0.039215688f, b: 0.07058824f, a: 1f)
    };

    public static Color Main => main;
    public static Color Cyan => DoubleSplitComplementary(2);
    public static Color Magenta => Analogous(3);
    public static Color Red => Square(0);
    public static Color Yellow => Square(1);
    public static Color Green => Square(2);
    public static Color Blue => Square(3);
    public static Color Gray => GrayScale(3);
    public static Color BlueGray_White => BlueGrey(0);
    public static Color BlueGray_WhiteBlue => BlueGrey(1);
    public static Color BlueGray_Gray => BlueGrey(3);
    public static Color BlueGray_Black => BlueGrey(6);

    public static Color Analogous(int level)
    {
        if (level > 3 || level < 0)
        {
            Debug.LogError("The int level for the ANALOGOUS color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return analogous[level];
        }
    }
    public static Color Complementary(int level)
    {
        if (level > 3 || level< 0)
        {
            Debug.LogError("The int level for the COMPLEMENTARY color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return complementary[level];
        }
    }
    public static Color Compound(int level)
    {
        if (level > 3 || level < 0)
        {
            Debug.LogError("The int level for the COMPOUND color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return compound[level];
        }
    }
    public static Color DoubleSplitComplementary(int level)
    {
        if (level > 3 || level < 0)
        {
            Debug.LogError("The int level for the DOUBLE_SPLIT_COMPLEMENTARY color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return double_split_complementary[level];
        }
    }
    public static Color Monochromatic(int level)
    {
        if (level > 3 || level < 0)
        {
            Debug.LogError("The int level for the MONOCHROMATIC color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return monochromatic[level];
        }
    }
    public static Color Shades(int level)
    {
        if (level > 3 || level < 0)
        {
            Debug.LogError("The int level for the SHADES color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return shades[level];
        }
    }
    public static Color SplitComplementary(int level)
    {
        if (level > 3 || level < 0)
        {
            Debug.LogError("The int level for the SPLIT_COMPLEMENTARY color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return split_complementary[level];
        }
    }
    public static Color Square(int level)
    {
        if (level > 3 || level < 0)
        {
            Debug.LogError("The int level for the SQUARE color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return square[level];
        }
    }
    public static Color Triad(int level)
    {
        if (level > 3 || level < 0)
        {
            Debug.LogError("The int level for the TRIAD color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return triad[level];
        }
    }
    public static Color GrayScale(int level)
    {
        if (level > 6 || level < 0)
        {
            Debug.LogError("The int level for the GRAY_SCALE color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return gray_scale[level];
        }
    }
    public static Color BlueGrey(int level)
    {
        if (level > 6 || level < 0)
        {
            Debug.LogError("The int level for the BLUEGREY color palette must be between 0 and 3! Level entered: " + level);
            return Color.magenta;
        }
        else
        {
            return blueGrey[level];
        }
    }









}
