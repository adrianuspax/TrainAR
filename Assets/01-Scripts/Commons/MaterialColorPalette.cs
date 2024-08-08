using ASP;
using ASP.Extensions;
using ASP.Scriptables;
using UnityEngine;

namespace TreinAR.Commons
{
    public class MaterialColorPalette : MonoBehaviour
    {
        [SerializeField, ReadOnly] private ColorPaletteScriptable amber;
        [SerializeField, ReadOnly] private ColorPaletteScriptable blue;
        [SerializeField, ReadOnly] private ColorPaletteScriptable blueGrey;
        [SerializeField, ReadOnly] private ColorPaletteScriptable brown;
        [SerializeField, ReadOnly] private ColorPaletteScriptable cyan;
        [SerializeField, ReadOnly] private ColorPaletteScriptable deepOrange;
        [SerializeField, ReadOnly] private ColorPaletteScriptable green;
        [SerializeField, ReadOnly] private ColorPaletteScriptable grey;
        [SerializeField, ReadOnly] private ColorPaletteScriptable indigo;
        [SerializeField, ReadOnly] private ColorPaletteScriptable lightBlue;
        [SerializeField, ReadOnly] private ColorPaletteScriptable lightGreen;
        [SerializeField, ReadOnly] private ColorPaletteScriptable lime;
        [SerializeField, ReadOnly] private ColorPaletteScriptable orange;
        [SerializeField, ReadOnly] private ColorPaletteScriptable pink;
        [SerializeField, ReadOnly] private ColorPaletteScriptable purple;
        [SerializeField, ReadOnly] private ColorPaletteScriptable red;
        [SerializeField, ReadOnly] private ColorPaletteScriptable teal;
        [SerializeField, ReadOnly] private ColorPaletteScriptable yellow;

        public Color Amber(int index, float alpha)
        {
            int length = amber.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Amber)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                amber.Color[index].SetAlpha(alpha);
            return amber.Color[index];
        }

        public Color Blue(int index, float alpha)
        {
            int length = blue.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Blue)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                blue.Color[index].SetAlpha(alpha);
            return blue.Color[index];
        }

        public Color BlueGrey(int index, float alpha)
        {
            int length = blueGrey.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(BlueGrey)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                blueGrey.Color[index].SetAlpha(alpha);
            return blueGrey.Color[index];
        }

        public Color Brown(int index, float alpha)
        {
            int length = brown.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Brown)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                brown.Color[index].SetAlpha(alpha);
            return brown.Color[index];
        }

        public Color Cyan(int index, float alpha)
        {
            int length = cyan.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Cyan)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                cyan.Color[index].SetAlpha(alpha);
            return cyan.Color[index];
        }

        public Color DeepOrange(int index, float alpha)
        {
            int length = deepOrange.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(DeepOrange)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                deepOrange.Color[index].SetAlpha(alpha);
            return deepOrange.Color[index];
        }

        public Color Green(int index, float alpha)
        {
            int length = green.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Green)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                green.Color[index].SetAlpha(alpha);
            return green.Color[index];
        }

        public Color Grey(int index, float alpha)
        {
            int length = grey.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Grey)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                grey.Color[index].SetAlpha(alpha);
            return grey.Color[index];
        }

        public Color Indigo(int index, float alpha)
        {
            int length = indigo.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Indigo)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                indigo.Color[index].SetAlpha(alpha);
            return indigo.Color[index];
        }

        public Color LightBlue(int index, float alpha)
        {
            int length = lightBlue.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(LightBlue)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                lightBlue.Color[index].SetAlpha(alpha);
            return lightBlue.Color[index];
        }

        public Color LightGreen(int index, float alpha)
        {
            int length = lightGreen.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(LightGreen)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                lightGreen.Color[index].SetAlpha(alpha);
            return lightGreen.Color[index];
        }

        public Color Lime(int index, float alpha)
        {
            int length = lime.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Lime)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                lime.Color[index].SetAlpha(alpha);
            return lime.Color[index];
        }

        public Color Orange(int index, float alpha)
        {
            int length = orange.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Orange)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                orange.Color[index].SetAlpha(alpha);
            return orange.Color[index];
        }

        public Color Pink(int index, float alpha)
        {
            int length = pink.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Pink)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                pink.Color[index].SetAlpha(alpha);
            return pink.Color[index];
        }

        public Color Purple(int index, float alpha)
        {
            int length = purple.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Purple)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                purple.Color[index].SetAlpha(alpha);
            return purple.Color[index];
        }

        public Color Red(int index, float alpha)
        {
            int length = red.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Red)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                red.Color[index].SetAlpha(alpha);
            return red.Color[index];
        }

        public Color Teal(int index, float alpha)
        {
            int length = teal.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Teal)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                teal.Color[index].SetAlpha(alpha);
            return teal.Color[index];
        }

        public Color Yellow(int index, float alpha)
        {
            int length = yellow.Color.Length;

            if (index < 0 || index >= length)
            {
                ASP.Debug.LogError($"The index value for {nameof(Yellow)} should be between 0 and {length - 1}, but {index} has been inserted!");
                return Color.magenta;
            }

            alpha = Mathf.Clamp01(alpha);

            if (alpha != 1f)
                yellow.Color[index].SetAlpha(alpha);
            return yellow.Color[index];
        }
    }
}
