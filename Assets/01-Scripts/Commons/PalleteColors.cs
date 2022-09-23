using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalleteColors : MonoBehaviour
{
    [ReadOnly, SerializeField] private Color main;
    [ReadOnly, SerializeField] private Color[] analogous;
    [ReadOnly, SerializeField] private Color[] complementary;
    [ReadOnly, SerializeField] private Color[] compound;
    [ReadOnly, SerializeField] private Color[] double_split_complementary;
    [ReadOnly, SerializeField] private Color[] monochromatic;
    [ReadOnly, SerializeField] private Color[] shades;
    [ReadOnly, SerializeField] private Color[] split_complementary;
    [ReadOnly, SerializeField] private Color[] square;
    [ReadOnly, SerializeField] private Color[] triad;
    [ReadOnly, SerializeField] private Color[] gray_scale;
    [ReadOnly, SerializeField] private Color[] blueGrey;

    public const string TAG = "PalleteColors";

    public Color Main => main;
    public Color Cyan => DoubleSplitComplementary(2);
    public Color Magenta => Analogous(3);
    public Color Red => Square(0);
    public Color Yellow => Square(1);
    public Color Green => Square(2);
    public Color Blue => Square(3);
    public Color Gray => GrayScale(3);
    public Color BlueGray_White => BlueGrey(0);
    public Color BlueGray_WhiteBlue => BlueGrey(1);
    public Color BlueGray_Gray => BlueGrey(3);
    public Color BlueGray_Black => BlueGrey(6);

    public Color Analogous(int level)
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
    public Color Complementary(int level)
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
    public Color Compound(int level)
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
    public Color DoubleSplitComplementary(int level)
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
    public Color Monochromatic(int level)
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
    public Color Shades(int level)
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
    public Color SplitComplementary(int level)
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
    public Color Square(int level)
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
    public Color Triad(int level)
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
    public Color GrayScale(int level)
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
    public Color BlueGrey(int level)
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
