using ASP;
using ASP.Extensions;
using ASP.Scriptables;
using UnityEngine;

namespace TreinAR.Commons
{
    public class AdobeColorPalette : MonoBehaviour
    {
        [SerializeField, ReadOnly] private ColorPaletteScriptable analogous;
        [SerializeField, ReadOnly] private ColorPaletteScriptable complementary;
        [SerializeField, ReadOnly] private ColorPaletteScriptable compound;
        [SerializeField, ReadOnly] private ColorPaletteScriptable main;
        [SerializeField, ReadOnly] private ColorPaletteScriptable monochromatic;
        [SerializeField, ReadOnly] private ColorPaletteScriptable shades;
        [SerializeField, ReadOnly] private ColorPaletteScriptable splitComplementary;
        [SerializeField, ReadOnly] private ColorPaletteScriptable square;
        [SerializeField, ReadOnly] private ColorPaletteScriptable triad;

        public Color Analougous(int index, float alpha = 1f)
        {
            int length = analogous.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Analougous)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                analogous.Color[index].SetAlpha(alpha);
            return analogous.Color[index];
        }

        public Color Complementary(int index, float alpha = 1f)
        {
            int length = complementary.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Complementary)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                complementary.Color[index].SetAlpha(alpha);
            return complementary.Color[index];
        }

        public Color Compound(int index, float alpha = 1f)
        {
            int length = compound.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Compound)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                compound.Color[index].SetAlpha(alpha);
            return compound.Color[index];
        }

        public Color Main(float alpha = 1f)
        {
            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                main.Color[0].SetAlpha(alpha);
            return main.Color[0];
        }

        public Color Monochromatic(int index, float alpha = 1f)
        {
            int length = complementary.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Monochromatic)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                monochromatic.Color[index].SetAlpha(alpha);
            return monochromatic.Color[index];
        }

        public Color Shades(int index, float alpha = 1f)
        {
            int length = shades.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Shades)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                shades.Color[index].SetAlpha(alpha);
            return shades.Color[index];
        }

        public Color SplitComplementary(int index, float alpha = 1f)
        {
            int length = splitComplementary.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(SplitComplementary)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                splitComplementary.Color[index].SetAlpha(alpha);
            return splitComplementary.Color[index];
        }

        public Color Square(int index, float alpha = 1f)
        {
            int length = square.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Square)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                square.Color[index].SetAlpha(alpha);
            return square.Color[index];
        }

        public Color Triad(int index, float alpha = 1f)
        {
            int length = square.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Triad)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                square.Color[index].SetAlpha(alpha);
            return square.Color[index];
        }
    }
}
