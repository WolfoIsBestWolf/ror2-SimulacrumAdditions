using UnityEngine;

namespace SimulacrumAdditions
{
    public class WRect
    {
        public static readonly System.Random random = new System.Random();

        public static Rect recnothing = new Rect(0, 0, 0, 0);
        public static Rect recwide = new Rect(0, 0, 384, 256);
        public static Rect rechalftall = new Rect(0, 0, 256, 320);
        public static Rect rechalfwide = new Rect(0, 0, 320, 256);
        public static Rect rectall = new Rect(0, 0, 256, 384);
        public static Rect rec512 = new Rect(0, 0, 512, 512);
        public static Rect rec320 = new Rect(0, 0, 320, 320);
        public static Rect rec256 = new Rect(0, 0, 256, 256);
        public static Rect rec192 = new Rect(0, 0, 192, 192);
        public static Rect rec128 = new Rect(0, 0, 128, 128);
        public static Rect rec106 = new Rect(0, 0, 106, 106);
        public static Rect rec64 = new Rect(0, 0, 64, 64);
        public static Vector2 half = new Vector2(0.5f, 0.5f);
    }
}